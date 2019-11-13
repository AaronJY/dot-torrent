namespace DotTorrent.OMDB
{
    public interface IOMDBClient
    {
        void Setup(string apiKey);
        OMDBTitleResponse GetByIMDBId(string imdbId);
        OMDBTitleResponse GetByTitle(string title);
    }
}