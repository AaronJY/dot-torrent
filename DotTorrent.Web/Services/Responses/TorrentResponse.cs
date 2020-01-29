using RestSharp;
using System;

namespace DotTorrent.Web.Services.Responses
{
    public class TorrentResponse
    {
        public string Name { get; set; }

        public ulong SizeInBytes { get; set; }

        public DateTime AddedDate { get; set; }

        public int FileCount { get; set; }

        public string TrackerName { get; set; }

        public string TrackerUrl { get; set; }
    }
}
