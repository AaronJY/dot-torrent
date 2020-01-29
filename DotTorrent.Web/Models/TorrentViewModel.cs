using DotTorrent.Web.Common;
using DotTorrent.Web.Services.Responses;
using System;

namespace DotTorrent.Web.Models
{
  public class TorrentViewModel
  {
    public string Name { get; set; }

    public ulong SizeInBytes { get; set; }

    public string PrettySize => Conversion.DisplaySizeFromBytes(SizeInBytes);

    public DateTime AddedDate { get; set; }

    public int FileCount { get; set; }

    public string TrackerName { get; set; }

    public string TrackerUrl { get; set; }

    public string IconImageUrl { get; set; }

    public TorrentViewModel(TorrentResponse resp)
    {
      Name = resp.Name;
      SizeInBytes = resp.SizeInBytes;
      AddedDate = resp.AddedDate;
      FileCount = resp.FileCount;
      TrackerName = resp.TrackerName;
      TrackerUrl = resp.TrackerUrl;

      IconImageUrl = "https://via.placeholder.com/64";
    }
  }
}
