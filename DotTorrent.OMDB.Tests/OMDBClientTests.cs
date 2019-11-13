using DotTorrent.OMDB.Exceptions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace DotTorrent.OMDB.Tests
{
  public class OMDBClientTests
  {
    [Test]
    public void GetByIMDBId_IfNoApiKey_ThrowsOMDBException()
    {
      var expectedExceptionMessage = "No api key set.";

      var restClientMock = new Mock<IRestClient>();
      var client = GetTestableOMDBClient(restClientMock);

      var exception = Assert.Throws<OMDBException>(() =>
      {
        client.GetByIMDBId(TestHelpers.GetRandomString());
      });

      Assert.AreEqual(expectedExceptionMessage, exception.Message);
    }

    [Test]
    public void GetByTitle_IfNoApiKey_ThrowsOMDBException()
    {
      var expectedExceptionMessage = "No api key set.";

      var restClientMock = new Mock<IRestClient>();
      var client = GetTestableOMDBClient(restClientMock);

      var exception = Assert.Throws<OMDBException>(() =>
      {
        client.GetByTitle(TestHelpers.GetRandomString());
      });

      Assert.AreEqual(expectedExceptionMessage, exception.Message);
    }

    [Test]
    public void GetByIMDBId_IfResponseReturnsError_ThrowsOMDBException()
    {
      var expectedExceptionMessage = "Mock error message";
      var imdbID = TestHelpers.GetRandomString();
      var errorResponseJson = $"{{ \"Error\": \"{expectedExceptionMessage}\" }}";
      var restResponse = new RestResponse
      {
        StatusCode = HttpStatusCode.InternalServerError,
        Content = errorResponseJson
      };

      var restClientMock = new Mock<IRestClient>();

      restClientMock
        .Setup(mock => mock.Execute(It.IsAny<IRestRequest>()))
        .Returns(restResponse);

      var exception = Assert.Throws<OMDBException>(() =>
      {
        var client = GetTestableOMDBClient(restClientMock);

        client.Setup(TestHelpers.GetRandomString());
        client.GetByIMDBId(imdbID);
      });

      Assert.AreEqual(expectedExceptionMessage, exception.Message);
    }

    [Test]
    public void GetByIMDBId_WhenNullIdGiven_ThrowsArgumentNullException()
    {
      var restClientMock = new Mock<IRestClient>();

      var client = GetTestableOMDBClient(restClientMock);
      client.Setup(TestHelpers.GetRandomString());

      Assert.Throws<ArgumentNullException>(() => { client.GetByIMDBId(null); });
    }

    [Test]
    public void GetByIMDBId_IfResponseSuccessful_ReturnsResponse()
    {
      var expectedResponse = GetRandomTitleResponse();

      var restClientMock = new Mock<IRestClient>();
      var restResponseJson = JsonConvert.SerializeObject(expectedResponse);
      var restResponse = new RestResponse
      {
        Content = restResponseJson,
        StatusCode = HttpStatusCode.OK
      };

      restClientMock
        .Setup(mock => mock.Execute(It.IsAny<IRestRequest>()))
        .Returns(restResponse);

      var client = GetTestableOMDBClient(restClientMock);
      client.Setup(TestHelpers.GetRandomString());

      var response = client.GetByIMDBId(TestHelpers.GetRandomString());
      
      AssertTitleResponsesAreEqual(expectedResponse, response);
    }

    [Test]
    public void GetByIMDBId_WhenTitleNotFound_ReturnsNull()
    {
      var restClientMock = new Mock<IRestClient>();
      var imdbID = TestHelpers.GetRandomString();
      var errorMessage = "Movie not found!";
      var errorResponseJson = $"{{ \"Error\": \"{errorMessage}\" }}";

      restClientMock
        .Setup(mock => mock.Execute(It.IsAny<IRestRequest>()))
        .Returns(new RestResponse
        {
          StatusCode = HttpStatusCode.NotFound,
          Content = errorResponseJson
        });

      var client = GetTestableOMDBClient(restClientMock);
      client.Setup(TestHelpers.GetRandomString());

      var response = client.GetByIMDBId(imdbID);
      Assert.IsNull(response);
    }

    [Test]
    public void GetByTitle_IfResponseReturnsError_ThrowsOMDBException()
    {
      var expectedExceptionMessage = "Mock error message";

      var title = TestHelpers.GetRandomString();
      var errorResponseJson = $"{{ \"Error\": \"{expectedExceptionMessage}\" }}";
      var restResponse = new RestResponse
      {
        StatusCode = HttpStatusCode.InternalServerError,
        Content = errorResponseJson
      };

      var restClientMock = new Mock<IRestClient>();

      restClientMock
        .Setup(mock => mock.Execute(It.IsAny<IRestRequest>()))
        .Returns(restResponse);

      var exception = Assert.Throws<OMDBException>(() =>
      {
        var client = GetTestableOMDBClient(restClientMock);

        client.Setup(TestHelpers.GetRandomString());
        client.GetByTitle(title);
      });

      Assert.AreEqual(expectedExceptionMessage, exception.Message);
    }

    [Test]
    public void GetByTitle_WhenNullIdGiven_ThrowsArgumentNullException()
    {
      var restClientMock = new Mock<IRestClient>();

      var client = GetTestableOMDBClient(restClientMock);
      client.Setup(TestHelpers.GetRandomString());

      Assert.Throws<ArgumentNullException>(() => { client.GetByTitle(null); });
    }

    [Test]
    public void GetByTitle_IfResponseSuccessful_ReturnsResponse()
    {
      var expectedResponse = GetRandomTitleResponse();

      var restClientMock = new Mock<IRestClient>();
      var restResponseJson = JsonConvert.SerializeObject(expectedResponse);
      var restResponse = new RestResponse
      {
        Content = restResponseJson,
        StatusCode = HttpStatusCode.OK
      };

      restClientMock
        .Setup(mock => mock.Execute(It.IsAny<IRestRequest>()))
        .Returns(restResponse);

      var client = GetTestableOMDBClient(restClientMock);
      client.Setup(TestHelpers.GetRandomString());

      var response = client.GetByTitle(TestHelpers.GetRandomString());
      
      AssertTitleResponsesAreEqual(expectedResponse, response);
    }

    [Test]
    public void GetByTitle_WhenTitleNotFound_ReturnsNull()
    {
      var restClientMock = new Mock<IRestClient>();
      var titleName = TestHelpers.GetRandomString();
      var errorMessage = "Movie not found!";
      var errorResponseJson = $"{{ \"Error\": \"{errorMessage}\" }}";

      restClientMock
        .Setup(mock => mock.Execute(It.IsAny<IRestRequest>()))
        .Returns(new RestResponse
        {
          StatusCode = HttpStatusCode.NotFound,
          Content = errorResponseJson
        });

      var client = GetTestableOMDBClient(restClientMock);
      client.Setup(TestHelpers.GetRandomString());

      var response = client.GetByTitle(titleName);
      Assert.IsNull(response);
    }

    #region Helpers

    OMDBClient GetTestableOMDBClient(Mock<IRestClient> restClientMock)
    {
      var parameterSpy = new List<Parameter>();

      restClientMock
        .SetupGet(mock => mock.DefaultParameters)
        .Returns(parameterSpy);

      restClientMock
        .Setup(mock => mock.DefaultParameters.Add(It.IsAny<Parameter>()))
        .Callback((Parameter p) => parameterSpy.Add(p));

      return new OMDBClient(restClientMock.Object);
    }

    OMDBTitleResponse GetRandomTitleResponse()
    {
      return new OMDBTitleResponse
      {
        Response = true,
        Actors = TestHelpers.GetRandomString(),
        Awards = TestHelpers.GetRandomString(),
        BoxOffice = TestHelpers.GetRandomString(),
        Country = TestHelpers.GetRandomString(),
        Director = TestHelpers.GetRandomString(),
        DVD = TestHelpers.GetRandomString(),
        Genre = TestHelpers.GetRandomString(),
        ImdbID = TestHelpers.GetRandomString(),
        ImdbRating = TestHelpers.GetRandomString(),
        ImdbVotes = TestHelpers.GetRandomString(),
        Language = TestHelpers.GetRandomString(),
        Metascore = TestHelpers.GetRandomString(),
        Plot = TestHelpers.GetRandomString(),
        Poster = TestHelpers.GetRandomString(),
        Production = TestHelpers.GetRandomString(),
        Rated = TestHelpers.GetRandomString(),
        Released = TestHelpers.GetRandomString(),
        Runtime = TestHelpers.GetRandomString(),
        Title = TestHelpers.GetRandomString(),
        Type = TestHelpers.GetRandomString(),
        Website = TestHelpers.GetRandomString(),
        Writer = TestHelpers.GetRandomString(),
        Year = TestHelpers.GetRandomString()
      };
    }

    void AssertTitleResponsesAreEqual(OMDBTitleResponse expectedResponse, OMDBTitleResponse response)
    {
      Assert.AreEqual(expectedResponse.Actors, response.Actors);
      Assert.AreEqual(expectedResponse.Awards, response.Awards);
      Assert.AreEqual(expectedResponse.BoxOffice, response.BoxOffice);
      Assert.AreEqual(expectedResponse.Country, response.Country);
      Assert.AreEqual(expectedResponse.Director, response.Director);
      Assert.AreEqual(expectedResponse.DVD, response.DVD);
      Assert.AreEqual(expectedResponse.Genre, response.Genre);
      Assert.AreEqual(expectedResponse.ImdbID, response.ImdbID);
      Assert.AreEqual(expectedResponse.ImdbRating, response.ImdbRating);
      Assert.AreEqual(expectedResponse.ImdbVotes, response.ImdbVotes);
      Assert.AreEqual(expectedResponse.Language, response.Language);
      Assert.AreEqual(expectedResponse.Metascore, response.Metascore);
      Assert.AreEqual(expectedResponse.Plot, response.Plot);
      Assert.AreEqual(expectedResponse.Poster, response.Poster);
      Assert.AreEqual(expectedResponse.Production, response.Production);
      Assert.AreEqual(expectedResponse.Rated, response.Rated);
      Assert.AreEqual(expectedResponse.Released, response.Released);
      Assert.AreEqual(expectedResponse.Response, response.Response);
      Assert.AreEqual(expectedResponse.Runtime, response.Runtime);
      Assert.AreEqual(expectedResponse.Title, response.Title);
      Assert.AreEqual(expectedResponse.Type, response.Type);
      Assert.AreEqual(expectedResponse.Website, response.Website);
      Assert.AreEqual(expectedResponse.Writer, response.Writer);
      Assert.AreEqual(expectedResponse.Year, response.Year);
    }

    #endregion

  }
}
