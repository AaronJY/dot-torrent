using System;

namespace DotTorrent.TorrentFinder.Models
{
  public interface ITorrent
  {
      DateTime AddedDate { get; set; }
      int FileCount { get; set; }
      string Name { get; set; }
      ulong SizeInBytes { get; set; }
  }
}