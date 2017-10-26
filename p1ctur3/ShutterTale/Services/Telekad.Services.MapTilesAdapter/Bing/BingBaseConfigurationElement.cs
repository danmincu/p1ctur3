using System.Diagnostics.CodeAnalysis;

namespace Telekad.Services.MapTilesAdapter.Bing
{
    public abstract class BingBaseConfigurationElement : MapLayerConfiguration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings"), System.Configuration.ConfigurationProperty("url", IsRequired = false)]
        public string Url
        {
            get
            {
                return this["url"] as string;
            }
        }

        [System.Configuration.ConfigurationProperty("applicationId", IsRequired = false)]
        public string ApplicationId
        {
            get
            {
                return this["applicationId"] as string;
            }
        }
        
    }
}