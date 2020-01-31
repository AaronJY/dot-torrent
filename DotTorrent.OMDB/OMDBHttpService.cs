using DotTorrent.OMDB.Exceptions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace DotTorrent.OMDB
{
  public class OMDBHttpService : IOMDBHttpService
  {
    private const string BaseUrl = "https://www.omdbapi.com/";

    readonly IRestClient restClient;

    string apiKey;

    public OMDBHttpService(IRestClient restClient)
    {
      this.restClient = restClient;
      restClient.BaseUrl = new Uri(BaseUrl);
    }

    public void SetApiKey(string apiKey)
    {
      this.apiKey = apiKey;
      restClient.AddDefaultParameter(new Parameter("apikey", apiKey, ParameterType.QueryString));
    }

    public async Task<OMDBResponse> GetByIMDBId(string IMDBID)
    {
      var req = new RestRequest("", Method.GET)
          .AddQueryParameter("i", IMDBID);

      return await this.Execute(req);
    }

    public async Task<OMDBResponse> GetByTitle(string title)
    {
      var req = new RestRequest("", Method.GET)
          .AddQueryParameter("t", title);

      return await this.Execute(req);
    }

    async Task<OMDBResponse> Execute(IRestRequest req)
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
  }
}
