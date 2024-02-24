using ProjectClean.Application;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using ProjectClean.Components.FileSystem;
namespace ProjectClean.Components
{
    public class Proccesses
    {
        /*
        public static void Get(string name)
        {
            Thread.Sleep(1500);
            try
            {
                var processes = Process.GetProcessesByName(name);
                var length = processes.Length;
                if (length <= 0)
                {
                    Console.WriteLine("No Minecraft Detected.", Color.Red);
                    Thread.Sleep(-1);
                }
                foreach (var p in processes)
                {
                    if (p.ProcessName == name)
                    {
                        Console.WriteLine(p.PrivateMemorySize64);
                        Console.WriteLine(p.PrivateMemorySize);
                        Console.WriteLine(p.PeakVirtualMemorySize64);
                        Console.ReadLine();
                        Colorful.Console.Write("[!] Minecraft found with PID: ", Color.Coral); Colorful.Console.Write($"{p.Id} | Executed: {p.StartTime}", Color.Green);
                        DPS.Get();
                        Recent.Get();
                        StringDumper.Dump(p.Id);
                        RegistryDumper.Dump(p);
                        USBDevice.Dump(p);
                        DllDumper.Dump(p.Id, p.ProcessName);
                    }
                    ConsoleTitle.PID_Found++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[!] " + ex);
            }
        }
        */
        public static void Get2(string name)
        {
            EventLogger.GetEvents();
            Console.ReadLine();
            Thread.Sleep(1500);
            try
            {
                var processes = Process.GetProcessesByName(name);
                var length = processes.Length;
                if (length <= 0)
                {
                    Console.WriteLine("No Minecraft Detected.", Color.Red);
                    Thread.Sleep(-1);
                }
                Process maxMemoryProcess = null;
                long maxMemoryUsage = 0;

                foreach (var p in processes)
                {
                    if (p.ProcessName == name)
                    {
                        long memoryUsage = p.PrivateMemorySize64;

                        if (memoryUsage > maxMemoryUsage)
                        {
                            maxMemoryUsage = memoryUsage;
                            maxMemoryProcess = p;
                        }
                    }
                    ConsoleTitle.PID_Found++;
                }
                if (maxMemoryProcess != null)
                {
                    Console.Write("[!] Minecraft found with PID: ", Color.Coral);
                    Console.Write($"{maxMemoryProcess.Id} | Executed: {maxMemoryProcess.StartTime}", Color.Green);
                    DPS.Get();
                    Recent.Get();
                    StringDumper.Dump(maxMemoryProcess.Id);
                    RegistryDumper.Dump(maxMemoryProcess);
                    USBDevice.Dump(maxMemoryProcess);
                    DllDumper.Dump(maxMemoryProcess.Id, maxMemoryProcess.ProcessName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[!] " + ex);
            }
        }
    }
}