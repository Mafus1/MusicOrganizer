using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicOrganizer.Library;

namespace MusicOrganizer.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Check the album path argument
            if (args.Count() == 0 || args[0] == "")
            {
                Console.WriteLine("Please specify album path!");
                return;
            }

            var folder = new DirectoryInfo(args[0]);
            MusicFolder musicFolder;
            try
            {
                var musicAlbum = Organizer.GetMusicAlbum(folder);
                musicFolder = new MusicFolder(folder, musicAlbum);
            }
            catch(System.ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            musicFolder.List();

            var quit = false;
            while (!quit)
            {
                Console.Write("> ");
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }

                // Split input by space
                var arguments = input.Split(new char[] { ' ' });

                switch (arguments[0])
                {
                    case "h":
                    case "help":
                        PrintHelp();
                        break;

                    case "q":
                    case "quit":
                        quit = true;
                        break;

                    case "save":
                        musicFolder.Save();
                        break;

                    case "list":
                        musicFolder.List();
                        break;

                    case "name":
                        if(arguments.Count() < 1 || !int.TryParse(arguments[1], out int track))
                        {
                            PrintHelp();
                            break;
                        }
                        musicFolder.ChangeName(track);
                        break;

                    case "swap":
                        if (arguments.Count() < 2 || !int.TryParse(arguments[1], out int track1) || !int.TryParse(arguments[2], out int track2))
                        {
                            PrintHelp();
                            break;
                        }
                        musicFolder.SwapTracks(track1, track2);
                        break;

                    case "album":
                        musicFolder.ChangeAlbum();
                        break;

                    case "artist":
                        musicFolder.ChangeArtist();
                        break;
                }
            }

            return;
        }

        private static void PrintHelp()
        {
            Console.WriteLine("morg - Music ORGanizer");
            Console.WriteLine();
            Console.WriteLine("OPTIONS");
            Console.WriteLine("\tlist".PadRight(20) + "List all title plus album and artist.");
            Console.WriteLine("\tquit".PadRight(20) + "Quit application without saving changes to files.");
            Console.WriteLine("\tsave".PadRight(20) + "Save changes to files.");
            Console.WriteLine("\tname [tr]".PadRight(20) + "Edit track name.");
            Console.WriteLine("\tswap [tr1] [tr2]".PadRight(20) + "Swap position of [track1] and [track2].");
            Console.WriteLine("\talbum".PadRight(20) + "Edit album name.");
            Console.WriteLine("\tartist".PadRight(20) + "Edit album artist.");
        }
    }
}
