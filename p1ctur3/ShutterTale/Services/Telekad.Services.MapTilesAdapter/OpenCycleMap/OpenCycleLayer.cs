using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Telekad.Mapping;

namespace Telekad.Services.MapTilesAdapter.OpenCycleMap
{
    public class OpenCycleLayer : BaseMapLayer
    {
        static int currentTileServer;

        private readonly string urlFomat;
        private readonly string refererUrl;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#")]
        public OpenCycleLayer(IConfigurationElement configuration)
            : base(configuration)
        {
            this.urlFomat = string.IsNullOrEmpty(configuration["url"])
                                ? "http://{0}.tile.opencyclemap.org/cycle/{1}/{2}/{3}.png%20HTTP/1.1"
                                : configuration["url"];
            this.refererUrl = string.IsNullOrEmpty(configuration["refererUrl"]) ? "http://www.opencyclemap.org/" : configuration["refererUrl"];
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
            request.Referer = this.refererUrl;
            request.KeepAlive = true;
            WebRequestToStream(request, stream, key);
        }

        protected override void GetLayer(QuadKey key, Stream stream)
        {
            GetResponse(key, this.urlFomat, stream);
        }
    }
}