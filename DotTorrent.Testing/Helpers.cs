using DotTorrent.OMDB;
using System;

namespace DotTorrent.Testing
{
  public static class Helpers
  {
    /// <summary>
    /// Generates a random string
    /// </summary>
    /// <returns></returns>
    public static string GetRandomString()
    {
      return Guid.NewGuid().ToString();
    }

    /// <summary>
    /// Generates a random datetime
    /// </summary>
    /// <returns></returns>
    public static DateTime GetRandomDateTime()
    {
      var to = DateTime.MaxValue;
      var from = DateTime.MinValue;

      var range = to - from;
      var rnd = new Random();
      var randTimeSpan = new TimeSpan((long)(rnd.NextDouble() * range.Ticks));

      return from + randTimeSpan;
    }

    /// <summary>
    /// Generates a random title response
    /// </summary>
    /// <returns></returns>
    public static OMDBTitleResponse GetRandomTitleResponse()
    {
      return new OMDBTitleResponse
      {
        Response = true,
        Actors = GetRandomString(),
        Awards = GetRandomString(),
        BoxOffice = GetRandomString(),
        Country = GetRandomString(),
        Director = GetRandomString(),
        DVD = GetRandomString(),
        Genre = GetRandomString(),
        ImdbID = GetRandomString(),
        ImdbRating = GetRandomString(),
        ImdbVotes = GetRandomString(),
        Language = GetRandomString(),
        Metascore = GetRandomString(),
        Plot = GetRandomString(),
        Poster = GetRandomString(),
        Production = GetRandomString(),
        Rated = GetRandomString(),
        Released = GetRandomDateTime().ToString(),
        Runtime = GetRandomString(),
        Title = GetRandomString(),
        Type = GetRandomString(),
        Website = GetRandomString(),
        Writer = GetRandomString(),
        Year = GetRandomString()
      };
    }
  }
}
