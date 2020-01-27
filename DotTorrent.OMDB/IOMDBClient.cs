using System.Threading.Tasks;

namespace DotTorrent.OMDB
{
    public interface IOMDBClient
    {
        void Setup(string apiKey);
        Task<OMDBTitleResponse> GetByIMDBId(string imdbId);
        Task<OMDBTitleResponse> GetByTitle(string title);
    }
}