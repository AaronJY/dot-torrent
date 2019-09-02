namespace DotTorrent.OMDB
{
    public interface IOMDBClient
    {
        OMDBTitleResponse GetByIMDBId(string imdbId);
        OMDBTitleResponse GetByTitle(string title);
    }
}