using Microsoft.Extensions.Configuration;

namespace DotTorrent.TorrentFinderApi.Config
{
  public class AppSettings : IAppSettings
  {
    readonly IConfiguration _configuration;

    public AppSettings(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public string OMDBApiKey => _configuration.GetValue<string>("OMDBApiKey");
  }
}
