using System;
using System.Linq;
using DotTorrent.TorrentFinderApi.Models;
using NUnit.Framework;

namespace DotTorrent.TorrentFinderApi.Tests
{
  public class MediaSearchResultTests
    {
        [Test]
        public void  ConstructingMediaSearchResult_WithTitleResponse_ConstructsCorrectly()
        {
            var titleResponse = Testing.Helpers.GetRandomTitleResponse();
            var expectedReleaseYear = DateTime.Parse(titleResponse.Released).Year;
            var expectedActors = titleResponse.Actors.Split(',').Select(actor => actor.Trim());

            var result = new MediaResult(titleResponse);

            Assert.AreEqual(titleResponse.Title, result.Title);
            Assert.AreEqual(titleResponse.ImdbID, result.IMDBId);
            Assert.AreEqual(titleResponse.Plot, result.Plot);
            Assert.AreEqual(expectedReleaseYear, result.ReleaseYear);
            Assert.AreEqual(expectedActors, result.Actors);
        }

        [Test]
        public void ConstructingMediaSearchResult_WhenNoRelaseYear_ReleaseYearIsNull()
        {
            var titleResponse = Testing.Helpers.GetRandomTitleResponse();
            titleResponse.Released = null;

            var result = new MediaResult(titleResponse);
            
            Assert.IsNull(result.ReleaseYear);
        }

        [Test]
        public void ConstructingMediaSearchResult_WhenNoActors_ActorsIsEmpty()
        {
            var titleResponse = Testing.Helpers.GetRandomTitleResponse();
            titleResponse.Actors = null;

            var result = new MediaResult(titleResponse);
            
            Assert.IsEmpty(result.Actors);
        }
    }
}
