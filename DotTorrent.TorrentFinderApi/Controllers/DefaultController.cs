using Microsoft.AspNetCore.Mvc;

namespace DotTorrent.TorrentFinderApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
      public ActionResult Index()
      {
          return Content("We're up!");
      }
    }
}