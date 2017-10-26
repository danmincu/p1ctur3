using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telekad.Services.MapTilesAdapter
{
    public interface IMapProvider
    {
        IEnumerable<IMapLayer> Layers { get; }
        string Name { get; }
        MapType MapType { get; }
    }
}