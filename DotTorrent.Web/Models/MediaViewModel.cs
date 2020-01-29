using DotTorrent.Web.Services.Responses;
using System.Collections.Generic;

namespace DotTorrent.Web.Models
{
  public class MediaViewModel
  {
    public string Title { get; set; }

    public int? ReleaseYear { get; set; }

    public string IMDBId { get; set; }

    public string Plot { get; set; }

    public IEnumerable<string> Actors { get; set; }

    public string PosterUrl { get; set; }

    public MediaViewModel(MediaResponse resp)
    {
      Title = resp.Title;
      ReleaseYear = resp.ReleaseYear;
      IMDBId = resp.IMDBId;
      Plot = resp.Plot;
      Actors = resp.Actors;
      PosterUrl = resp.PosterUrl;
    }
  }
}
