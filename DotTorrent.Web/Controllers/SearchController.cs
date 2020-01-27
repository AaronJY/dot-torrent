using DotTorrent.Web.Models;
using DotTorrent.Web.Services;
using DotTorrent.Web.Services.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DotTorrent.Web.Controllers
{
    [Route("")]
    [Route("search")]
    public class SearchController : Controller
    {
        readonly ITorrentFinderHttpService torrentFinderHttpService;

        public SearchController(
          ITorrentFinderHttpService torrentFinderHttpService)
        {
            this.torrentFinderHttpService = torrentFinderHttpService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index(string query = null)
        {
            var viewModel = new SearchViewModel
            {
                Query = query
            };

            if (!string.IsNullOrWhiteSpace(query))
            {
                try
                {
                    ITorrentFinderResponse result = await torrentFinderHttpService.SearchTitle(query);
                    if (!result.Successful)
                    {
                        viewModel.ErrorMessage = $"We couldn't find any titles matching the search term \"{query}\"";
                    }
                    else
                    {
                        viewModel.SearchResult = new SearchResultViewModel((TorrentResponse)result);
                    }
                }
                catch (Exception ex)
                {
                    viewModel.ErrorMessage = "Sorry, but something went wrong on our end! Please try again.";
                    Console.WriteLine(ex);
                }
            }

            return View("~/Views/Search.cshtml", viewModel);
        }
    }
}