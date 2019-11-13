using DotTorrent.TorrentFinderApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace DotTorrent.TorrentFinderApi.Tests
{
  public class DefaultControllerTests
  {
    [Test]
    public void Index_ShowsGreeting()
    {
      var controller = new DefaultController();
      var result = (ContentResult)controller.Index();

      Assert.AreEqual("We're up!", result.Content);
    }
  }
}
