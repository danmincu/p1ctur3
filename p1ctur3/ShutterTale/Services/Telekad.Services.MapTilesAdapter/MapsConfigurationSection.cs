using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace Telekad.Services.MapTilesAdapter
{
    public class MapsConfigurationSection : ConfigurationSection
    {
        private const string ConfigurationSectionConst = "Maps";

        /// <summary>
        /// Returns an MapsConfigurationSection instance
        /// </summary>
        public static MapsConfigurationSection GetConfig()
        {
            return (MapsConfigurationSection)ConfigurationManager.
               GetSection(MapsConfigurationSection.ConfigurationSectionConst) ??
               new MapsConfigurationSection();
        }

        [System.Configuration.ConfigurationProperty("", IsDefaultCollection = true,IsRequired = true)]
        public MapsCollection MapsCollection
        {
            get
            {
                return (MapsCollection)this[""] ?? new MapsCollection();
            }
        }

        public IEnumerable<IMapProvider> MapProviders
        {
            get
            {
                return this.MapsCollection.Cast<IMapProvider>();
            }
        }
    }
}