using System;

namespace DotTorrent.OMDB.Tests
{
  public static class TestHelpers
  {
    public static string GetRandomString()
    {
      return Guid.NewGuid().ToString();
    }
  }
}
