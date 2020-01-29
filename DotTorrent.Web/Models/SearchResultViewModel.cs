using DotTorrent.Web.Services.Responses;
using System.Collections.Generic;
using System.Linq;

namespace DotTorrent.Web.Models
{
  public class SearchResultViewModel
  {
    public MediaViewModel Media { get; set; }

    public IEnumerable<TorrentViewModel> Torrents { get; set; }

    public SearchResultViewModel(TitleResponse resp)
    {
      Media = new MediaViewModel(resp.Media);
      Torrents = resp.Torrents.Select(x => new TorrentViewModel(x));
    }
  }
}
