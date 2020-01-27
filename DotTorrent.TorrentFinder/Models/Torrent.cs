using System;

namespace DotTorrent.TorrentFinder.Models
{
  public class Torrent : ITorrent
  {
    public string Name { get; set; }

    /// <summary>
    /// Size in bytes
    /// </summary>
    public ulong SizeInBytes { get; set; }

    public DateTime AddedDate { get; set; }

    public int FileCount { get; set; }

    public string TrackerName { get; set; }

    public string TrackerUrl { get; set; }
  }
}
