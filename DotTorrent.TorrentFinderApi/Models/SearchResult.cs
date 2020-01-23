using System.Collections.Generic;

namespace DotTorrent.TorrentFinderApi.Models
{
  public class SearchResult
  {
    public MediaResult Media { get; set; }

    public IEnumerable<TorrentResult> Torrents { get; set; }
  }
}
