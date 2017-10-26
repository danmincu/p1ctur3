using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows.Media.Imaging;
using Telekad.Mapping;

namespace Telekad.Services.MapTilesAdapter
{
    public abstract class BaseMapLayer : IMapLayer
    {
        protected BaseMapLayer(IConfigurationElement configuration)
        {
            this.Name = configuration["name"];
            this.CacheTilesFolder = configuration["cacheTilesFolder"];
        }

        protected abstract void GetLayer(QuadKey key, Stream stream);

        public BitmapImage GetBitmap(QuadKey key)
        {
            Stream stream;
            if (this.CacheTiles)
            {
                var filePath = Path.Combine(new[] { this.CacheTilesFolder, string.Format(CultureInfo.InvariantCulture, "{0}.png", key) });
                if (File.Exists(filePath))
                {
                    return new BitmapImage(new Uri(filePath, UriKind.Absolute));
                }
                stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }
            else
            {
                stream = new MemoryStream();
            }

            using (stream)
            {
                this.GetLayer(key, stream);
                stream.Seek(0, 0);
                var result = new BitmapImage();
                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.UriSource = null;
                result.StreamSource = stream;
                result.EndInit();
                stream.Close();
                return result;
            }
        }

        public static void WebRequestToStream(WebRequest request, Stream stream, QuadKey key)
        {
#if DEBUG
            System.Diagnostics.Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0};{1};{2}",DateTime.Now.Ticks, key, request.RequestUri));
#endif
            using (WebResponse wr = request.GetResponse())
            {
                using (var responseStream = wr.GetResponseStream())
                {
                    if (responseStream == null)
                        return;
                    responseStream.CopyTo(stream);
                    if (stream.CanSeek)
                        stream.Seek(0, 0);
                }
            }
        }

        public string Name { get; set; }

        public bool CacheTiles
        {
            get { return !string.IsNullOrEmpty(this.CacheTilesFolder); }
        }

        public string CacheTilesFolder
        {
            get;
            set;
        }
    }
}