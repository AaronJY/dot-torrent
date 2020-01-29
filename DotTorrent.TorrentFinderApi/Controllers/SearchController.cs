using AutoMapper;
using DotTorrent.OMDB;
using DotTorrent.TorrentFinderApi.Config;
using DotTorrent.TorrentFinderApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<SearchResult>> SearchTitle(string query)
        {
            OMDBTitleResponse omdbTitle = await _omdbClient.GetByTitle(query);
            if (omdbTitle == null)
                return NotFound();

            var result = new SearchResult
            {
                Media = new MediaResult(omdbTitle),
                Torrents = new List<TorrentResult>()
                {
                   new TorrentResult
                   {
                       Name = "Placeholder torrent",
                       AddedDate = DateTime.Now,
                       FileCount = 2,
                       SizeInBytes = 1234567,
                       TrackerName = "Placeholder tracker",
                       TrackerUrl = "http://localhost"
                   }
                }
            };

            return result;
        }

        /// <summary>
        /// Searches for an IMDB title with a given IMDB ID
        /// </summary>
        /// <param name="query">IMDB title ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("id")]
        public async Task<ActionResult<SearchResult>> SearchId(string query)
        {
            OMDBTitleResponse omdbTitle = await _omdbClient.GetByIMDBId(query);
            if (omdbTitle == null)
                return NotFound();

            var result = new SearchResult
            {
                Media = new MediaResult(omdbTitle),
                Torrents = new List<TorrentResult>()
            };

            return result;
        }
    }
}
