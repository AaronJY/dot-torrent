using AutoMapper;
using DotTorrent.TorrentFinder.Models;
using DotTorrent.TorrentFinderApi.Models;

namespace DotTorrent.TorrentFinderApi.Config.AutoMapperProfiles
{
  public class TorrentProfile : Profile
  {
    public TorrentProfile()
    {
      CreateMap<Torrent, TorrentResult>();
    }
  }
}
