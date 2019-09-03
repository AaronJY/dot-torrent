using DotTorrent.OMDB.Enums;
using DotTorrent.OMDB.Exceptions;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace DotTorrent.OMDB
{
    public class OMDBClient : IOMDBClient
    {
        const string _BaseUrl = "https://www.omdbapi.com/";

        protected string apiKey;

        RestClient restClient;

        public OMDBClient(string apiKey)
        {
            this.apiKey = apiKey;

            restClient = new RestClient(_BaseUrl);
            restClient.AddDefaultParameter("apikey", apiKey);
        }

        /// <summary>
        /// Gets OMDB details from an IMDB ID
        /// </summary>
        /// <param name="imdbId"></param>
        /// <returns></returns>
        public OMDBTitleResponse GetByIMDBId(string imdbId)
        {
            if (string.IsNullOrEmpty(imdbId))
                throw new ArgumentNullException(nameof(imdbId));

            var req = new RestRequest("", Method.GET);
            req.AddQueryParameter("i", imdbId);

            OMDBResponse resp = this.Execute(req);
            if (resp.Response)
                return (OMDBTitleResponse)resp;

            var errorResponse = (OMDBErrorResponse)resp;
            if (TitleNotFound(errorResponse))
              return null;

            throw new OMDBException(errorResponse.Error);
        }

        /// <summary>
        /// Gets OMDB details for a title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public OMDBTitleResponse GetByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException(nameof(title));

            var req = new RestRequest("", Method.GET);
            req.AddQueryParameter("t", title);

            OMDBResponse resp = this.Execute(req);
            if (resp.Response)
                return (OMDBTitleResponse)resp;

            var errorResponse = (OMDBErrorResponse)resp;
            if (TitleNotFound(errorResponse))
              return null;

            throw new OMDBException(errorResponse.Error);
        }

        protected OMDBResponse Execute(IRestRequest req)
        {
          IRestResponse resp = restClient.Execute(req);

          var titleResponse = JsonConvert.DeserializeObject<OMDBTitleResponse>(resp.Content);
          if (titleResponse.Response)
              return titleResponse;

          var errorResponse = JsonConvert.DeserializeObject<OMDBErrorResponse>(resp.Content);
          switch (errorResponse.Error)
          {
              case "Incorrect IMDb ID.":
              case "Movie not found!":
                  return null;

              default:
                  throw new OMDBException(errorResponse.Error);
          }
        }

        private bool TitleNotFound(OMDBErrorResponse resp)
        {
          switch (resp.ErrorType)
          {
              case ResponseErrorType.IncorrectIMDBId:
              case ResponseErrorType.MovieNotFound:
                  return true;

              default:
                  return false;
          }
        }
    }
}
