using DotTorrent.Web.Controllers;
using DotTorrent.Web.Models;
using DotTorrent.Web.Services;
using DotTorrent.Web.Services.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotTorrent.Web.Tests.Controllers
{
  public class SearchControllerTests
  {
      SearchController controller;
      Mock<ITorrentFinderHttpService> torrentFinderHttpServiceMock;
      
      [SetUp]
      public void Setup()
      {
          torrentFinderHttpServiceMock = new Mock<ITorrentFinderHttpService>();
          controller = new SearchController(torrentFinderHttpServiceMock.Object);
      }

      [Test]
      public async Task Index_QueryIsPopulated()
      {
          // Arrange

          string query = Testing.Helpers.GetRandomString();

          // Act

          var result = (ViewResult)await controller.Index(query);
          var model = (SearchViewModel)result.Model;

          // Assert

          Assert.AreEqual(query, model.Query);
      }

      [Test]
      public async Task Index_WhenQueryEmpty_SearchResultIsNotPopulated()
      {
          // Act

          var result = (ViewResult)await controller.Index("");
          var model = (SearchViewModel)result.Model;

          // Assert

          Assert.IsEmpty(model.Query);
      }

      [Test]
      public async Task Index_WhenQueryGivenAndTitleFound_SearchResultIsNotNull()
      {
          // Arrange

          string query = Testing.Helpers.GetRandomString();
          ITorrentFinderResponse serviceResponse = new TitleResponse()
          {
              Media = new MediaResponse(),
              Torrents = new List<TorrentResponse>()
          };

          torrentFinderHttpServiceMock
              .Setup(x => x.SearchTitle(query))
              .Returns(Task.FromResult(serviceResponse));

          // Act

          var result = (ViewResult)await controller.Index(query);
          var model = (SearchViewModel)result.Model;

          // Assert

          Assert.IsNotNull(model.SearchResult);
      }

      [Test]
      public async Task Index_WhenQueryGivenAndTitleFound_ErrorIsNull()
      {
          // Arrange

          string query = Testing.Helpers.GetRandomString();
          ITorrentFinderResponse serviceResponse = new TitleResponse()
          {
              Media = new MediaResponse(),
              Torrents = new List<TorrentResponse>()
          };

          torrentFinderHttpServiceMock
              .Setup(x => x.SearchTitle(query))
              .Returns(Task.FromResult(serviceResponse));

          // Act

          var result = (ViewResult)await controller.Index(query);
          var model = (SearchViewModel)result.Model;

          // Assert

          Assert.IsNull(model.ErrorMessage);
      }

      [Test]
      public async Task Index_WhenQueryGivenAndTitleNotFound_SearchResultIsNull()
      {
          // Arrange

          string query = Testing.Helpers.GetRandomString();
          ITorrentFinderResponse serviceResponse = new ErrorResponse();

          torrentFinderHttpServiceMock
              .Setup(x => x.SearchTitle(query))
              .Returns(Task.FromResult(serviceResponse));

          // Act

          var result = (ViewResult)await controller.Index(query);
          var model = (SearchViewModel)result.Model;

          // Assert

          Assert.IsNull(model.SearchResult);
      }

      [Test]
      public async Task Index_WhenQueryGivenAndTitleNotFound_ErrorIsNotEmpty()
      {
          // Arrange

          string query = Testing.Helpers.GetRandomString();
          ITorrentFinderResponse serviceResponse = new ErrorResponse();

          torrentFinderHttpServiceMock
              .Setup(x => x.SearchTitle(query))
              .Returns(Task.FromResult(serviceResponse));

          // Act

          var result = (ViewResult)await controller.Index(query);
          var model = (SearchViewModel)result.Model;

          // Assert

          Assert.IsNotEmpty(model.ErrorMessage);
      }

      [Test]
      public async Task Index_WhenQueryGivenAndExceptionThrown_ErrorIsNotEmpty()
      {
          // Arrange

          torrentFinderHttpServiceMock
              .Setup(x => x.SearchTitle(It.IsAny<string>()))
              .Throws(new Exception("Oof"));

          // Act

          var result = (ViewResult)await controller.Index(Testing.Helpers.GetRandomString());
          var model = (SearchViewModel)result.Model;

          // Assert

          Assert.IsNotEmpty(model.ErrorMessage);
      }
  }
}
