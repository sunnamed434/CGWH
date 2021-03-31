using System.Runtime.InteropServices;

namespace CGWH.Core.Handlers
{
    internal class MouseHandler
    {
        internal const int MOUSEEVENTF_LEFTDOWN = 2;

        internal const int MOUSEEVENTF_LEFTUP = 4;



        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        internal static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);



        internal static void ImitateClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);

            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
    }
}
