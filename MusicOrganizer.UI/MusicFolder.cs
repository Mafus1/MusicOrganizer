using MusicOrganizer.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicOrganizer.UI
{
    public class MusicFolder
    {
        public DirectoryInfo Folder { get; set; }
        public MusicAlbum MusicAlbum { get; set; }

        public MusicFolder(DirectoryInfo folder, MusicAlbum musicAlbum)
        {
            Folder = folder;
            MusicAlbum = musicAlbum;
        }

        public void List()
        {
            Console.WriteLine("Album: " + MusicAlbum.AlbumTitle);
            Console.WriteLine("Artist: " + MusicAlbum.AlbumArtist);
            Console.WriteLine();
            Console.WriteLine("Tr:".PadRight(5) + "Title:".PadRight(30) + "File name:");
            Console.WriteLine("");
            foreach(var musicFile in MusicAlbum.MusicFiles)
            {
                Console.WriteLine(musicFile.TagTrack.ToString().PadRight(5) + musicFile.TagTitle.PadRight(30) + musicFile.FileName);
            }
        }

        public void SwapTracks(int track1, int track2)
        {
            var musicFile1 = MusicAlbum.MusicFiles.Where(x => x.TagTrack == track1).FirstOrDefault();
            var musicFile2 = MusicAlbum.MusicFiles.Where(x => x.TagTrack == track2).FirstOrDefault();
            if (musicFile1 == null || musicFile2 == null)
            {
                throw new System.ArgumentOutOfRangeException("Track number out of range");
            }

            musicFile1.TagTrack = track2;
            musicFile2.TagTrack = track1;
        }

        public void ChangeName(int trackNumber)
        {
            var musicFile = MusicAlbum.MusicFiles.Where(x => x.TagTrack == trackNumber).FirstOrDefault();
            if (musicFile == null)
            {
                throw new System.ArgumentOutOfRangeException("Track number out of range");
            }


            Console.Write("Name: ");
            SendKeys.SendWait(musicFile.TagTitle);
            var newTitle = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newTitle))
            {
                musicFile.TagTitle = newTitle;
            }
        }

        public void ChangeAlbum()
        {
            Console.Write("Album: ");
            SendKeys.SendWait(MusicAlbum.AlbumTitle);
            var newAlbumTitle = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newAlbumTitle))
            {
                MusicAlbum.AlbumTitle = newAlbumTitle;
            }
        }

        public void ChangeArtist()
        {
            Console.Write("Artist: ");
            SendKeys.SendWait(MusicAlbum.AlbumArtist);
            var newAlbumArtist = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newAlbumArtist))
            {
                MusicAlbum.AlbumArtist = newAlbumArtist;
            }
        }

        public void Save()
        {
            Console.WriteLine("Are you sure you want to save? (Y/n)");
            var keyInfo = Console.ReadKey(intercept: true);
            if (keyInfo.Key == ConsoleKey.Y)
            {
                MusicAlbum.SaveTags();
                Console.WriteLine("Changes successfully saved!");
            }
        }
    }
}
