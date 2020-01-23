using DotTorrent.Web.Config;
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

    public async Task<object> SearchTitle(string title)
    {
      var req = new RestRequest("search/title");
      req.AddQueryParameter("query", title);

      IRestResponse resp = await restClient.ExecuteTaskAsync(req);
      if (!resp.IsSuccessful)
      {
        // Handle error
        return null;
      }

      return JsonConvert.DeserializeObject<object>(resp.Content);
    }

    public void SearchIMDBId(string IMDBID)
    {

    }
  }
}
