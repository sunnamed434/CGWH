using CGWH.Core.Handlers;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace CGWH.Core.Input
{
    public class LowLevelKeyboardListener
    {
        private const int WH_KEYBOARD_LL = 13;

        private const int WM_KEYDOWN = 0x0100;

        private const int WM_SYSKEYDOWN = 0x0104;

        private IntPtr _hookID = IntPtr.Zero;



        private LowLevelKeyboardProc _proc { get; }



        internal delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        internal event Action<KeyPressArgs> OnKeyPressed;



        internal LowLevelKeyboardListener()
        {
            _proc = HookCallback;

            HookKeyboard();
        }



        #region DLL-Import`s

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);


        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion



        internal void HookKeyboard()
        {
            _hookID = SetHook(_proc);
        }

        internal void UnHookKeyboard()
        {
            UnhookWindowsHookEx(_hookID);
        }



        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                OnKeyPressed?.Invoke(new KeyPressArgs(KeyInterop.KeyFromVirtualKey(vkCode)));
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
    }
}
