using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telekad.Services.MapTilesAdapter
{
    [Serializable]
    public class TileProvider
    {
        public string Mode { set; get; }
        public MapType MapType { set; get; }
    }
}