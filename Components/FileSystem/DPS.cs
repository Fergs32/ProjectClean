using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Threading;

namespace ProjectClean.Components.FileSystem
{
    internal class DPS
    {
        private static string BatScript = Directory.GetCurrentDirectory() + "\\Programs\\GetDPS.bat";
        private static string StringDir = Directory.GetCurrentDirectory() + "\\Programs\\strings2.exe";
        private static string DumpsDir = Directory.GetCurrentDirectory() + "\\string_dumps\\";


        private static void ExecuteCommand(String command)
        {
            Process p = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Environment.ExpandEnvironmentVariables("%SystemRoot%") + @"\System32\cmd.exe";
            startInfo.Arguments = @"/c " + command; // cmd.exe spesific implementation
            startInfo.Verb = "runas";
            p.StartInfo = startInfo;
            p.Start();
            p.WaitForExit();
        }
        private static void ExecuteBatch()
        {
            var process = new Process();
            var startinfo = new ProcessStartInfo("cmd.exe", $"/C {BatScript}");
            startinfo.RedirectStandardOutput = true;
            startinfo.UseShellExecute = false;
            process.StartInfo = startinfo;
            process.OutputDataReceived += (sender, args) =>
            {
                int count = 1;
                DumpDPS(sender, args, count);
            };
            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
        }

        public static void Get()
        {
            ExecuteBatch();
        }
        public static void DumpDPS(object sendingProcess, DataReceivedEventArgs outLine, int count)
        {
            if (outLine.Data == null)
            {

            }
            else
            {
                switch (count)
                {
                    case 1:
                        ExecuteCommand(StringDir + " -pid " + outLine.Data + $" > {DumpsDir + "dps_log.txt"}");
                        break;
                    case 2:
                        break;

                }
            }
        }
    }
}
