using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectClean.Components.FileSystem
{
    internal class Recent
    {
        public static List<string> RecentFolder = new List<string>().ToList<string>();
        public static void Get()
        {
            string recentFolder = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
            string[] files = Directory.GetFiles(recentFolder);
            DateTime threshold = DateTime.Now.AddDays(-1);
            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);
                if (info.LastWriteTime >= threshold)
                {
                    RecentFolder.Add(file + " | " + info);
                }
            }
        }
    }
}

