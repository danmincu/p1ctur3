using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Telekad.Services.MapTilesAdapter
{
    public abstract class MapLayerConfiguration : System.Configuration.ConfigurationElement, IConfigurationElement
    {
        [System.Configuration.ConfigurationProperty("name", IsRequired = false)]
        public string Name
        {
            get
            {
                return this["name"] as string;
            }
        }

        [System.Configuration.ConfigurationProperty("cacheTilesFolder", IsRequired = false)]
        public string CacheTilesFolder
        {
            get
            {
                return this["cacheTilesFolder"] as string;
            }
        }

        public abstract IMapLayer CreateLayer();

        public new string this[string index]
        {
            get
            {
                if (this.Properties.Contains(index))
                    return base[index] as string;
                else
                    return null;
            }
        }
    }



}