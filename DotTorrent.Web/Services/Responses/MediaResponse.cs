using System.Collections.Generic;

namespace DotTorrent.Web.Services.Responses
{
  public class MediaResponse
  {
    public MediaResponse() { }

    public string Title { get; set; }

    public int? ReleaseYear { get; set; }

    public string IMDBId { get; set; }

    public string Plot { get; set; }

    public IEnumerable<string> Actors { get; set; }

    public string PosterUrl { get; set; }
  }
}
