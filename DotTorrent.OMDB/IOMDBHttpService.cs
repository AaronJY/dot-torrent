using System.Threading.Tasks;

namespace DotTorrent.OMDB
{
  public interface IOMDBHttpService
  {
    Task<OMDBResponse> GetByIMDBId(string IMDBID);
    Task<OMDBResponse> GetByTitle(string title);
    void SetApiKey(string apiKey);
  }
}