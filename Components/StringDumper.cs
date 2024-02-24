using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;

namespace ProjectClean.Components
{
    public class StringDumper
    {
        private static string Dir = Directory.GetCurrentDirectory() + "\\Programs\\strings2.exe";
        private static string RuntimeDir = Directory.GetCurrentDirectory() + "\\string_dumps";

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

        public static void Dump(int PID)
        {
            List<string> DetectedStrings = new List<string>();
            Action onCompleted = () =>
            {
                List<string> cheat_strings = File.ReadAllLines("strings\\tagged_strings.txt").ToList<string>();
                List<string> string_dump = File.ReadAllLines("strings.txt").ToList<string>();
                Console.Write("[!] Dumped", Color.Coral); Console.Write($" {string_dump.Count}", Color.Green); Console.Write(" strings for javaw.exe\n", Color.Coral);

                for (int i = 0; i < cheat_strings.Count; i++)
                {
                    var cheat_details = cheat_strings[i].Split(" | ", StringSplitOptions.None);
                    var matchingvalues = string_dump.Where(stringToCheck => stringToCheck.Contains(cheat_details[0])).ToList<string>();
                    if (matchingvalues.Count > 0)
                    {
                        foreach (string line in matchingvalues)
                        {
                            DetectedStrings.Add(line);
                            Console.WriteLine(cheat_strings[i]);
                        }
                    }
                }

                Console.Write($"Operation completed, found {DetectedStrings.Count} detected strings", Color.Coral); Console.Write(" IN-INSTANCE\n", Color.Green);
            };
            var thread = new Thread(
              () =>
              {
                  try
                  {
                      // So, found out that if we inspect the threads, go to windows and debug via handles we can see common.dll is been handled etc, we can easily
                      // filter this and maybe add warning etc.
                      ExecuteCommand(Dir + "  -l 5" + " -pid " + PID  + "> strings.txt");
                      GetRuntimePID("RuntimeBroker");
                  }
                  finally
                  {
                      onCompleted();
                  }
              });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
        }

        private static void GetRuntimePID(string n)
        {
            int i = 0;
            try
            {
                var processes = Process.GetProcessesByName(n);
                foreach (var p in processes)
                {
                    if (p.ProcessName == n)
                    {
                        if (i < 7)
                        {
                            ExecuteCommand(Dir + $" -l 5 -pid {p.Id} > " + RuntimeDir + $"\\run{i}.txt");
                            i++;
                        }
                        else { }
                    }
                }
                // Transforming into 1 file
                const int chunkSize = 2 * 1024;
                var inputFiles = new[] { "string_dumps\\run0.txt", "string_dumps\\run1.txt", "string_dumps\\run2.txt", "string_dumps\\run3.txt", "string_dumps\\run4.txt", "string_dumps\\run5.txt", "string_dumps\\run6.txt" };
                using (var output = File.Create("string_dumps\\runtime_strings.txt"))
                {
                    foreach (var file in inputFiles)
                    {
                        using (var input = File.OpenRead(file))
                        {
                            var buffer = new byte[chunkSize];
                            int bytesRead;
                            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                output.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }
                File.Delete("string_dumps\\run0.txt"); 
                File.Delete("string_dumps\\run1.txt"); 
                File.Delete("string_dumps\\run2.txt"); 
                File.Delete("string_dumps\\run3.txt");
                File.Delete("string_dumps\\run4.txt"); 
                File.Delete("string_dumps\\run5.txt");
                File.Delete("string_dumps\\run6.txt");
            }
            catch (Exception ex) {  }
        }
    }
}