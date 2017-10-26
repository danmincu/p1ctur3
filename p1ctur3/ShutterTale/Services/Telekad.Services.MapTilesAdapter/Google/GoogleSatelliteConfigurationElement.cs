using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;

namespace Telekad.Services.MapTilesAdapter.Google
{
    [Export(typeof(MapLayerConfiguration))]
    [ExportMetadata("ConfigurationElementName", "googleSatellite")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GoogleSatelliteConfigurationElement : GoogleBaseConfigurationElement
    {
        public override IMapLayer CreateLayer()
        {
            return new GoogleSatelliteLayer(this);
        }
    }
}