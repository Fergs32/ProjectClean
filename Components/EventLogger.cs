using System;
using Microsoft.Win32;
using System.Diagnostics;
namespace ProjectClean.Components
{
    internal class EventLogger
    {
        public static void GetEvents()
        {
            EventlogAnalyzer();
        }
        static void EventlogAnalyzer()
        {
            // Set console color for information display
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("[System Scanner] Running checks to detect event log bypasses...");
            Console.ResetColor();

            const string registryPath = "SYSTEM\\CurrentControlSet\\Services\\EventLog\\System";
            const string valueName = "File";
            const string expectedValue = "%SystemRoot%\\System32\\Winevt\\Logs\\System.evtx";

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryPath, false))
            {
                if (key != null)
                {
                    int dataSize = 0;
                    object temp = key.GetValue(valueName);
                    if (temp != null)
                    {
                        dataSize = temp.ToString().Length * 2; // Assuming Unicode (2 bytes per character)
                    }
                    byte[] buffer = new byte[dataSize];
                    int bytesRead = key.GetValue(valueName, buffer) as byte[] != null ? dataSize : 0;

                    if (bytesRead > 0)
                    {
                        Console.WriteLine(buffer);
                        string value = System.Text.Encoding.Unicode.GetString(buffer);

                        if (string.Compare(value, expectedValue, StringComparison.OrdinalIgnoreCase) != 0)
                        {
                            Console.WriteLine("[!] Event log bypass detected. Ban the user.");
                        }
                    }
                }
            }
        }
    }
}
