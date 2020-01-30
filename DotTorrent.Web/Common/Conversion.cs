using System;

namespace DotTorrent.Web.Common
{
  public static class Conversion
  {
    private const ulong BytesInKB = 1024;
    private const ulong BytesInMB = 1048576;
    private const ulong BytesInGB = 1073741824;

    private const string BytesSuffix = "bytes";
    private const string KBSuffix = "KB";
    private const string MBSuffix = "MB";
    private const string GBSuffix = "GB";

    public static string GetPrettySize(ulong bytes, int decimalPlaces = 0)
    {
      if (bytes < BytesInKB) return bytes + " " + BytesSuffix;
      if (bytes < BytesInMB) return Math.Round((double)bytes / BytesInKB, decimalPlaces) + " " + KBSuffix;
      if (bytes < BytesInGB) return Math.Round((double)bytes / BytesInMB, decimalPlaces) + " " + MBSuffix;
      return Math.Round((double)bytes / BytesInGB, decimalPlaces) + " " + GBSuffix;
    }
  }
}
