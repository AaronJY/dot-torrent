using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace DotTorrent.OMDB
{
  public static class IServiceCollectionExtensions
  {
    public static IServiceCollection AddOMDBServices(this IServiceCollection services)
    {
      services.AddTransient<IRestClient, RestClient>();

      return services;
    }
  }
}
