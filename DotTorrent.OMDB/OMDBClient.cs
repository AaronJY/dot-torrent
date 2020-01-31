using DotTorrent.OMDB.Enums;
using DotTorrent.OMDB.Exceptions;
using System;
using System.Threading.Tasks;

namespace DotTorrent.OMDB
{
  public class OMDBClient : IOMDBClient
    {
        protected string apiKey;

        readonly IOMDBHttpService httpService;

        public OMDBClient(IOMDBHttpService httpService)
        {
            this.httpService = httpService;
        }

        /// <summary>
        /// Gets OMDB details from an IMDB ID
        /// </summary>
        /// <param name="imdbId"></param>
        /// <returns></returns>
        public async Task<OMDBTitleResponse> GetByIMDBId(string imdbId)
        {
            if (string.IsNullOrEmpty(imdbId))
                throw new ArgumentNullException(nameof(imdbId));

            var resp = await httpService.GetByIMDBId(imdbId);
            if (resp.Response)
                return (OMDBTitleResponse)resp;

            var errorResponse = (OMDBErrorResponse)resp;
            if (IsErrorResponseTitleNotFound(errorResponse))
                return null;

            throw new OMDBException(errorResponse.Error);
        }

        /// <summary>
        /// Gets OMDB details for a title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<OMDBTitleResponse> GetByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException(nameof(title));
           
            var resp = await httpService.GetByTitle(title);
            if (resp.Response)
                return (OMDBTitleResponse)resp;

            var errorResponse = (OMDBErrorResponse)resp;
            if (IsErrorResponseTitleNotFound(errorResponse))
                return null;

            throw new OMDBException(errorResponse.Error);
        }

        bool IsErrorResponseTitleNotFound(OMDBErrorResponse resp)
        {
            switch (resp.ErrorType)
            {
                case ResponseErrorType.IncorrectIMDBId:
                case ResponseErrorType.MovieNotFound:
                    return true;

                default:
                    return false;
            }
        }
    }
}
