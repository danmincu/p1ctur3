using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using Telekad.Mapping;

namespace Telekad.Services.MapTilesAdapter.ArcGis
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "GIS")]
    public class ArcGISWorldStreetMapLayer : ArcGISBaseMapLayer
    {

        private readonly string urlFormat = "http://server.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{0}/{1}/{2}";
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#")]
        public ArcGISWorldStreetMapLayer(IConfigurationElement configuration)
            : base(configuration)
        {
            if (!string.IsNullOrEmpty(configuration["url"]))
                this.urlFormat = configuration["url"];
        }
        
        protected override void GetLayer(QuadKey key, Stream stream)
        {
            GetResponse(key, urlFormat, stream);
        }
    }
}