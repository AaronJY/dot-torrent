using DotTorrent.OMDB;
using DotTorrent.TorrentFinderApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotTorrent.TorrentFinderApi.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        readonly IOMDBClient omdbClient;

        public SearchController()
        {
            omdbClient = new OMDBClient("4b9133b2");
        }

        [HttpGet]
        [Route("title/{title}")]
        public ActionResult<MediaSearchResult> SearchTitle(string title)
        {
            var omdbTitle = omdbClient.GetByTitle(title);
            if (omdbTitle == null)
                return NotFound();

            return new MediaSearchResult(omdbTitle);
        }

        [HttpGet]
        [Route("id/{id}")]
        public ActionResult<MediaSearchResult> SearchId(string id)
        {
            var omdbTitle = omdbClient.GetByIMDBId(id);
            if (omdbTitle == null)
                return NotFound();

            return new MediaSearchResult(omdbTitle);
        }
    }
}
