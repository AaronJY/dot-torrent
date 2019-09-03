using DotTorrent.OMDB.Enums;

namespace DotTorrent.OMDB
{
  public class OMDBErrorResponse : OMDBResponse
  {
    public string Error { get; set; }

    public ResponseErrorType ErrorType
    {
      get
      {
        switch (Error)
        {
          case "Incorrect IMDb ID.":
            return ResponseErrorType.IncorrectIMDBId;
          case "Movie not found!":
            return ResponseErrorType.MovieNotFound;

          default:
            return ResponseErrorType.Unknown;
        }

      }
    }
  }
}
