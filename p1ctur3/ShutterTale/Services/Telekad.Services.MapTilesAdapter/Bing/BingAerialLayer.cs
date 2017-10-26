using System.Diagnostics.CodeAnalysis;
using System.IO;
using Telekad.Mapping;

namespace Telekad.Services.MapTilesAdapter.Bing
{
    public class BingAerialLayer : BingBaseMapLayer
    {
        private readonly string urlFormat = "http://t{0}.tiles.virtualearth.net/tiles/a{1}.png?g=244&token={2}";
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#")]
        public BingAerialLayer(IConfigurationElement configuration)
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