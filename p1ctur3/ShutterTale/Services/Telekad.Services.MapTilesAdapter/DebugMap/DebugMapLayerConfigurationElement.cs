using System.ComponentModel.Composition;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Telekad.Services.MapTilesAdapter.Debug
{
    [Export(typeof(MapLayerConfiguration))]
    [ExportMetadata("ConfigurationElementName", "debugMap")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DebugMapLayerConfigurationElement : MapLayerConfiguration
    {
        public override IMapLayer CreateLayer()
        {
            return new DebugMapLayer(this);
            
        }
    }
    
}