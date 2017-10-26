using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace Telekad.Services.MapTilesAdapter.OpenStreetMap
{
    public abstract class OpenStreetBaseConfigurationElement : MapLayerConfiguration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings"), System.Configuration.ConfigurationProperty("url", IsRequired = false)]
        public string Url
        {
            get
            {
                return this["url"] as string;
            }
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings"), System.Configuration.ConfigurationProperty("refererUrl", IsRequired = false)]
        public string RefererUrl
        {
            get
            {
                return this["refererUrl"] as string;
            }
        }

    }
}