using NUnit.Framework;
using System;

namespace DotTorrent.Testing.Tests
{
  public class HelpersTests
  {
    [Test]
    public void GetRandomString_ReturnsRandomString()
    {
      // Act

      var str = Helpers.GetRandomString();

      // Assert

      Assert.DoesNotThrow(() => Guid.Parse(str));
    }

    [Test]
    public void GetRandomDateTime_ReturnsRandomDateTime()
    {
      // Act

      var dateTime = Helpers.GetRandomDateTime();

      // Assert

      Assert.IsInstanceOf<DateTime>(dateTime);
    }

    [Test]
    public void GetRandomTitleResponse_ReturnsRandomOMDBTitleResponse()
    {
      // Act

      var resp = Helpers.GetRandomTitleResponse();

      // Assert

      Assert.IsTrue(resp.Response);
      Assert.That(!string.IsNullOrEmpty(resp.Actors));
      Assert.That(!string.IsNullOrEmpty(resp.Awards));
      Assert.That(!string.IsNullOrEmpty(resp.BoxOffice));
      Assert.That(!string.IsNullOrEmpty(resp.Country));
      Assert.That(!string.IsNullOrEmpty(resp.Director));
      Assert.That(!string.IsNullOrEmpty(resp.DVD));
      Assert.That(!string.IsNullOrEmpty(resp.Genre));
      Assert.That(!string.IsNullOrEmpty(resp.ImdbID));
      Assert.That(!string.IsNullOrEmpty(resp.ImdbRating));
      Assert.That(!string.IsNullOrEmpty(resp.ImdbVotes));
      Assert.That(!string.IsNullOrEmpty(resp.Language));
      Assert.That(!string.IsNullOrEmpty(resp.Metascore));
      Assert.That(!string.IsNullOrEmpty(resp.Plot));
      Assert.That(!string.IsNullOrEmpty(resp.Poster));
      Assert.That(!string.IsNullOrEmpty(resp.Production));
      Assert.That(!string.IsNullOrEmpty(resp.Rated));
      Assert.That(!string.IsNullOrEmpty(resp.Released));
      Assert.That(!string.IsNullOrEmpty(resp.Runtime));
      Assert.That(!string.IsNullOrEmpty(resp.Title));
      Assert.That(!string.IsNullOrEmpty(resp.Type));
      Assert.That(!string.IsNullOrEmpty(resp.Website));
      Assert.That(!string.IsNullOrEmpty(resp.Writer));
      Assert.That(!string.IsNullOrEmpty(resp.Year));
    }
  }
}