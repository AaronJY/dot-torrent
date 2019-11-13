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

        public SearchController(
            IAppSettings appSettings,
            IOMDBClient omdbClient)
        {
            _omdbClient = omdbClient;
            _omdbClient.Setup(appSettings.OMDBApiKey);
        }

        /// <summary>
        /// Search for a title on OMDB
        /// </summary>
        /// <param name="query">IMDB title name</param>
        /// <returns></returns>
        [HttpGet]
        [Route("title")]
        public ActionResult<MediaSearchResult> SearchTitle(string query)
        {
            var omdbTitle = _omdbClient.GetByTitle(query);
            if (omdbTitle == null)
                return NotFound();

            return new MediaSearchResult(omdbTitle);
        }

        /// <summary>
        /// Searches for an IMDB title with a given IMDB ID
        /// </summary>
        /// <param name="query">IMDB title ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("id")]
        public ActionResult<MediaSearchResult> SearchId(string query)
        {
            var omdbTitle = _omdbClient.GetByIMDBId(query);
            if (omdbTitle == null)
                return NotFound();

            return new MediaSearchResult(omdbTitle);
        }
    }
}
