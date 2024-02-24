using ProjectClean.Application;
using ProjectClean.Components;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace ProjectClean
{
    /*
     * 
     * Task List;
     * 
     *  Add more searching modules (Recent, temp, dps, pcavsec)
     * 
     * 
     */
    class Program
    {
        public static List<string> search_strings = File.ReadAllLines("strings/strings.txt").ToList<string>();
        private static string Client;

        static void Main()
        {
            MainWindow.InitUserInterface();
            do
            {
                Client = MainWindow.UserNavigation();
                switch(Client)
                {
                    case "Cosmic Client":
                        Proccesses.Get2("java");
                        break;
                    case "Lunar Client":
                        Proccesses.Get2("java");
                        break;
                    case "Minecraft (Forge)":
                        Proccesses.Get2("javaw");
                        break;
                    case "Badlion Client":
                        Proccesses.Get2("javaw");
                        break;
                    case "Feather Client":
                        Proccesses.Get2("javaw");
                        break;                       
                }
            }
            while (string.IsNullOrEmpty(Client));
        }
    }
}
