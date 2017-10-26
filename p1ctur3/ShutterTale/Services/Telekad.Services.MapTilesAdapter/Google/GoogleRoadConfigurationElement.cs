using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;

namespace Telekad.Services.MapTilesAdapter.Google
{
    [Export(typeof(MapLayerConfiguration))]
    [ExportMetadata("ConfigurationElementName", "googleRoad")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GoogleRoadConfigurationElement : GoogleBaseConfigurationElement
    {
        public override IMapLayer CreateLayer()
        {
            return new GoogleRoadLayer(this);
        }
    }
}