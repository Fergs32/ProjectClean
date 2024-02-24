using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace ProjectClean.Components
{
    public class APIHookDetection
    {
        const int PROCESS_ALL_ACCESS = 0x1F0FFF; // Adjust the access rights as needed

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, uint nSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        static void HnadleProcess(Process p)
        {
            IntPtr processHandle = OpenProcess(PROCESS_ALL_ACCESS, false, p.Id);

            if (processHandle == IntPtr.Zero)
            {
                Console.WriteLine("Failed to open process.");
                return;
            }

            IntPtr baseAddress = IntPtr.Zero; // Set this to the memory address you want to start reading from
            int bufferSize = 1024; // Set the buffer size according to your needs
            byte[] buffer = new byte[bufferSize];
            int bytesRead;

            // Read memory from the process
            if (ReadProcessMemory(processHandle, baseAddress, buffer, (uint)bufferSize, out bytesRead))
            {
                // Perform CRC or other operations on the buffer as needed
                // ...

                Console.WriteLine($"Read {bytesRead} bytes from process memory.");
            }
            else
            {
                Console.WriteLine("Failed to read process memory.");
            }

            // Close the handle to the process
            CloseHandle(processHandle);
        }
    }
}
