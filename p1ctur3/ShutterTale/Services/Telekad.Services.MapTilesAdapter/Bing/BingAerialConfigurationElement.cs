using System.ComponentModel.Composition;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Telekad.Services.MapTilesAdapter.Bing
{
    [Export(typeof(MapLayerConfiguration))]
    [ExportMetadata("ConfigurationElementName", "bingAerial")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BingAerialConfigurationElement : BingBaseConfigurationElement
    {
        public override IMapLayer CreateLayer()
        {
            return new BingAerialLayer(this);
        }
    }
}