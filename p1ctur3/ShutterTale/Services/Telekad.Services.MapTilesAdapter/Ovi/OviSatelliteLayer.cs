using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Web;
using Telekad.Mapping;

namespace Telekad.Services.MapTilesAdapter.Ovi
{
    public class OviSatelliteLayer : OviBaseMapLayer
    {
        private readonly string urlFormat;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public OviSatelliteLayer(IConfigurationElement configuration)
            : base(configuration)
        {
            this.urlFormat = string.IsNullOrEmpty(configuration["url"])
                                 ? "http://{3}.maptile.maps.svc.ovi.com/maptiler/v2/maptile/newest/satellite.day/{0}/{1}/{2}/256/png"
                                 : configuration["url"];
        }

        [ExcludeFromCodeCoverage]
        protected override void GetLayer(QuadKey key, Stream stream)
        {
            GetResponse(key, this.urlFormat, stream);
        }
    }
}