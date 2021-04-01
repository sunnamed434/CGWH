using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CGWH.Core.Handlers
{
    internal class WindowHandler
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();


        [DllImport("user32.dll")]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);



        internal static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }

            return null;
        }

        internal static bool TryGetCSGOWindow()
        {
            string result = GetActiveWindowTitle();

            if (result != null && result.Equals(Information.PROCESS_FULLNAME)) return true;

            return false;
        }

        internal static bool TryGetActiveWindowByName(string name)
        {
            string result = string.Empty;

            if ((result = GetActiveWindowTitle()) != null && result.Equals(name)) return true;

            return false;
        }
    }
}
