using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ProjectClean.Components
{
    public class MemoryIntegrity
    {
        public static void MemoryIntegrityCheck(Process process, IntPtr address, byte[] originalBytes)
        {
            byte[] currentBytes = new byte[originalBytes.Length];
            ReadProcessMemory(process.Handle, address, currentBytes, currentBytes.Length, out _);

            if (!currentBytes.SequenceEqual(originalBytes))
            {
                Console.WriteLine("Memory modification detected!");
            }
        }

        private static void ReadProcessMemory(IntPtr handle, IntPtr address, byte[] currentBytes, int length, out object _)
        {
            throw new NotImplementedException();
        }
    }
}
