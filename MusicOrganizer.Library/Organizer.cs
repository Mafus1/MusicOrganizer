using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganizer.Library
{
    public class Organizer
    {
        /// <summary>
        /// List of supported file extension. e.g: ".mp3"
        /// </summary>
        private static string[] MusicFileExtenstions
        {
            get
            {
                return new string[] { ".aac", ".aif", ".flac", ".mp3", ".wav" };
            }
        }

        public static MusicAlbum GetMusicAlbum(DirectoryInfo folder)
        {
            return new MusicAlbum(GetMusicFiles(folder));
        }

        /// <summary>
        /// Get list of music files in specified folder. 
        /// Trys to sort the files, if they have a specific file tag.
        /// If they dont have a tag, we try to sort them using the file name.
        /// </summary>
        /// <returns></returns>
        private static List<MusicFile> GetMusicFiles(DirectoryInfo folder)
        {
            // Check that the folder exists
            if (!folder.Exists)
            {
                throw new ArgumentException("Folder does not exist");
            }

            // Check the folder for content
            if (folder.GetFiles().Count() < 1)
            {
                throw new ArgumentException("Folder does not contain any files");
            }

            // Check for any files with extensions like MusicFileExtensions
            if(!folder.GetFiles().Where(x => MusicFileExtenstions.Contains(x.Extension)).Any())
            {
                throw new ArgumentException("Folder does not contain any music files");
            }

            // List with music files
            var musicFiles = new List<MusicFile>();

            var files = folder.GetFiles().Where(x => MusicFileExtenstions.Contains(x.Extension));
            foreach(var file in files)
            {
                musicFiles.Add(new MusicFile(file));
            }

            // Lets guess a track number value for those
            // files that dont have a tag value
            int index = musicFiles.Where(x => x.TagTrack != 0)
                .OrderBy(x => x.TagTrack).Select(x => x.TagTrack).LastOrDefault();

            if(index == 0)
            {
                index = 1;
            }
            else
            {
                index += 1;
            }

            foreach(var musicFile in musicFiles.Where(x => x.TagTrack == 0).OrderBy(x => x.FileName))
            {
                musicFile.TagTrack = index;
                index++;
            }

            return musicFiles;
        }
    }
}
