namespace DotTorrent.Web.Services.Responses
{
  public class ErrorResponse : ITorrentFinderResponse
  {
    public bool Successful => false;

    string Error { get; }
  }
}
