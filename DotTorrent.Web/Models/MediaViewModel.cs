using System.Collections.Generic;

namespace DotTorrent.Web.Models
{
  public class MediaViewModel
  {
    public IEnumerable<MediaItemViewModel> MediaItems { get; set; }
  }
}
