using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;

namespace Telekad.Services.MapTilesAdapter.Yahoo
{
    [Export(typeof(MapLayerConfiguration))]
    [ExportMetadata("ConfigurationElementName", "yahooRoad")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class YahooRoadConfigurationElement : YahooBaseConfigurationElement
    {
        public override IMapLayer CreateLayer()
        {
            return new YahooRoadLayer(this);
        }
    }
}