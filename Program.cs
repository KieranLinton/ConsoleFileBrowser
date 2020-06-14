
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
            DirectoryInfo Root = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Home");
            DirectoryInfo SelectedDirectory;
            List<FileSystemInfo> AvailableItems = new List<FileSystemInfo>();

            SelectedDirectory = Root;

            NewDir:
            AvailableItems.Clear();
            AvailableItems.AddRange(SelectedDirectory.GetDirectories());
            AvailableItems.AddRange(SelectedDirectory.GetFiles());

           

            Refresh:
            Console.Clear();
            Console.WriteLine($"Current directory: {SelectedDirectory.FullName}");
            var DefaultBC = Console.BackgroundColor;
            var DefaultFC = Console.ForegroundColor;

            for (var i = 0; i < AvailableItems.Count; i++)
            {
                
                if (i == selected)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                
                Console.WriteLine(AvailableItems[i].Name);

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
                if(AvailableItems.Count != 0 )
                    SelectedDirectory = (DirectoryInfo)AvailableItems[selected];
                    selected = 0;
            }
            void GoBack()
            {
                if(SelectedDirectory.FullName != Root.FullName) 
                    SelectedDirectory = SelectedDirectory.Parent;
                    selected = 0;
            }
            void GoUp()
            {
                if (selected > 0)
                    selected--;
            }
            void Godown()
            {
                if (selected < AvailableItems.Count - 1)
                    selected++;
            }
        }
    }
}
