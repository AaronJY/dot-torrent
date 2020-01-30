using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System;

namespace DotTorrent.OMDB
{
  public static class Internal
  {
      static IServiceProvider _serviceProvivder;

      public static IServiceProvider GetServiceProvider()
      {
          if (_serviceProvivder != null)
              return _serviceProvivder;

          _serviceProvivder = new ServiceCollection()
              .AddTransient<IRestClient, RestClient>()
              .BuildServiceProvider();

          return _serviceProvivder;
      }
  }
}
