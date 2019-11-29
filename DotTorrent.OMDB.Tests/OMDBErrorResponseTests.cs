using DotTorrent.OMDB.Enums;
using NUnit.Framework;

namespace DotTorrent.OMDB.Tests
{
  public class OMDBErrorResponseTests
  {
    [TestCase(null, ResponseErrorType.None)]
    [TestCase("Incorrect IMDb ID.", ResponseErrorType.IncorrectIMDBId)]
    [TestCase("Movie not found!", ResponseErrorType.MovieNotFound)]
    [TestCase("4397xcz56ohfd7ght786rqgha", ResponseErrorType.Unknown)]
    public void ErrorType_ReturnsCorrectEnumType(string errorMessage, ResponseErrorType expectedErrorType)
    {
      var errorResponse = new OMDBErrorResponse(errorMessage);
      Assert.AreEqual(expectedErrorType, errorResponse.ErrorType);
    }
  }
}
