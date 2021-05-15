using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace CGWH.Core.Input
{
    public class SimpleKeyboardListener
    {
        private const int WH_KEYBOARD_LL = 13;

        private const int WM_KEYDOWN = 0x0100;

        private const int WM_SYSKEYDOWN = 0x0104;



        private IntPtr hookId = IntPtr.Zero;

        private Handle handle { get; }



        internal delegate IntPtr Handle(int nCode, IntPtr wParam, IntPtr lParam);

        internal event Action<Key> OnKeyPressed;



        internal SimpleKeyboardListener()
        {
            handle = hookCallback;

            Hook();
        }



        #region DLL-Import`s

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, Handle lpfn, IntPtr hMod, uint dwThreadId);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);


        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion



        internal void Hook() => hookId = setHook(handle);

        internal void UnHook() => UnhookWindowsHookEx(hookId);



        private IntPtr setHook(Handle handle) => SetWindowsHookEx(WH_KEYBOARD_LL, handle, GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);

        private IntPtr hookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN)
                OnKeyPressed?.Invoke(KeyInterop.KeyFromVirtualKey(Marshal.ReadInt32(lParam)));

            return CallNextHookEx(hookId, nCode, wParam, lParam);
        }
    }
}
