using DotTorrent.Web.Services.Responses;
using System;

namespace DotTorrent.Web.Models
{
  public class SearchResultViewModel
    {
        public string Name { get; set; }

        public ulong SizeInBytes { get; set; }

        public DateTime AddedDate { get; set; }

        public int FileCount { get; set; }

        public string TrackerName { get; set; }

        public string TrackerUrl { get; set; }

        public SearchResultViewModel(TorrentResponse resp)
        {
          Name = resp.Name;
          SizeInBytes = resp.SizeInBytes;
          AddedDate = resp.AddedDate;
          FileCount = resp.FileCount;
          TrackerName = resp.TrackerName;
          TrackerUrl = resp.TrackerUrl;
        }
    }
}
