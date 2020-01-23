namespace DotTorrent.Web.Models
{
  public class SearchViewModel : IModelError
  {
    public string Query { get; set; }
    public object SearchResult { get; set; }
    public string ErrorMessage { get; set;}
  }
}
