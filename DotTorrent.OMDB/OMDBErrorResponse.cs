using DotTorrent.OMDB.Enums;

namespace DotTorrent.OMDB
{
  public class OMDBErrorResponse : OMDBResponse
  {
    public string Error { get; set; }

    public OMDBErrorResponse(string error)
    {
      Error = error;
    }

    public ResponseErrorType ErrorType
    {
      get
      {
        if (Error == null)
          return ResponseErrorType.None;

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
