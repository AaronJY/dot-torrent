namespace DotTorrent.Web.Services.Responses
{
  public class ErrorResponse : ITorrentFinderResponse
  {
    public bool Successful { get; }

    string Error { get; }

    public ErrorResponse()
    {
      Successful = false;
    }
  }
}
