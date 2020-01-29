using System.Collections.Generic;

namespace DotTorrent.Web.Services.Responses
{
  public class TitleResponse : ITorrentFinderResponse
  {
    public IEnumerable<TorrentResponse> Torrents { get; set; }

    public MediaResponse Media { get; set; }

    public bool Successful => true;
  }
}
