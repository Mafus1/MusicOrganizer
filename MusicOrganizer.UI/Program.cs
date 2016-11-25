using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganizer.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Check the album path argument
            if (args.Count() == 0 || args[0] == "")
            {
                Console.WriteLine("Please specify album path! (First argument)");
                return;
            }

            // Check weather the folder exists
            var albumFolder = new DirectoryInfo(args[0]);

            if (!albumFolder.Exists)
            {
                Console.WriteLine("Folder does not exist");
                return;
            }

            // Check the folder for content
            if (albumFolder.GetFiles().Count() < 1)
            {
                Console.WriteLine("Folder is empty");
                return;
            }

            // Try to get the artist from a music file
            var musicFile = Library.FileManager.GetFile(albumFolder.GetFiles().Where(x => Library.FileManager.MusicFileExtenstions.Contains(x.Extension)).First());
            if (musicFile == null)
            {
                Console.WriteLine("Folder does not containt any supported music files");
                //Console.WriteLine("Supported files: [", Library.FileManager.MusicFileExtenstions.);
                return;
            }

            var artist = !string.IsNullOrWhiteSpace(musicFile.Tag.JoinedAlbumArtists) ? musicFile.Tag.JoinedAlbumArtists : musicFile.Tag.JoinedArtists;
            if (string.IsNullOrWhiteSpace(artist))
            {
                artist = albumFolder.Parent.Name;
            }

            // Ask if artist is correct
            Console.WriteLine("Is this artist correct? (Y/N)");
            Console.WriteLine("\"" + artist + "\"");

            // Check answer is Yes or No
            var answer = Console.ReadKey(intercept: true);
            while (answer.Key != ConsoleKey.Y && answer.Key != ConsoleKey.N)
            {
                Console.WriteLine("Please answer with Yes (= Y) or No (= N)");
                answer = Console.ReadKey(intercept: true);
            }

            // Let the user edit the artist
            if (answer.Key == ConsoleKey.N)
            {
                Console.WriteLine("Define the album artist, then press enter");
                Console.Write("Artist name: ");
                artist = Console.ReadLine().Trim();
            }

            Console.WriteLine(artist);
            Console.ReadKey();
            //foreach(var file in albumFolder.GetFiles())
            //{

            //}



            //var musicFile = Library.FileManager.GetFile(@"C:\Users\Fabian Dev\Downloads\Rage Against The Machine\1994 - Rage Against The Machine\02 - Killing In The Name Of.mp3");

            //if(musicFile == null)
            //{
            //    Console.WriteLine("Please specify album path! (First argument)");
            //    return;
            //}

            //Console.WriteLine(musicFile.Tag.JoinedArtists);
            //Console.WriteLine(musicFile.Tag.JoinedAlbumArtists);
            //Console.WriteLine(musicFile.Tag.Album);
            //Console.WriteLine(musicFile.Tag.Title);
            //Console.WriteLine(musicFile.Tag.Track);
            //Console.WriteLine(musicFile.Tag.TrackCount);
            //Console.ReadKey();
        }
    }
}
