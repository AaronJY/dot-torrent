using DotTorrent.Web.Services.Responses;
using System.Threading.Tasks;

namespace DotTorrent.Web.Services
{
    public interface ITorrentFinderHttpService
    {
        Task<ITorrentFinderResponse> SearchTitle(string title);

        Task<ITorrentFinderResponse> SearchIMDBId(string IMDBID);
    }
}