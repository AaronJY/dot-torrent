using DotTorrent.Web.Services.Responses;
using System.Threading.Tasks;

namespace DotTorrent.Web.Services
{
  public interface ITorrentFinderHttpService
  {
    void SearchIMDBId(string IMDBID);

    Task<ITorrentFinderResponse> SearchTitle(string title);
  }
}