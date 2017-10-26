using System.ComponentModel.Composition;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Telekad.Services.MapTilesAdapter.Bing
{
    [Export(typeof(MapLayerConfiguration))]
    [ExportMetadata("ConfigurationElementName", "bingRoad")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BingRoadConfigurationElement : BingBaseConfigurationElement
    {
        public override IMapLayer CreateLayer()
        {
            return new BingRoadLayer(this);
        }
    }
}