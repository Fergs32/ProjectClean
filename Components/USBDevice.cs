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
    //https://paste.gg/p/anonymous/49108d28a151430aa44d66bcc9d7fb7d/files/64c792474ad9447ab03c8ee66736ca3c/raw
    public class USBDevice
    {
        public static List<string> Alted_USBs = new List<string>();
        private static List<string> USB_Devices_Extracted = new List<string>().ToList<string>();
        private static string Dir = Directory.GetCurrentDirectory() + "\\Programs\\USBDeview.exe";

        private static void ExecuteCommand(String command)
        {
            Process p = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/c " + command; // cmd.exe spesific implementation
            p.StartInfo = startInfo;
            p.Start();
            p.WaitForExit();
        }

        public static void Dump(Process p)
        {
            Console.Write("[!] Dumping USB Devices: ", Color.Coral);
            ExecuteCommand(Dir + " /stext dumped_usbs.txt");
            ExtractProgramsAndDates(p);
        }

        private static void ExtractProgramsAndDates(Process p)
        {
            string device = "";
            string desc = "";
            string connected = "";
            List<string> USBList = File.ReadAllLines("dumped_usbs.txt").ToList<string>();

            foreach (string line in USBList)
            {
                if (line.Contains("Device Name"))
                {
                    device = line.Replace("       : ", ": ");
                }
                else if (line.Contains("Description"))
                {
                    desc = line.Replace("       : ", ": ");
                }
                else if (line.Contains("Connected"))
                {
                    connected = line.Replace("         : ", ": ");
                }
                else if (line.Contains("Registry Time 1"))
                {
                    string reg_time = line.Replace("   : ", ": ");
                    string formatted_string = string.Format("{0} | {1} | {2} || {3}", device, desc, connected, reg_time);
                    USB_Devices_Extracted.Add(formatted_string);
                }
            }
            Thread.Sleep(1000);
            CompareDateTimes(p);
        }

        private static void CompareDateTimes(Process p)
        {
            CultureInfo ci = CultureInfo.InstalledUICulture;
            List<string> no_dupes = USB_Devices_Extracted.Distinct<string>().ToList<string>();
            for (int j = 0; j < no_dupes.Count; j++)
            {
                string new_line = no_dupes[j].Split("||")[1].Replace(" Registry Time 1: ", "");
                if (!string.IsNullOrEmpty(new_line))
                {
                    if (DateTime.Parse(new_line, new CultureInfo(ci.Name)) >= DateTime.Parse(p.StartTime.ToString(), new CultureInfo(ci.Name)))
                    {
                        Alted_USBs.Add(no_dupes[j]);
                        File.AppendAllText("altered_usbs.txt", no_dupes[j] + Environment.NewLine);
                    }
                }
            }

            Console.Write("Completed\n", Color.Green); File.Delete("dumped_usbs.txt");
        }
    }
}