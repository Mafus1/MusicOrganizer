using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganizer.Library
{
    public class MusicFile
    {
        private TagLib.File taglibFile;

        public MusicFile(FileInfo file)
        {
            // set file name
            FileName = file.Name;

            // create TaglibFile from FileInfo
            taglibFile = TagLib.File.Create(file.FullName);

            // guess title from and/or title sort from file name
            if (string.IsNullOrEmpty(TagTitle) || TagTrack == 0)
            {
                // try to set the file name as the title of the track
                // since often file names look like "01-XXXXXXX.mp3"
                // we try to remove the numeric part and things
                // like spaces and dashes
                var startIndex = 0;
                var index = 0;
                var nonAlphabeticCharacters = "1234567890-_* ";
                var numbers = "1234567890";
                var trackNumber = "";
                var fileNameWithoutExt = FileName.Split(new char[] { '.' }).First();
                while (index < 8 && index < fileNameWithoutExt.Length)
                {
                    if (nonAlphabeticCharacters.Contains(fileNameWithoutExt[index]))
                    {
                        // increment start index, if the letter at the current index
                        // is a non alphabetic character
                        startIndex++;

                        // add number to possible title sort string
                        if (numbers.Contains(fileNameWithoutExt[index]))
                        {
                            trackNumber = trackNumber + fileNameWithoutExt[index];
                        }
                    }

                    // indcrement index
                    index++;
                }

                // make best guess at title, if the tag is empty
                if (string.IsNullOrEmpty(TagTitle))
                {
                    TagTitle = fileNameWithoutExt.Substring(startIndex, fileNameWithoutExt.Length - (startIndex));
                }

                // make best guess at track number, if tag is 0
                if (TagTrack == 0 && !string.IsNullOrEmpty(trackNumber))
                {
                    if (int.TryParse(trackNumber, out int parsedTrackNumber))
                    {
                        TagTrack = parsedTrackNumber;
                    }
                }
            }
        }

        /// <summary>
        /// Tag: title of the file
        /// </summary>
        public string TagTitle
        {
            get
            {
                return taglibFile.Tag.Title;
            }
            set
            {
                taglibFile.Tag.Title = value;
            }
        }

        /// <summary>
        /// Tag: track number of the file
        /// </summary>
        public int TagTrack
        {
            get
            {
                return (int)taglibFile.Tag.Track;
            }
            set
            {
                taglibFile.Tag.Track = (uint)value;
            }
        }

        /// <summary>
        /// Tag: Album artist
        /// Get: Fist album artist
        /// Set: Sets array with only one entry
        ///      as album artists and performers
        /// </summary>
        public string TagAlbumArtist
        {
            get
            {
                return taglibFile.Tag.FirstAlbumArtist ?? taglibFile.Tag.FirstPerformer;
            }
            set
            {
                taglibFile.Tag.AlbumArtists = new string[] { value };
                taglibFile.Tag.Performers = new string[] { value };
            }
        }

        public string TagAlbum
        {
            get
            {
                return taglibFile.Tag.Album;
            }
            set
            {
                taglibFile.Tag.Album = value;
            }
        }

        /// <summary>
        /// File name of the file
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Save tags to the file
        /// </summary>
        public void SaveTags()
        {
            taglibFile.Save();
        }
    }
}
