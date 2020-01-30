using DotTorrent.Testing;
using DotTorrent.Web.Config;
using DotTorrent.Web.Services;
using DotTorrent.Web.Services.Responses;
using Moq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace DotTorrent.Web.Tests.Services
{
    public class TorrentFinderHttpServiceTests
    {
        ITorrentFinderHttpService service;
        Mock<IAppSettings> appSettingsMock;
        Mock<IRestClient> restClientMock;

        private const string SuccessfulRestResponseJSON = "{\"media\":{\"title\":\"Alien\",\"releaseYear\":1979,\"imdbId\":\"tt0078748\",\"plot\":\"After a space merchant vessel perceives an unknown transmission as a distress call, its landing on the source moon finds one of the crew attacked by a mysterious lifeform, and they soon realize that its life cycle has merely begun.\",\"actors\":[\"Tom Skerritt\",\"Sigourney Weaver\",\"Veronica Cartwright\",\"Harry Dean Stanton\"],\"posterUrl\":\"https://m.media-amazon.com/images/M/MV5BMmQ2MmU3NzktZjAxOC00ZDZhLTk4YzEtMDMyMzcxY2IwMDAyXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_SX300.jpg\"},\"torrents\":[{\"name\":\"Placeholder torrent\",\"sizeInBytes\":1234567,\"addedDate\":\"2020-01-29T16:29:39.2397817+00:00\",\"fileCount\":2,\"trackerName\":\"Placeholder tracker\",\"trackerUrl\":\"http://localhost\"}]}";
        private const string UnSuccessfulRestResponseJSON = "{\"successful\":\"false\",\"error\":\"Something went wrong!\"}";

        [SetUp]
        public void Setup()
        {
            appSettingsMock = new Mock<IAppSettings>();
            appSettingsMock.SetupGet(x => x.TorrentFinderApiUrl).Returns("https://www.google.com");

            restClientMock = new Mock<IRestClient>();

            service = new TorrentFinderHttpService(
              appSettingsMock.Object, restClientMock.Object);
        }

        [Test]
        public async Task SearchTitle_WhenSuccessful_ReturnsTitleResponse()
        {
            // Arrange
        
            IRestResponse restResponse = GetDummySuccessfulRestResponse();

            restClientMock
                .Setup(x => x.ExecuteTaskAsync(It.IsAny<IRestRequest>()))
                .Returns(Task.FromResult(restResponse));

            // Act

            var resp = (TitleResponse)await service.SearchTitle(Helpers.GetRandomString());

            // Assert

            Assert.IsTrue(resp.Successful);
            Assert.IsNotEmpty(resp.Media.Title);
            Assert.IsNotEmpty(resp.Media.PosterUrl);
            Assert.IsNotEmpty(resp.Media.Plot);
            Assert.IsNotEmpty(resp.Media.IMDBId);
        }

        [Test]
        public async Task SearchTitle_WhenResponseNotSuccessful_ReturnsErrorResponse()
        {
            // Arrange

            IRestResponse restResponse = GetDummyUnSuccessfulRestResponse();

            restClientMock
                .Setup(x => x.ExecuteTaskAsync(It.IsAny<IRestRequest>()))
                .Returns(Task.FromResult(restResponse));

            // Act

            var resp = (ErrorResponse)await service.SearchTitle(Helpers.GetRandomString());

            // Assert

            Assert.IsNotEmpty(resp.Error);
            Assert.IsFalse(resp.Successful);
        }

        [Test]
        public void SearchTitle_WhenTitleArgumentNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await service.SearchTitle(null));
        }
          
        [Test]
        public void SearchTitle_WhenTitleArgumentEmpty_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await service.SearchTitle(""));
        }

        [Test]
        public async Task SearchIMDBId_WhenSuccessful_ReturnsTitleResponse()
        {
            // Arrange
        
            IRestResponse restResponse = GetDummySuccessfulRestResponse();

            restClientMock
                .Setup(x => x.ExecuteTaskAsync(It.IsAny<IRestRequest>()))
                .Returns(Task.FromResult(restResponse));

            // Act

            var resp = (TitleResponse)await service.SearchIMDBId(Helpers.GetRandomString());

            // Assert

            Assert.IsTrue(resp.Successful);
            Assert.IsNotEmpty(resp.Media.Title);
            Assert.IsNotEmpty(resp.Media.PosterUrl);
            Assert.IsNotEmpty(resp.Media.Plot);
            Assert.IsNotEmpty(resp.Media.IMDBId);
        }

        [Test]
        public async Task SearchIMDBId_WhenResponseNotSuccessful_ReturnsErrorResponse()
        {
            // Arrange

            IRestResponse restResponse = GetDummyUnSuccessfulRestResponse();

            restClientMock
                .Setup(x => x.ExecuteTaskAsync(It.IsAny<IRestRequest>()))
                .Returns(Task.FromResult(restResponse));

            // Act

            var resp = (ErrorResponse)await service.SearchIMDBId(Helpers.GetRandomString());

            // Assert

            Assert.IsNotEmpty(resp.Error);
            Assert.IsFalse(resp.Successful);
        }

        [Test]
        public void SearchIMDBId_WhenTitleArgumentNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await service.SearchIMDBId(null));
        }
          
        [Test]
        public void SearchIMDBId_WhenTitleArgumentEmpty_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await service.SearchIMDBId(""));
        }

        IRestResponse GetDummySuccessfulRestResponse()
        {
            return new RestResponse
            {
                Content = SuccessfulRestResponseJSON,
                ResponseStatus = ResponseStatus.Completed,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }

        IRestResponse GetDummyUnSuccessfulRestResponse()
        {
            return new RestResponse
            {
                Content = UnSuccessfulRestResponseJSON,
                ResponseStatus = ResponseStatus.Error,
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            };
        }
    }
}
