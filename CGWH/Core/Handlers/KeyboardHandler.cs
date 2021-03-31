using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CGWH.Core.Handlers
{
    internal sealed class KeyboardHandler
    {
        internal const int KEYEVENTF_EXTENDEDKEY = 0x0001;

        internal const int KEYEVENTF_KEYUP = 0x0002;



        [DllImport("User32.dll")]
        internal static extern short GetAsyncKeyState(Int32 vKey);



        internal static bool GetKeyDown(Keys key) => 
            GetAsyncKeyState((int)key) != 0;
    }
}
