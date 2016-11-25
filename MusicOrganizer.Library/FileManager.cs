using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganizer.Library
{
    public class FileManager
    {
        public static string[] MusicFileExtenstions { get { return new string[] { ".aac", ".aif", ".flac", ".mp3", ".wav" }; } }

        public static TagLib.File GetFile(string path)
        {
            var fileInfo = new FileInfo(path);

            if (!fileInfo.Exists)
                return null;

            var file = TagLib.File.Create(fileInfo.FullName);

            return file;
        }

        public static TagLib.File GetFile(FileInfo fileInfo)
        {
            if (!fileInfo.Exists)
                return null;

            var file = TagLib.File.Create(fileInfo.FullName);

            return file;
        }

    }
}
