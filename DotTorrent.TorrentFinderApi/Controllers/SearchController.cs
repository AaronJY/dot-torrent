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

        /// <summary>
        /// Search for a title on OMDB
        /// </summary>
        /// <param name="title">IMDB title name</param>
        /// <returns></returns>
        [HttpGet]
        [Route("title/{title}")]
        public ActionResult<MediaSearchResult> SearchTitle(string title)
        {
            var omdbTitle = omdbClient.GetByTitle(title);
            if (omdbTitle == null)
                return NotFound();

            return new MediaSearchResult(omdbTitle);
        }

        /// <summary>
        /// Searches for an IMDB title with a given IMDB ID
        /// </summary>
        /// <param name="id">IMDB title ID</param>
        /// <returns></returns>
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
