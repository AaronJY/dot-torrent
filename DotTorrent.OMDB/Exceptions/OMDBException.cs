using System;

namespace DotTorrent.OMDB.Exceptions
{
    public class OMDBException : Exception
    {
        public OMDBException() : base() { }

        public OMDBException(string message) : base(message) { }
    }
}
