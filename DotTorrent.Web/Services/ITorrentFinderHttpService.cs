using System.Threading.Tasks;

namespace DotTorrent.Web.Services
{
  public interface ITorrentFinderHttpService
  {
    void SearchIMDBId(string IMDBID);
    Task<object> SearchTitle(string title);
  }
}