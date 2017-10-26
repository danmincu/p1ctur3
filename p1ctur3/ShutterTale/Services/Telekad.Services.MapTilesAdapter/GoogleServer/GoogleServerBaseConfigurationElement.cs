using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace Telekad.Services.MapTilesAdapter.GoogleServer
{
    public abstract class GoogleServerBaseConfigurationElement : MapLayerConfiguration
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings"), System.Configuration.ConfigurationProperty("url", IsRequired = false)]
        public string Url
        {
            get
            {
                return this["url"] as string;
            }
        }

        [System.Configuration.ConfigurationProperty("machineName", IsRequired = true)]
        public string MachineName
        {
            get
            {
                return this["machineName"] as string;
            }
        }


        [System.Configuration.ConfigurationProperty("defaultMap", IsRequired = false)]
        public string DefaultMap
        {
            get
            {
                return this["defaultMap"] as string;
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



        [System.Configuration.ConfigurationProperty("channel", IsRequired = true)]
        public string Channel
        {
            get
            {
                return this["channel"] as string;
            }
        }


        [System.Configuration.ConfigurationProperty("version", IsRequired = true)]
        public string Version
        {
            get
            {
                return this["version"] as string;
            }
        }



    }
}