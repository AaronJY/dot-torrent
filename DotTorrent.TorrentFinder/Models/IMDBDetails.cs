using System;
using System.Collections.Generic;

namespace DotTorrent.TorrentFinder.Models
{
    public class IMDBDetails
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public double Rating { get; set; }

        public DateTime ReleaseDate { get; set; }

        public IEnumerable<string> Directors { get; set; }

        public IEnumerable<string> Starring { get; set; }

        public string PosterUrl { get; set; }

        public int RuntimeMinutes { get; set; }
    }
}
