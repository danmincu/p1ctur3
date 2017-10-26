using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace Telekad.Services.MapTilesAdapter.OpenStreetMap
{
    [Export(typeof(MapLayerConfiguration))]
    [ExportMetadata("ConfigurationElementName", "openStreetOsm")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpenStreetOsmConfigurationElement : OpenStreetBaseConfigurationElement
    {
        public override IMapLayer CreateLayer()
        {
            return new OpenStreetOsmLayer(this);
        }
    }
}