using DotTorrent.OMDB;
using System;

namespace DotTorrent.TorrentFinderApi.Models
{
    public class MediaSearchResult
    {
        public MediaSearchResult() { }
        
        public MediaSearchResult(OMDBTitleResponse omdbTitleResponse)
        {
            Title = omdbTitleResponse.Title;
            IMDBId = omdbTitleResponse.ImdbID;

            if (DateTime.TryParse(omdbTitleResponse.Released, out DateTime releaseDateTime))
            {
                ReleaseYear = releaseDateTime.Year;
            }
        }

        public string Title { get; set; }

        public int? ReleaseYear { get; set; }

        public string IMDBId { get; set; }
    }
}
