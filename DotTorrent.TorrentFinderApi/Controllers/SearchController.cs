using DotTorrent.OMDB;
using DotTorrent.TorrentFinderApi.Config;
using DotTorrent.TorrentFinderApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotTorrent.TorrentFinderApi.Controllers
{
    /// <summary>
    /// Search for IMDB titles
    /// </summary>
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        readonly IOMDBClient _omdbClient;

        public SearchController(IAppSettings appSettings)
        {
            _omdbClient = new OMDBClient(appSettings.OMDBApiKey);
        }

        /// <summary>
        /// Search for a title on OMDB
        /// </summary>
        /// <param name="title">IMDB title name</param>
        /// <returns></returns>
        [HttpGet]
        [Route("title")]
        public ActionResult<MediaSearchResult> SearchTitle(string title)
        {
            var omdbTitle = _omdbClient.GetByTitle(title);
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
        [Route("id")]
        public ActionResult<MediaSearchResult> SearchId(string id)
        {
            var omdbTitle = _omdbClient.GetByIMDBId(id);
            if (omdbTitle == null)
                return NotFound();

            return new MediaSearchResult(omdbTitle);
        }
    }
}
