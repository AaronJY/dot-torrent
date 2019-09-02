using System;
using System.Collections.Generic;
using System.Text;

namespace DotTorrent.OMDB
{
    public class OMDBErrorResponse : OMDBResponse
    {
        public string Error { get; set; }
    }
}
