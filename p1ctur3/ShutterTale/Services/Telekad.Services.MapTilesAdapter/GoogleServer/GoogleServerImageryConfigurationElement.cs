

using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;

namespace Telekad.Services.MapTilesAdapter.GoogleServer
{
    [Export(typeof(MapLayerConfiguration))]
    [ExportMetadata("ConfigurationElementName", "googleServerImagery")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GoogleServerImageryConfigurationElement : GoogleServerBaseConfigurationElement
    {
        public override IMapLayer CreateLayer()
        {
            return new GoogleServerImageryLayer(this);
        }
    }
}