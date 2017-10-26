using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;

namespace Telekad.Services.MapTilesAdapter.Yahoo
{
    [Export(typeof(MapLayerConfiguration))]
    [ExportMetadata("ConfigurationElementName", "yahooLabel")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class YahooLabelConfigurationElement : YahooBaseConfigurationElement
    {
        public override IMapLayer CreateLayer()
        {
            return new YahooLabelLayer(this);
        }
    }
}