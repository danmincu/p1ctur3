using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Telekad.Mapping;

namespace Telekad.Services.MapTilesAdapter.Yahoo
{
    public abstract class YahooBaseMapLayer : BaseMapLayer
    {
        static int currentTileServer;

        private readonly string refererUrlConfig;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        protected YahooBaseMapLayer(IConfigurationElement configuration)
            : base(configuration)
        {
            this.refererUrlConfig = string.IsNullOrEmpty(configuration["refererUrl"]) ? "http://maps.yahoo.com/" : configuration["refererUrl"];
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        [ExcludeFromCodeCoverage]
        protected void GetResponse(QuadKey key, string serverUrl, Stream stream)
        {
            var tile = key.ToTile();
            var request = (HttpWebRequest)WebRequest.Create(new Uri(string.Format(CultureInfo.InvariantCulture, serverUrl, (currentTileServer++) % 3 + 1,
                           tile.X, (((1 << key.Level) >> 1) - 1 - tile.Y), key.Level + 1)));
            request.ServicePoint.ConnectionLimit = 50;
            request.Proxy = WebRequest.DefaultWebProxy;
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.1.7) Gecko/20091221 Firefox/3.5.7";
            request.Timeout = 10 * 1000;
            request.ReadWriteTimeout = request.Timeout * 6;
            request.Referer = this.refererUrlConfig;
            request.KeepAlive = true;
            WebRequestToStream(request, stream, key);
        }
    }
}