using System.Diagnostics;
using System.Linq;
using System.IO;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ProjectClean.Utils
{
    internal class DllHelper
    {
        public static List<string> FlaggedDlls = new List<string>().ToList<string>();

        public static void UnloadModules(string process_name)
        {
            Process process = Process.GetProcessesByName(process_name).FirstOrDefault();
            if (process != null)
            {
                foreach (ProcessModule module in process.Modules)
                {
                    try
                    {
                        FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(module.FileName);
                        if (string.IsNullOrEmpty(versionInfo.FileDescription))
                        {   
                            GetImportFunctions(module.FileName, process);
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine("Module: {0} was MOVED or DELETED, file path: {1}", module.ModuleName, module.FileName, Color.Red);
                    }
                }
            }
        }
        private static void GetImportFunctions(string FilePath, Process p)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = Directory.GetCurrentDirectory() + "\\Programs\\dumpbin.exe";
                process.StartInfo.Arguments = $"/IMPORTS \"{FilePath}\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\output.txt", output);
                process.WaitForExit();

                int ImportAlerts = CheckDllImports();
                if (ImportAlerts > 10) // This was discussed with many people, I felt like 10 alerts was enough to say the dll was "suspicious" not in a cheating but generally about it
                {
                    FlaggedDlls.Add($"[CAUTION] Suspicious Handle injected in {p.ProcessName} | File: {FilePath} | Alerts: {ImportAlerts}");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"The file {FilePath} was DELETED or MOVED | CAUTION", Color.Red);
            }
        }
        private static int CheckDllImports()
        {
            UInt16 counter = 0;
            List<string> dll_scanned_file = File.ReadAllLines("output.txt").ToList<string>();
            
            string[] SuspiciousImports = new string[]
            {
               "CreateProcess",
               "GetProcAddress",
               "CreateRemoteThread",
               "LoadLibrary",
               "VirtualAllocEx",
               "WriteProcessMemory",
               "ReadProcessMemory",
               "RtlVirtualUnwind",
               "GetAsyncKeyState",
               "FindWindowW",
               "GetKeyState",
               "SetConsoleCursorPosition",
               "SetConsoleScreenBufferSize",
               "AdjustTokenPrivileges",
               "RtlLookupFunctionEntry",
               "RtlCaptureContext",
               "OpenProcessToken",
               "LookupPrivilegeValueW",
            };
            // Most commonly used functions within dll injections (not random ones dw)
            for (int x = 0; x < SuspiciousImports.Count(); x++)
            {
                var matchingvalues = dll_scanned_file.Where(stringToCheck => stringToCheck.Contains(SuspiciousImports[x])).ToList<string>();
                if (matchingvalues.Count > 0)
                {
                    foreach (string line in matchingvalues)
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }
    }
}
