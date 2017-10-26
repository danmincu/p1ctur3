using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace Telekad.Services.MapTilesAdapter.Ovi
{
    [Export(typeof(MapLayerConfiguration))]
    [ExportMetadata("ConfigurationElementName", "oviHybrid")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OviHybridConfigurationElement : OviBaseConfigurationElement
    {
        public override IMapLayer CreateLayer()
        {
            return new OviHybridLayer(this);
        }
    }
}