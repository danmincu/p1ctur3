using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using Telekad.Mapping;

namespace Telekad.Services.MapTilesAdapter.GoogleServer
{
    public class GoogleServerImageryLayer : GoogleServerBaseMapLayer
    {
        private readonly string serverUrl;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "6#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public GoogleServerImageryLayer(IConfigurationElement configuration)
            : base(configuration)
        {
            var urlFormatConfig = string.IsNullOrEmpty(configuration["url"])
                                 ? "http://{5}/{6}/query?request=ImageryMaps&channel={3}&version={4}&x={0}&y={1}&z={2}"
                                 : configuration["url"];
            serverUrl = string.Format(CultureInfo.InvariantCulture, urlFormatConfig, "{0}", "{1}", "{2}", this.Channel, this.Version, this.MachineName, this.DefaultMap);
        }

        protected override void GetLayer(QuadKey key, Stream stream)
        {
            GetResponse(key, serverUrl, stream);
        }
    }
}