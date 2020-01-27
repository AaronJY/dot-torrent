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
            var req = new RestRequest("search/title")
              .AddParameter("query", title);

            IRestResponse resp = await restClient.ExecuteTaskAsync(req);
            if (!resp.IsSuccessful)
                return JsonConvert.DeserializeObject<ErrorResponse>(resp.Content);

            var test =  JsonConvert.DeserializeObject<TorrentResponse>(resp.Content);
            return test;
        }

        public void SearchIMDBId(string IMDBID)
        {
            throw new NotImplementedException();
        }
    }
}
