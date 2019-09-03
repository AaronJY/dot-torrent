using Microsoft.AspNetCore.Mvc;

namespace DotTorrent.Web.Controllers
{
    [Route("")]
    [Route("search")]
    public class SearchController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("~/Views/Search.cshtml");
        }
    }
}