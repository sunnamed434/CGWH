using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CGWH.Core
{
    internal class Cheat
    {
        internal static Memory Memory;

        internal static int ModuleAddress;



        internal static bool TryFindProcess()
        {
            try
            {
                Process[] processes = Process.GetProcesses();
                Process process = null;

                for (int i = 0; i < processes.Length; i++)
                {
                    if (processes[i].ProcessName == Information.PROCESS_NAME)
                    {
                        process = processes[i];
                        break;
                    }
                }

                Utilities.Debug.Log(">> TryFindProcess()");
                Utilities.Debug.Log($"[DEBUG:1] {(process?.ProcessName ?? "NULL")} P: ({string.Join(" , ", processes.Select(p => p.ProcessName))})");
                if (process != null)
                {
                    Memory = new Memory(Information.PROCESS_NAME);
                    string[] array = new string[process.Modules.Count];
                    for (int i = 0; i < process.Modules.Count; i++)
                    {
                        array[i] = string.Format("{0}:{1}", process.Modules[i].ModuleName, process.Modules[i].FileName);
                        if (process.Modules[i].ModuleName == Information.MODULE_DLL_NAME)
                        {
                            ModuleAddress = (int)process.Modules[i].BaseAddress;
                            return true;
                        }
                    }
                    Utilities.Debug.Log($"[DEBUG:2] {string.Join(" , ", array)}");
                }
            }
            catch { }
            return false;
        }



        internal static bool TryCheckValidVersion()
        {
            string steamPath = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Valve\\Steam", "InstallPath", null).ToString();

            string infPath = steamPath + "\\steamapps\\common\\Counter-Strike Global Offensive\\csgo\\steam.inf";

            Utilities.Debug.Log(">> IsGetProcess()");
            Utilities.Debug.Log($"[DEBUG:1] {infPath}");

            if (File.Exists(infPath))
            {
                string infText = File.ReadAllText(infPath);
                if (infText.Contains(string.Concat(Information.VERSION_DATE_TITLE, Information.VERSION_DATE)) && infText.Contains(string.Concat(Information.VERSION_TIME_TITLE, Information.VERSION_TIME)))
                    return true;
            }

            return false;
        }
    }
}
