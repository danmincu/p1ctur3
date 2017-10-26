using System.Diagnostics.CodeAnalysis;
using System.IO;
using Telekad.Mapping;

namespace Telekad.Services.MapTilesAdapter.Google
{
    public class GoogleRoadLayer : GoogleBaseMapLayer
    {
        private readonly string urlFormat =
            "http://mt{3}.google.com/vt/lyrs=m@161000000&hl=en&x={0}&y={1}&z={2}&s=G%20HTTP/1.1";
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public GoogleRoadLayer(IConfigurationElement configuration)
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