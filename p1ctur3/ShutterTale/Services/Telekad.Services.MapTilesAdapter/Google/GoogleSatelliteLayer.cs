using System.Diagnostics.CodeAnalysis;
using System.IO;
using Telekad.Mapping;

namespace Telekad.Services.MapTilesAdapter.Google
{
    public class GoogleSatelliteLayer : GoogleBaseMapLayer
    {
        private readonly string urlFormat =
            "http://khm{3}.google.com//kh/v=93&hl=en&x={0}&y={1}&z={2}&s=Galil HTTP/1.1";
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public GoogleSatelliteLayer(IConfigurationElement configuration)
            : base(configuration)
        {
            if (!string.IsNullOrEmpty(configuration["url"]))
                this.urlFormat = configuration["url"];
        }

        [ExcludeFromCodeCoverage]
        protected override void GetLayer(QuadKey key, Stream stream)
        {
            GetResponse(key, urlFormat, stream);
        }

    }
}