using System;

namespace DotTorrent.TorrentFinder.Models
{
    public class Torrent
    {
        public string Name { get; set; }

        /// <summary>
        /// Size in bytes
        /// </summary>
        public ulong Size { get; set; }

        public DateTime AddedDate { get; set; }

        public int FileCount { get; set; }
    }
}
