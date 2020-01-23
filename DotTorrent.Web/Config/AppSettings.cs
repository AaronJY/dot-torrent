using Microsoft.Extensions.Configuration;

namespace DotTorrent.Web.Config
{
  public class AppSettings : IAppSettings
  {
    readonly IConfiguration _configuration;

    public AppSettings(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public string TorrentFinderApiUrl => _configuration.GetValue<string>("TorrentFinderApiUrl");
  }
}
