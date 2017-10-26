using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;

namespace Telekad.Services.MapTilesAdapter.ArcGis
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "GIS"), Export(typeof(MapLayerConfiguration))]
    [ExportMetadata("ConfigurationElementName", "arcGISWorldStreetMap")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ArcGISWorldStreetMapConfigurationElement : ArcGISBaseConfigurationElement
    {
        public override IMapLayer CreateLayer()
        {
            return new ArcGISWorldStreetMapLayer(this);
        }
    }
}