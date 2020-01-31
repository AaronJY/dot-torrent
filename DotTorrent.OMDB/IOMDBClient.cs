using System.Threading.Tasks;

namespace DotTorrent.OMDB
{
    public interface IOMDBClient
    {
        Task<OMDBTitleResponse> GetByIMDBId(string imdbId);
        Task<OMDBTitleResponse> GetByTitle(string title);
    }
}