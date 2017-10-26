using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telekad.Services.MapTilesAdapter.ArcGis
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "GIS")]
    public abstract class ArcGISBaseConfigurationElement : MapLayerConfiguration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings"), System.Configuration.ConfigurationProperty("url", IsRequired = false)]
        public string Url
        {
            get
            {
                return this["url"] as string;
            }
        }
    }

}