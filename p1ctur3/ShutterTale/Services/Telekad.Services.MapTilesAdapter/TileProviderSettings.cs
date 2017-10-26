using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telekad.Services.MapTilesAdapter
{
    [Serializable]
    public class TileProvidersSettings
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<TileProvider> Providers { set; get; }
    }
}