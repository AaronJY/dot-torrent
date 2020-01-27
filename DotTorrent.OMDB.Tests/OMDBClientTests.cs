using DotTorrent.OMDB.Exceptions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

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

      var exception = Assert.ThrowsAsync<OMDBException>(async () =>
      {
        await client.GetByIMDBId(Testing.Helpers.GetRandomString());
      });

      Assert.AreEqual(expectedExceptionMessage, exception.Message);
    }

    [Test]
    public void GetByTitle_IfNoApiKey_ThrowsOMDBException()
    {
      var expectedExceptionMessage = "No api key set.";

      var restClientMock = new Mock<IRestClient>();
      var client = GetTestableOMDBClient(restClientMock);

      var exception = Assert.ThrowsAsync<OMDBException>(async () =>
      {
        await client.GetByTitle(Testing.Helpers.GetRandomString());
      });

      Assert.AreEqual(expectedExceptionMessage, exception.Message);
    }

    [Test]
    public void GetByIMDBId_IfResponseReturnsError_ThrowsOMDBException()
    {
      var expectedExceptionMessage = "Mock error message";
      var imdbID = Testing.Helpers.GetRandomString();
      var errorResponseJson = $"{{ \"Error\": \"{expectedExceptionMessage}\" }}";
      IRestResponse restResponse = new RestResponse
      {
        StatusCode = HttpStatusCode.InternalServerError,
        Content = errorResponseJson
      };

      var restClientMock = new Mock<IRestClient>();

      restClientMock
        .Setup(mock => mock.ExecuteTaskAsync(It.IsAny<IRestRequest>()))
        .Returns(Task.FromResult(restResponse));

      OMDBClient client = GetTestableOMDBClient(restClientMock);
      client.Setup(Testing.Helpers.GetRandomString());

      OMDBException exception = Assert.ThrowsAsync<OMDBException>(async () =>
      {
        await client.GetByIMDBId(imdbID);
      });

      Assert.AreEqual(expectedExceptionMessage, exception.Message);
    }

    [Test]
    public void GetByIMDBId_WhenNullIdGiven_ThrowsArgumentNullException()
    {
      var restClientMock = new Mock<IRestClient>();

      var client = GetTestableOMDBClient(restClientMock);
      client.Setup(Testing.Helpers.GetRandomString());

      Assert.ThrowsAsync<ArgumentNullException>(async () => { await client.GetByIMDBId(null); });
    }

    [Test]
    public async Task GetByIMDBId_IfResponseSuccessful_ReturnsResponse()
    {
      var expectedResponse = Testing.Helpers.GetRandomTitleResponse();

      var restClientMock = new Mock<IRestClient>();
      var restResponseJson = JsonConvert.SerializeObject(expectedResponse);
      IRestResponse restResponse = new RestResponse
      {
        Content = restResponseJson,
        StatusCode = HttpStatusCode.OK
      };

      restClientMock
        .Setup(mock => mock.ExecuteTaskAsync(It.IsAny<IRestRequest>()))
        .Returns(Task.FromResult(restResponse));

      var client = GetTestableOMDBClient(restClientMock);
      client.Setup(Testing.Helpers.GetRandomString());

      var response = await client.GetByIMDBId(Testing.Helpers.GetRandomString());
      
      AssertTitleResponsesAreEqual(expectedResponse, response);
    }

    [Test]
    public async Task GetByIMDBId_WhenTitleNotFound_ReturnsNull()
    {
      var restClientMock = new Mock<IRestClient>();
      var imdbID = Testing.Helpers.GetRandomString();
      var errorMessage = "Movie not found!";
      var errorResponseJson = $"{{ \"Error\": \"{errorMessage}\" }}";

      IRestResponse restResult = new RestResponse
      {
        StatusCode = HttpStatusCode.NotFound,
        Content = errorResponseJson
      };

      restClientMock
        .Setup(mock => mock.ExecuteTaskAsync(It.IsAny<IRestRequest>()))
        .Returns(Task.FromResult(restResult));

      var client = GetTestableOMDBClient(restClientMock);
      client.Setup(Testing.Helpers.GetRandomString());

      var response = await client.GetByIMDBId(imdbID);
      Assert.IsNull(response);
    }

    [Test]
    public void GetByTitle_IfResponseReturnsError_ThrowsOMDBException()
    {
      var expectedExceptionMessage = "Mock error message";

      var title = Testing.Helpers.GetRandomString();
      var errorResponseJson = $"{{ \"Error\": \"{expectedExceptionMessage}\" }}";
      IRestResponse restResponse = new RestResponse
      {
        StatusCode = HttpStatusCode.InternalServerError,
        Content = errorResponseJson
      };

      var restClientMock = new Mock<IRestClient>();

      restClientMock
        .Setup(mock => mock.ExecuteTaskAsync(It.IsAny<IRestRequest>()))
        .Returns(Task.FromResult(restResponse));

      var exception = Assert.ThrowsAsync<OMDBException>(async () =>
      {
        var client = GetTestableOMDBClient(restClientMock);

        client.Setup(Testing.Helpers.GetRandomString());
        await client.GetByTitle(title);
      });

      Assert.AreEqual(expectedExceptionMessage, exception.Message);
    }

    [Test]
    public void GetByTitle_WhenNullIdGiven_ThrowsArgumentNullException()
    {
      var restClientMock = new Mock<IRestClient>();

      var client = GetTestableOMDBClient(restClientMock);
      client.Setup(Testing.Helpers.GetRandomString());

      Assert.ThrowsAsync<ArgumentNullException>(async () => { await client.GetByTitle(null); });
    }

    [Test]
    public async Task GetByTitle_IfResponseSuccessful_ReturnsResponse()
    {
      var expectedResponse = Testing.Helpers.GetRandomTitleResponse();

      var restClientMock = new Mock<IRestClient>();
      var restResponseJson = JsonConvert.SerializeObject(expectedResponse);
      IRestResponse restResponse = new RestResponse
      {
        Content = restResponseJson,
        StatusCode = HttpStatusCode.OK
      };

      restClientMock
        .Setup(mock => mock.ExecuteTaskAsync(It.IsAny<IRestRequest>()))
        .Returns(Task.FromResult(restResponse));

      var client = GetTestableOMDBClient(restClientMock);
      client.Setup(Testing.Helpers.GetRandomString());

      var response = await client.GetByTitle(Testing.Helpers.GetRandomString());
      
      AssertTitleResponsesAreEqual(expectedResponse, response);
    }

    [Test]
    public async Task GetByTitle_WhenTitleNotFound_ReturnsNull()
    {
      var restClientMock = new Mock<IRestClient>();
      var titleName = Testing.Helpers.GetRandomString();
      var errorMessage = "Movie not found!";
      var errorResponseJson = $"{{ \"Error\": \"{errorMessage}\" }}";

      IRestResponse restResponse = new RestResponse
      {
        StatusCode = HttpStatusCode.NotFound,
        Content = errorResponseJson
      };

      restClientMock
        .Setup(mock => mock.ExecuteTaskAsync(It.IsAny<IRestRequest>()))
        .Returns(Task.FromResult(restResponse));

      var client = GetTestableOMDBClient(restClientMock);
      client.Setup(Testing.Helpers.GetRandomString());

      var response = await client.GetByTitle(titleName);
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
