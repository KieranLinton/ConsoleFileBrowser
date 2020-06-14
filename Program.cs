
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;

namespace FileBrowser
{
    class Program
    {
        static void Main(string[] args)
        {
            int selected = 0;
            String Root = Directory.GetCurrentDirectory() + "\\Home";
            DirectoryInfo SelectedDirectory;
            List<FileSystemInfo> AvailableDirectorys = new List<FileSystemInfo>();

            SelectedDirectory = new DirectoryInfo(Root);

            NewDir:
            AvailableDirectorys.Clear();
            AvailableDirectorys.AddRange(SelectedDirectory.GetDirectories());
            AvailableDirectorys.AddRange(SelectedDirectory.GetFiles());

           

            Refresh:
            Console.Clear();
            Console.WriteLine($"Current directory: {SelectedDirectory.FullName}");
            var DefaultBC = Console.BackgroundColor;
            var DefaultFC = Console.ForegroundColor;

            for (var i = 0; i < AvailableDirectorys.Count; i++)
            {
                
                if (i == selected)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                
                Console.WriteLine(AvailableDirectorys[i].Name);

                Console.BackgroundColor = DefaultBC;
                Console.ForegroundColor = DefaultFC;
            }
            
            ReadKey:

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow: GoUp(); break;
                case ConsoleKey.DownArrow: Godown(); break;
                case ConsoleKey.RightArrow: GoToSelected(); goto NewDir;
                case ConsoleKey.LeftArrow: GoBack(); goto NewDir;
                case ConsoleKey.Enter: GoToSelected(); goto NewDir;
                case ConsoleKey.Backspace: GoBack(); goto NewDir;
                default: goto ReadKey; break;
            }
            
            goto Refresh;

            Console.ReadLine();

            void GoToSelected()
            {
                SelectedDirectory = (DirectoryInfo)AvailableDirectorys[selected];
                selected = 0;
            }
            void GoBack()
            {
                SelectedDirectory = SelectedDirectory.Parent;
                selected = 0;
            }
            void GoUp()
            {
                if (selected > 0)
                {
                    selected--;
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                }
            }
            void Godown()
            {
                if (selected < AvailableDirectorys.Count - 1)
                { 
                    selected++;
                    Console.SetCursorPosition(0, Console.CursorTop + 1);
                }
            }
        }
    }
}
