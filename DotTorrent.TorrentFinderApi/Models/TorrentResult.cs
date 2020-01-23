using DotTorrent.TorrentFinder.Models;
using System;

namespace DotTorrent.TorrentFinderApi.Models
{
  public class TorrentResult
  {
    public string Name { get; set; }
    public ulong SizeInBytes { get; set; }
    public DateTime AddedDate { get; set; }
    public int FileCount { get; set; }

    public TorrentResult() { }

    public TorrentResult(ITorrent torrent)
    {
      Name = torrent.Name;
      SizeInBytes = torrent.SizeInBytes;
      AddedDate = torrent.AddedDate;
      FileCount = torrent.FileCount;
    }
  }
}
