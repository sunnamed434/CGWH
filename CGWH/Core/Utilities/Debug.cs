using System;
using System.IO;

namespace CGWH.Core.Utilities
{
    internal class Debug
    {
        internal static readonly string Path = "Debug.log";



        internal static void Log(string message)
        {
            using (StreamWriter writer = new StreamWriter(Path, true))
            {
                writer.WriteLine($"[{DateTime.Now}] {message}");
            }
        }
    }
}
