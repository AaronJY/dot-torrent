using DotTorrent.Web.Models;
using DotTorrent.Web.Services;
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
                    object result = await torrentFinderHttpService.SearchTitle(query);
                    if (result == null)
                    {
                        viewModel.ErrorMessage = $"We couldn't find any titles matching the search term \"{query}\"";
                    }
                    else
                    {
                        viewModel.SearchResult = result;
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