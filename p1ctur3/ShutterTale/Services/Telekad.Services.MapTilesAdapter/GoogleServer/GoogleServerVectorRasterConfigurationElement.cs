using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;

namespace Telekad.Services.MapTilesAdapter.GoogleServer
{
    [Export(typeof(MapLayerConfiguration))]
    [ExportMetadata("ConfigurationElementName", "googleServerVectorRaster")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GoogleServerVectorRasterConfigurationElement : GoogleServerBaseConfigurationElement
    {
        public override IMapLayer CreateLayer()
        {
            return new GoogleServerVectorRasterLayer(this);
        }

    }
}