using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;

namespace Telekad.Services.MapTilesAdapter.ArcGis
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "GIS"), Export(typeof(MapLayerConfiguration))]
    [ExportMetadata("ConfigurationElementName", "arcGISWorldTopoMap")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ArcGISWorldTopoMapConfigurationElement : ArcGISBaseConfigurationElement
    {
        public override IMapLayer CreateLayer()
        {
            return new ArcGISWorldTopoMapLayer(this);
        }
    }

}