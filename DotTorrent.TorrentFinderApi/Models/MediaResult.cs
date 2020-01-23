using DotTorrent.OMDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotTorrent.TorrentFinderApi.Models
{
    public class MediaResult
    {
        public MediaResult() { }
        
        public MediaResult(OMDBTitleResponse omdbTitleResponse)
        {
            Title = omdbTitleResponse.Title;
            IMDBId = omdbTitleResponse.ImdbID;
            Plot = omdbTitleResponse.Plot;
            PosterUrl = omdbTitleResponse.Poster;

            if (DateTime.TryParse(omdbTitleResponse.Released, out DateTime releaseDateTime))
                ReleaseYear = releaseDateTime.Year;

            if (omdbTitleResponse.Actors != null)
                Actors = omdbTitleResponse.Actors.Split(',').Select(x => x.Trim());
            else
                Actors = Enumerable.Empty<string>();
        }

        public string Title { get; set; }

        public int? ReleaseYear { get; set; }

        public string IMDBId { get; set; }

        public string Plot { get; set; }

        public IEnumerable<string> Actors { get; set; }

        public string PosterUrl { get; set; }
    }
}
