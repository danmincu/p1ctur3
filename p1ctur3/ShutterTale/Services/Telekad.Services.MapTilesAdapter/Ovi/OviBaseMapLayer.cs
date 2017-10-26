using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Telekad.Mapping;

namespace Telekad.Services.MapTilesAdapter.Ovi
{
    public abstract class OviBaseMapLayer : BaseMapLayer
    {
        static int currentTileServer;
        private readonly string refererUrlConfig;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        protected OviBaseMapLayer(IConfigurationElement configuration)
            : base(configuration)
        {
            this.refererUrlConfig = string.IsNullOrEmpty(configuration["refererUrl"]) ? "http://maps.ovi.com/" : configuration["refererUrl"];
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        [ExcludeFromCodeCoverage]
        protected void GetResponse(QuadKey key, string serverUrl, Stream stream)
        {
            var serverPrefix = new string[] { "b", "c", "d", "e" };
            var tile = key.ToTile();
            var request = (HttpWebRequest)WebRequest.Create(new Uri(string.Format(CultureInfo.InvariantCulture, serverUrl, key.Level, tile.X, tile.Y, serverPrefix[(currentTileServer++) % 3])));
            request.ServicePoint.ConnectionLimit = 50;
            request.Proxy = WebRequest.DefaultWebProxy;
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.1.7) Gecko/20091221 Firefox/3.5.7";
            request.Referer = this.refererUrlConfig;
            request.Timeout = 10 * 1000;
            request.ReadWriteTimeout = request.Timeout * 6;
            request.KeepAlive = true;
            WebRequestToStream(request, stream, key);
        }
    }
}