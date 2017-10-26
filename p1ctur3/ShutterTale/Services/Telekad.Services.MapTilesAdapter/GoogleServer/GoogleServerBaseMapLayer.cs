using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Net;
using Telekad.Mapping;
using Telekad.Utils;


namespace Telekad.Services.MapTilesAdapter.GoogleServer
{
    public abstract class GoogleServerBaseMapLayer : BaseMapLayer
    {
        public string MachineName { get; private set; }
        public string DefaultMap { get; private set; }
        public string Channel { get; private set; }
        public string Version { get; private set; }

        private static string refererUrlConfig = "http://{0}/{1}";
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "5#")]
        protected GoogleServerBaseMapLayer(IConfigurationElement configuration)
            : base(configuration)
        {
            ArgumentValidation.CheckArgumentForNullOrEmpty(configuration["machineName"], "machineName");
            this.DefaultMap = (string.IsNullOrEmpty(configuration["defaultMap"])) ? "default_map" : configuration["defaultMap"];
            this.MachineName = configuration["machineName"];
            this.Channel = configuration["channel"];
            this.Version = configuration["version"];
            if (!string.IsNullOrEmpty(configuration["refererUrl"]))
                refererUrlConfig = configuration["refererUrl"];
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        protected void GetResponse(QuadKey key, string serverUrl, Stream stream)
        {
            var tile = key.ToTile();
            var request = (HttpWebRequest)WebRequest.Create(new Uri(string.Format(CultureInfo.InvariantCulture, serverUrl, tile.X, tile.Y, key.Level)));
            request.ServicePoint.ConnectionLimit = 50;
            request.Proxy = WebRequest.DefaultWebProxy;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.187 Safari/535.1";
            request.Timeout = 10 * 1000;
            request.ReadWriteTimeout = request.Timeout * 6;
            request.Referer = string.Format(CultureInfo.InvariantCulture, refererUrlConfig, this.MachineName, this.DefaultMap);
            request.KeepAlive = true;
            WebRequestToStream(request, stream, key);
        }
    }
}