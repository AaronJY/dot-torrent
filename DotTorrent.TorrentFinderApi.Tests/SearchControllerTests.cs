using DotTorrent.OMDB;
using DotTorrent.TorrentFinderApi.Config;
using DotTorrent.TorrentFinderApi.Controllers;
using DotTorrent.TorrentFinderApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DotTorrent.TorrentFinderApi.Tests
{
  public class SearchControllerTests
  {
    Mock<IAppSettings> _appSettingsMock;
    Mock<IOMDBClient> _omdbClientMock;

    [SetUp]
    public void SetUp()
    {
      _appSettingsMock = new Mock<IAppSettings>();
      _omdbClientMock = new Mock<IOMDBClient>();
    }

    [Test]
    public async Task SearchTitle_WhenTitleNotFound_ReturnsNotFound()
    {
      _omdbClientMock
        .Setup(mock => mock.GetByTitle(It.IsAny<string>()))
        .Returns(() => null);

      var controller = GetTestableSearchController(_appSettingsMock, _omdbClientMock);
      var actionResult = await controller.SearchTitle(Testing.Helpers.GetRandomString());

      Assert.IsTrue(actionResult.Result.GetType() == typeof(NotFoundResult));
    }

    [Test]
    public async Task SearchTitle_WhenTitleFound_ReturnsResult()
    {
      var titleResponse = Testing.Helpers.GetRandomTitleResponse();

      _omdbClientMock
        .Setup(mock => mock.GetByTitle(It.IsAny<string>()))
        .Returns(titleResponse);

      var controller = GetTestableSearchController(_appSettingsMock, _omdbClientMock);
      var mediaSearchResult = (await controller.SearchTitle(Testing.Helpers.GetRandomString())).Value;

      AssertOMDBTitleResponseIsEqualToMediaSearchResult(titleResponse, mediaSearchResult);
    }

    [Test]
    public async Task SearchId_WhenTitleNotFound_ReturnsNotFound()
    {
      _omdbClientMock
        .Setup(mock => mock.GetByIMDBId(It.IsAny<string>()))
        .Returns(() => null);

      var controller = GetTestableSearchController(_appSettingsMock, _omdbClientMock);
      var actionResult = await controller.SearchId(Testing.Helpers.GetRandomString());

      Assert.IsTrue(actionResult.Result.GetType() == typeof(NotFoundResult));
    }

    [Test]
    public async Task SearchId_WhenTitleFound_ReturnsResult()
    {
      var titleResponse = Testing.Helpers.GetRandomTitleResponse();

      _omdbClientMock
        .Setup(mock => mock.GetByIMDBId(It.IsAny<string>()))
        .Returns(titleResponse);

      var controller = GetTestableSearchController(_appSettingsMock, _omdbClientMock);
      var mediaSearchResult = (await controller.SearchId(Testing.Helpers.GetRandomString())).Value;

      AssertOMDBTitleResponseIsEqualToMediaSearchResult(titleResponse, mediaSearchResult);
    }

    SearchController GetTestableSearchController(Mock<IAppSettings> appSettingsMock, Mock<IOMDBClient> omdbClientMock)
    {
      appSettingsMock.SetupGet(mock => mock.OMDBApiKey)
        .Returns(Testing.Helpers.GetRandomString());

      return new SearchController(
        appSettingsMock.Object,
        omdbClientMock.Object);
    }

    void AssertOMDBTitleResponseIsEqualToMediaSearchResult(OMDBTitleResponse expected, MediaSearchResult result)
    {
      var expectedActors = expected.Actors.Split(',').Select(actor => actor.Trim());

      int? expectedReleaseYear = null;
      if (expected.Released != null)
      {
        expectedReleaseYear = DateTime.Parse(expected.Released).Year;
      }

      Assert.AreEqual(expectedActors, result.Actors);
      Assert.AreEqual(expected.ImdbID, result.IMDBId);
      Assert.AreEqual(expected.Plot, result.Plot);
      Assert.AreEqual(expectedReleaseYear, result.ReleaseYear);
      Assert.AreEqual(expected.Title, result.Title);
    }
  }
}