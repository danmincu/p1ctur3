using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;

namespace Telekad.Services.MapTilesAdapter.Google
{
    [Export(typeof(MapLayerConfiguration))]
    [ExportMetadata("ConfigurationElementName", "googleLabel")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GoogleLabelConfigurationElement : GoogleBaseConfigurationElement
    {
        public override IMapLayer CreateLayer()
        {
            return new GoogleLabelLayer(this);
        }
    }

}