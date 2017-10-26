using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;

namespace Telekad.Services.MapTilesAdapter.Yahoo
{
    [Export(typeof(MapLayerConfiguration))]
    [ExportMetadata("ConfigurationElementName", "yahooSatellite")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class YahooSatelliteConfigurationElement : YahooBaseConfigurationElement
    {
        public override IMapLayer CreateLayer()
        {
            return new YahooSatelliteLayer(this);
        }
    }
}