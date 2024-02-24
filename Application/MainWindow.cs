using Spectre.Console;
using System;
using System.IO;
using System.Text;
using Color = System.Drawing.Color;

namespace ProjectClean.Application
{
    public class MainWindow
    {
        public static void InitUserInterface()
        {
            try
            {
                AsciiArt();
                DisplayWorkingDirectory();
                ProgrammingStatistics();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void AsciiArt()
        {
            var BloodDrip = new string[]
            {
                "                                               ⣿⢦",
                "                                                 ⢦",
                "                                                 ⢦"
            };
            var Ascii = new string[]
            {
                @"
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⠾⠓⠂⠉⠛⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣰⢟⣡⣄⣤⠤⠤⣈⣿⣆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣴⣣⡿⠛⠉⠀⠀⠀⣀⣈⣙⣻⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⠞⠉⢁⠀⠀⠀⣀⡴⣿⢻⣿⣿⣿⣷⡝⣦⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡾⠁⠀⢠⠋⠀⣠⣾⣯⡆⠀⣾⡇⠀⢸⣿⣿⢸⡇        ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⡇⢀⢀⡏⠀⣼⡋⢿⣽⣿⣤⢿⣧⣾⢿⣿⡿⡼⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢻⣎⣾⡇⢸⣿⢷⣤⣭⠉⠿⠿⢯⣁⣸⣿⣿⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⣠⡴⠶⠶⠿⣯⡿⡼⣿⡜⠛⣿⣤⣤⣤⣸⣿⣿⣿⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⡤⣤⣤⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⣠⣾⣡⣾⣆⣸⠀⠬⣿⡄⠹⣿⡆⢿⣙⣋⣩⣿⣿⡿⢿⠇⠀⠀⠀⠀⠀⠀⠀⠀⢸⢋⣴⠛⣧⣿⡀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⢀⣠⠶⠋⠙⠛⠋⠉⠉⠻⡆⡆⠙⢿⣆⠈⠛⢛⣽⣿⣿⢿⣍⣴⣿⣀⠀⠀⠀⠀⠀⠀⠀⠀⢘⣿⣿⣿⠛⢧⡙⣄⠀⠀⠀⠀⠀⠀⠀
⠀⣾⡿⠂⠀⠀⠐⠒⠢⢤⣠⣹⡇⣦⠀⢻⡷⠚⣿⣿⠃⠁⠀⠀⠀⠀⣿⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⠟⢷⡀⠀⠑⠘⢦⠀⠀⠀⠀⠀⠀
⢀⡟⠀⡴⣀⣠⣤⣄⣀⣘⠻⣿⣧⣸⡆⠀⠉⠐⢻⠃⣠⡞⣀⡴⠀⠀⣿⡇⠀⠀⠀⠀⡠⠊⣹⠟⠁⠀⠀⠻⣄⢀⠀⠈⢧⠀⠀⠀⠀⠀
⣼⢠⡎⢀⣵⣾⠟⠋⠁⠀⠦⣼⣿⣿⣧⡾⠀⠀⠀⠘⣿⣿⠋⠀⠀⠀⣟⢻⡀⢀⣠⣾⣿⡿⠃⠀⠀⠀⠀⠀⠙⢿⡿⠁⠈⣧⠀⠀⠀⠀
⢹⡟⠀⠈⡿⠁⢀⡾⠋⠀⠤⣼⣿⣿⠿⠧⠤⠤⠤⢤⣿⣅⠀⠀⢀⣠⣬⣤⠟⠻⣿⡿⠋⣀⣤⣤⣤⣤⡄⠀⠀⠘⢷⡀⢀⠘⣧⠀⠀⠀
⢸⠃⠀⢰⡇⠀⡼⠁⠀⢀⡾⠋⠁⢀⣤⡠⠖⠋⢀⣤⣾⣿⣿⣿⢿⣿⣿⣳⣿⣿⣿⣷⢿⣿⣿⣿⣿⣗⣿⠀⠀⠀⠈⠿⣿⠀⠘⡆⠀⠀
⣼⠀⢀⣼⠀⠀⠁⠀⢠⡟⠁⠀⢠⡿⠉⠀⣠⣴⠿⠟⣛⣭⠿⠛⣻⣿⣿⣿⣿⣿⣿⡇⢸⣧⣽⣿⣿⣬⡇⠀⠀⠀⠀⠀⢧⠀⠀⢹⡄⠀
⠹⣦⣼⡆⠀⠀⠀⢀⠟⠀⠀⡄⣼⠁⠀⣰⣿⣥⣶⠛⠁⠀⢠⡞⣹⡿⠿⣿⣇⢹⣿⣇⠘⣿⣿⣿⠋⠉⠀⠀⠀⠀⠀⠀⠈⢧⠀⠈⣧⠀
⠀⠀⠙⣷⡀⠀⠀⠀⠀⠀⠀⢷⡏⠀⠀⣿⠻⣿⡏⠀⢀⣴⣿⠟⠹⣧⠀⠈⠻⣦⡹⢿⡄⢿⠿⢿⡀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢇⠀⢹⡄
⠀⠀⠀⠈⢷⡀⠀⠀⠀⠀⡀⠈⠻⠤⠀⢿⣿⡇⣧⣴⣿⠟⠁⠀⠀⢿⣆⠀⠀⠈⢳⡄⠁⢸⣶⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⡆⢸⡇
⠀⠀⠀⠀⠀⠙⠲⢤⣄⣀⠈⠓⢦⡀⠀⠸⣿⠀⣿⡟⠁⠀⠀⠀⠀⠘⣿⣦⠀⠀⠀⠹⣄⣀⠻⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢹⠈⣧
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠉⠻⡶⢿⡄⠀⢹⢀⡏⠠⣼⣦⠀⠀⠀⠀⠈⠈⠳⣄⠀⠀⠙⠿⠿⠟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⣿⣿
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡼⢛⣿⣄⢀⡼⠁⠀⠙⢿⡻⣦⠀⢀⡀⠀⠀⠈⠓⢦⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢻⡏
⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⠞⣩⡶⠋⠙⠿⣿⡁⠤⣤⣀⣀⡁⠀⠉⠀⠈⠑⠢⢄⣀⡀⠈⠙⠲⢶⣤⣤⠴⠆⠀⠀⠀⠀⠀⠀⠀ ⠘⡇"
            };
            Console.OutputEncoding = Encoding.UTF8;
            foreach (string line in Ascii)
            {
                Console.WriteLine(line, Color.Coral);
            }
            foreach (string line in BloodDrip)
            {
                Console.WriteLine(line, Color.Red);
            }
        }

        private static string WorkingDirectory = Directory.GetCurrentDirectory();

        private static void DisplayWorkingDirectory()
        {
            var path = new TextPath(WorkingDirectory);
            path.RootStyle = new Style(foreground: Spectre.Console.Color.Red);
            path.SeparatorStyle = new Style(foreground: Spectre.Console.Color.Green);
            path.StemStyle = new Style(foreground: Spectre.Console.Color.Blue);
            path.LeafStyle = new Style(foreground: Spectre.Console.Color.Yellow);

            AnsiConsole.Write(path);
        }

        private static void ProgrammingStatistics()
        {
            AnsiConsole.Write(new BreakdownChart()
            .ShowPercentage()
            .Width(30)
            .AddItem("C#", 100, Spectre.Console.Color.LightCoral));
        }

        public static string UserNavigation()
        {
            var ClientOption = "";
            try
            {
                var UserClient = AnsiConsole.Prompt(
                new SelectionPrompt<string>().Title("[springgreen1]Choose your[/] [white]client[/] [springgreen1]option below[/]").PageSize(5).AddChoices(new[] {
                "Cosmic Client", "Lunar Client", "Minecraft (Forge)",
                "Badlion Client", "Feather Client",}));

                ClientOption = UserClient;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[!] " + ex);
            }
            return ClientOption;
        }
    }
}