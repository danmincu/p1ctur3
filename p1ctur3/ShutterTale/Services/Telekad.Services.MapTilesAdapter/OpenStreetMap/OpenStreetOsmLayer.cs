using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Web;
using Telekad.Mapping;

namespace Telekad.Services.MapTilesAdapter.OpenStreetMap
{
    public class OpenStreetOsmLayer : OpenStreetBaseLayer
    {
        private readonly string urlFormat;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public OpenStreetOsmLayer(IConfigurationElement configuration)
            : base(configuration)
        {
            this.urlFormat = string.IsNullOrEmpty(configuration["url"])
                                 ? "http://{0}.tah.openstreetmap.org/Tiles/tile/{1}/{2}/{3}.png%20HTTP/1.1"
                                 : configuration["url"];
        }

        protected override void GetLayer(QuadKey key, Stream stream)
        {
            GetResponse(key, urlFormat, stream);
        }

    }
}