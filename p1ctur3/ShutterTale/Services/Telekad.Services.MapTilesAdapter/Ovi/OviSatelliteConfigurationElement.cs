using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace Telekad.Services.MapTilesAdapter.Ovi
{
    [Export(typeof(MapLayerConfiguration))]
    [ExportMetadata("ConfigurationElementName", "oviSatellite")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OviSatelliteConfigurationElement : OviBaseConfigurationElement
    {
        public override IMapLayer CreateLayer()
        {
            return new OviSatelliteLayer(this);
        }
    }

}