using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Net;
using Telekad.Mapping;

namespace Telekad.Services.MapTilesAdapter.Google
{
    public abstract class GoogleBaseMapLayer : BaseMapLayer
    {
        static int currentTileServer;
        private static string refererUrlConfig;
        private static string refererMagicStringConfig;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        protected GoogleBaseMapLayer(IConfigurationElement configuration)
            : base(configuration)
        {
            refererUrlConfig = string.IsNullOrEmpty(configuration["refererUrl"]) ? "http://maps.{0}/" : configuration["refererUrl"];
            refererMagicStringConfig = string.IsNullOrEmpty(configuration["refererMagicWord"])
                                           ? "zOl/KnHzebJUqs6JWROaCQ=="
                                           : configuration["refererMagicWord"];
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        [ExcludeFromCodeCoverage]
        protected void GetResponse(QuadKey key, string serverUrl, Stream stream)
        {
            var tile = key.ToTile();
            var request = (HttpWebRequest)WebRequest.Create(new Uri(string.Format(CultureInfo.InvariantCulture, serverUrl, tile.X, tile.Y, key.Level, (currentTileServer++) % 4)));
            request.ServicePoint.ConnectionLimit = 50;
            request.Proxy = WebRequest.DefaultWebProxy;
            request.UserAgent = "Opera/9.62 (Windows NT 5.1; U; en) Presto/2.1.1";
            request.Timeout = 10 * 1000;
            request.ReadWriteTimeout = request.Timeout * 6;
            request.Referer = string.Format(CultureInfo.InvariantCulture, refererUrlConfig, refererMagicStringConfig);
            request.KeepAlive = true;
            WebRequestToStream(request, stream, key);
        }
    }
}