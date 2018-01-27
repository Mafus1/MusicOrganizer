using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganizer.Library
{
    public class MusicAlbum
    {
        List<MusicFile> _musicFiles;
        public List<MusicFile> MusicFiles
        {
            get
            {
                return _musicFiles.OrderBy(x => x.TagTrack).ToList();
            }
            private set
            {
                _musicFiles = value;
            }
        }

        public string AlbumTitle { get; set; }
        public string AlbumArtist { get; set; }

        public MusicAlbum(List<MusicFile> musicFiles)
        {
            MusicFiles = musicFiles;
            AlbumTitle = musicFiles.Where(x => !string.IsNullOrWhiteSpace(x.TagAlbum)).Select(x => x.TagAlbum).FirstOrDefault();
            AlbumArtist = musicFiles.Where(x => !string.IsNullOrWhiteSpace(x.TagAlbumArtist)).Select(x => x.TagAlbumArtist).FirstOrDefault();
        }

        public void SaveTags()
        {
            foreach(var musicFile in _musicFiles)
            {
                // set album name and artist name
                musicFile.TagAlbum = AlbumTitle;
                musicFile.TagAlbumArtist = AlbumArtist;
                musicFile.SaveTags();
            }
        }
    }
}
