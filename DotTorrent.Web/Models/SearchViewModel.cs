namespace DotTorrent.Web.Models
{
  public class SearchViewModel : IModelError
  {
    public string Query { get; set; }

    public SearchResultViewModel SearchResult { get; set; }

    public string ErrorMessage { get; set; }
  }
}
