using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Telekad.Mapping;

namespace Telekad.Services.MapTilesAdapter.OpenStreetMap
{
    public abstract class OpenStreetBaseLayer : BaseMapLayer
    {
        static int currentTileServer;
        private readonly string refererUrlConfig;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        protected OpenStreetBaseLayer(IConfigurationElement configuration)
            : base(configuration)
        {
            this.refererUrlConfig = string.IsNullOrEmpty(configuration["refererUrl"]) ? "http://www.openstreetmap.org/" : configuration["refererUrl"];
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        protected void GetResponse(QuadKey key, string serverUrl, Stream stream)
        {
            var serverPrefix = new string[] { "a", "b", "c" };
            var tile = key.ToTile();
            var request = (HttpWebRequest)WebRequest.Create(new Uri(string.Format(CultureInfo.InvariantCulture, serverUrl, serverPrefix[(currentTileServer++) % 3], key.Level, tile.X, tile.Y)));
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