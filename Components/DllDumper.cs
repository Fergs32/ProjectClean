using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Linq;
using ProjectClean.Utils;

namespace ProjectClean.Components
{
    public class DllDumper
    {
        public static List<string> Flagged_DLLs = new List<string>().ToList<string>();
        private static string Dir = Directory.GetCurrentDirectory() + "\\Programs\\listdll.exe";

        private static void ExecuteCommand(String command)


        {
            Process p = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/c " + command; // cmd.exe spesific implementation
            p.StartInfo = startInfo;
            p.StartInfo.Verb = "runas";
            p.Start();
            p.WaitForExit();
        }

        public static void Dump(int PID, string process_name)
        {
            ExecuteCommand(Dir + $" {PID} > dumped_dlls.txt");
            foreach (string line in File.ReadAllLines("dumped_dlls.txt"))
            {
                if (ContainsAny(line, "System32", "Command line", "javaw.exe pid", @"C:\WINDOWS\SYSTEM32\", @"C:\WINDOWS\system32\") == true)
                {
                    Flagged_DLLs.Add(line);
                }
            }
            DllHelper.UnloadModules(process_name);
            foreach(string line in DllHelper.FlaggedDlls)
            {
                Console.WriteLine(line, Color.Red);
            }
        }

        public static bool ContainsAny(string line, params string[] items)
        {
            foreach (string item in items)
            {
                if (line.Contains(item))
                    return false;
            }
            return true;
        }
    }
}