using System.Collections.Generic;

namespace DotTorrent.TorrentFinder.Models
{
    public class MediaTorrent : Torrent
    {
        public IMDBDetails IMDBDetails { get; set; }

        public string MediaName { get; set; }

        public IEnumerable<string> SpokenLanguages { get; set; }
    }
}
