using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace ProjectClean.Components
{
    public class RegistryDumper
    {
        private static string Dir = Directory.GetCurrentDirectory() + "\\Programs\\ExecutedProgramsList.exe";

        private static bool ExecuteCommand(String command)
        {
            try
            {
                Process p = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = @"/c " + command; // cmd.exe spesific implementation
                p.StartInfo = startInfo;
                p.Start();
                p.WaitForExit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void Dump(Process p)
        {
            Console.Write("\n[!] Dumping MUICache: ", Color.Coral);
            if (ExecuteCommand(Dir + " /stext muicache.txt"))
            {
                Console.Write("Completed\n", Color.Green);
                ExtractProgramsAndDates(p);
            }
            else
            {
                Console.Write("Failed\n", Color.Red);
            }
        }

        private static void ExtractProgramsAndDates(Process p)
        {
            string unc_string = "";
            List<string> ProgramsDateList = new List<string>().ToList<string>();
            List<string> MuiCacheList = File.ReadAllLines("muicache.txt").ToList<string>();

            foreach (string line in MuiCacheList)
            {
                if (line.Contains("Executed File"))
                {
                    unc_string = line.Replace("     : ", ": ");
                }
                else if (line.Contains("Last Executed On  "))
                {
                    string new_line = line.Replace("Last", " | Last").Replace("  : ", ": ");

                    ProgramsDateList.Add(unc_string + new_line);
                }
            }
            File.AppendAllLines(Directory.GetCurrentDirectory() + "\\executedprograms.txt", ProgramsDateList);
            Console.Write("[!] Dumping recent executed programs: ", Color.Coral);
            Thread.Sleep(1300); // Tested on slower laptop, since the laptop couldn't appled all the lines in time, the function would return an error as it was in process of writing lines
            // found that 1300 was the sweet spot for low end computers / slow laptops which gives them enough time to write and close.
            CompareDateTimes(p);
        }

        private static void CompareDateTimes(Process p)
        {
            List<string> date_file = File.ReadAllLines("executedprograms.txt").ToList<string>();
            for (int j = 0; j < date_file.Count; j++)
            {
                string new_line = date_file[j].Split("|")[1].Replace(" Last Executed On: ", "");
                if (!string.IsNullOrEmpty(new_line))
                {
                    if (DateTime.Parse(new_line, new CultureInfo("pt-BR")) >= DateTime.Parse(p.StartTime.ToString(), new CultureInfo("pt-BR")))
                    {
                        File.AppendAllText("executed_after.txt", date_file[j] + Environment.NewLine);
                    }
                }
            }
            File.Delete("muicache.txt"); File.Delete("executedprograms.txt");
            Console.Write("Completed\n", Color.Green);
        }
    }
}