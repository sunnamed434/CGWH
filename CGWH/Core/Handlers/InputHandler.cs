using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CGWH.Core.Handlers
{
    internal sealed class InputHandler
    {
        internal const int VK_LBUTTON = 0x01;

        internal const int VK_RBUTTON = 0x02;

        internal const int KEYEVENTF_KEYUP = 0x0002;



        [DllImport("user32.dll")]
        internal static extern short GetAsyncKeyState(Int32 vKey);



        internal static bool GetKeyDown(Keys key) => 
            GetAsyncKeyState((int)key) != 0;

        internal static bool GetKeyDown(int key) =>
            GetAsyncKeyState(key) != 0;

        internal static bool GetLeftMouseButtonDown() => 
            GetKeyDown(VK_LBUTTON);

        internal static bool GetRightMouseButtonDown() => 
            GetKeyDown(VK_RBUTTON);
    }
}
