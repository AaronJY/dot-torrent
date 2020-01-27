using DotTorrent.OMDB.Enums;
using DotTorrent.OMDB.Exceptions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace DotTorrent.OMDB
{
    public class OMDBClient : IOMDBClient
    {
        const string BaseUrl = "https://www.omdbapi.com/";

        protected string apiKey;

        readonly IRestClient restClient;

        public OMDBClient(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        /// <summary>
        /// Sets up the client with an API key
        /// </summary>
        /// <param name="apiKey"></param>
        public void Setup(string apiKey)
        {
            restClient.BaseUrl = new Uri(BaseUrl);
            SetApiKey(apiKey);
        }

        /// <summary>
        /// Gets OMDB details from an IMDB ID
        /// </summary>
        /// <param name="imdbId"></param>
        /// <returns></returns>
        public async Task<OMDBTitleResponse> GetByIMDBId(string imdbId)
        {
            if (string.IsNullOrEmpty(imdbId))
                throw new ArgumentNullException(nameof(imdbId));

            var req = new RestRequest("", Method.GET);
            req.AddQueryParameter("i", imdbId);

            OMDBResponse resp = await this.Execute(req);
            if (resp.Response)
                return (OMDBTitleResponse)resp;

            var errorResponse = (OMDBErrorResponse)resp;
            if (IsErrorResponseTitleNotFound(errorResponse))
              return null;

            throw new OMDBException(errorResponse.Error);
        }

        /// <summary>
        /// Gets OMDB details for a title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<OMDBTitleResponse> GetByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException(nameof(title));

            var req = new RestRequest("", Method.GET);
            req.AddQueryParameter("t", title);

            OMDBResponse resp = await this.Execute(req);
            if (resp.Response)
                return (OMDBTitleResponse)resp;

            var errorResponse = (OMDBErrorResponse)resp;
            if (IsErrorResponseTitleNotFound(errorResponse))
              return null;

            throw new OMDBException(errorResponse.Error);
        }

        protected async Task<OMDBResponse> Execute(IRestRequest req)
        {
          ThrowIfNoAPIKey();

          IRestResponse resp = await restClient.ExecuteTaskAsync(req);

          var titleResponse = JsonConvert.DeserializeObject<OMDBTitleResponse>(resp.Content);
          if (titleResponse.Response)
              return titleResponse;

          var errorResponse = JsonConvert.DeserializeObject<OMDBErrorResponse>(resp.Content);
          return errorResponse;
        }

        void ThrowIfNoAPIKey()
        {
          if (string.IsNullOrWhiteSpace(apiKey))
            throw new OMDBException("No api key set.");
        }

        void SetApiKey(string apiKey)
        {
            this.apiKey = apiKey;
            restClient.AddDefaultParameter(new Parameter("apikey", apiKey, ParameterType.QueryString));
        }

        bool IsErrorResponseTitleNotFound(OMDBErrorResponse resp)
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
