using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;

namespace Telekad.Services.MapTilesAdapter.Debug
{
    [Export(typeof(MapLayerConfiguration))]
    [ExportMetadata("ConfigurationElementName", "debugTransparentMap")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DebugTransparentMapLayerConfigurationElement : MapLayerConfiguration
    {
        public override IMapLayer CreateLayer()
        {
            return new DebugTransparentMapLayer(this);
        }
    }
}