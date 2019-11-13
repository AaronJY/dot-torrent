using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotTorrent.OMDB.Tests
{
  public class IServiceCollectionExtensionsTests
  {
    [Test]
    public void AddOMDBServices_CorrectlyAddsAllServices()
    {
      var serviceCollection = new ServiceCollection();
      serviceCollection.AddOMDBServices();

      var expectedBindings = new Dictionary<Type, Type>()
      {
        { typeof(IRestClient), typeof(RestClient) }
      };

      foreach (var binding in expectedBindings)
      {
        Assert.IsTrue(serviceCollection.Any(desc => desc.ServiceType == binding.Key && desc.ImplementationType == binding.Value));
      }
    }
  }
}
