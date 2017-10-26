using System.ComponentModel.Composition;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Telekad.Services.MapTilesAdapter.Bing
{
    [Export(typeof(MapLayerConfiguration))]
    [ExportMetadata("ConfigurationElementName", "bingHybrid")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BingHybridConfigurationElement : BingBaseConfigurationElement
    {
        public override IMapLayer CreateLayer()
        {
            return new BingHybridLayer(this);
        }

    }
}