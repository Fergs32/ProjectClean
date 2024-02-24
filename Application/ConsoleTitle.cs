using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace ProjectClean.Application
{
    public class ConsoleTitle
    {
        public static int PID_Found;

        #region ConsoleTitle
        public static void Set()
        {
            while (true)
            {
                try
                {
                    Console.Title = string.Format("Proccess's Found: {0}", PID_Found);
                    Thread.Sleep(2000);
                }
                catch { }
            }
        }
        #endregion
    }
}
