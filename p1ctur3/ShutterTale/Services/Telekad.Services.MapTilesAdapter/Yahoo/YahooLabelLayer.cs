using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Web;
using Telekad.Mapping;

namespace Telekad.Services.MapTilesAdapter.Yahoo
{
    public class YahooLabelLayer : YahooBaseMapLayer
    {
        private readonly string urlFormat;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public YahooLabelLayer(IConfigurationElement configuration)
            : base(configuration)
        {
            this.urlFormat = string.IsNullOrEmpty(configuration["url"])
                                 ? "http://maps{0}.yimg.com/hx/tl?v=4.3&t=h&.intl=en&x={1}&y={2}&z={3}&r=1%20HTTP/1.1"
                                 : configuration["url"];
        }

        [ExcludeFromCodeCoverage]
        protected override void GetLayer(QuadKey key, Stream stream)
        {

            GetResponse(key, this.urlFormat, stream);
        }
    }
}