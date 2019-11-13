using Microsoft.AspNetCore.Mvc;

namespace DotTorrent.Web.Controllers
{
    [Route("media")]
    public class MediaController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index(string search = null)
        {
            return View("~Views/Media.cshtml");
        }
    }
}