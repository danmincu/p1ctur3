using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Telekad.Services.MapTilesAdapter
{
    public class MapCollectionElement : System.Configuration.ConfigurationElement, IMapProvider
    {
        [System.Configuration.ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return this["name"] as string;
            }
        }

        [System.Configuration.ConfigurationProperty("mapType", IsRequired = true)]
        public MapType MapType
        {
            get
            {
                return (MapType)base["mapType"];
            }
        }

        [System.Configuration.ConfigurationProperty("", IsDefaultCollection = true, IsRequired = true)]
        public LayersCollection LayersCollection
        {
            get { return base[""] as LayersCollection; }
        }

        public IEnumerable<IMapLayer> Layers
        {
            get
            {
                return from MapLayerConfiguration layer in this.LayersCollection select layer.CreateLayer();
            }
        }
    }
}