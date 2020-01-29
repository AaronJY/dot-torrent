using DotTorrent.Web.Config;
using DotTorrent.Web.Services.Responses;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace DotTorrent.Web.Services
{
    public class TorrentFinderHttpService : ITorrentFinderHttpService
    {
        readonly IAppSettings appSettings;
        readonly IRestClient restClient;

        public TorrentFinderHttpService(
            IAppSettings appSettings,
            IRestClient restClient)
        {
            this.appSettings = appSettings;

            this.restClient = restClient;
            this.restClient.BaseUrl = new Uri(appSettings.TorrentFinderApiUrl);
        }

        public async Task<ITorrentFinderResponse> SearchTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));

            var req = new RestRequest("search/title").AddParameter("query", title);
            return await ExecuteTorrentFinderApiRequestAsync<TitleResponse>(req);
        }

        public async Task<ITorrentFinderResponse> SearchIMDBId(string IMDBID)
        {
            if (string.IsNullOrWhiteSpace(IMDBID))
                throw new ArgumentNullException(nameof(IMDBID));

            var req = new RestRequest("search/id").AddParameter("query", IMDBID);
            return await ExecuteTorrentFinderApiRequestAsync<TitleResponse>(req);
        }

        async Task<ITorrentFinderResponse> ExecuteTorrentFinderApiRequestAsync<T>(IRestRequest req) where T : ITorrentFinderResponse
        {
          IRestResponse resp = await restClient.ExecuteTaskAsync(req);

          // Any unsuccessful response should be possible to parse as an ErrorResponse
          if (!resp.IsSuccessful)
              return JsonConvert.DeserializeObject<ErrorResponse>(resp.Content);

          return JsonConvert.DeserializeObject<T>(resp.Content);
        }
    }
}
