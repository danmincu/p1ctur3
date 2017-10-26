using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using System.Web;
using System.Windows.Media.Imaging;
using Telekad.Mapping;
using System.Linq;
using System.Collections.Generic;
using Telekad.Services.MapTilesAdapter.Bing;
using Telekad.Services.MapTilesAdapter.Debug;


namespace Telekad.Services.MapTilesAdapter
{
    /// <summary>
    /// Summary description for TileHandler
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MapTilesHandler : IHttpHandler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public void ProcessRequest(HttpContext context)
        {
            var key = context.Request.QueryString["key"];
            if (string.IsNullOrEmpty(key))
                return;
            var quadKey = new QuadKey(key);
            var mode = context.Request.QueryString["mode"];
            //todo - pass the mapproviders differently to make this class more testable
            var mapProvider = mode == null ? Global.MapProviders.FirstOrDefault() :
                 Global.MapProviders.Where(provider => provider.Name.ToLowerInvariant() == mode.ToLowerInvariant()).FirstOrDefault();
            if (mapProvider == null)
                return;
            var providers = mapProvider.Layers;
            using (var memoryStream = new MemoryStream())
            {
                //todo - asParallel
                var images = providers.Select(provider => provider.GetBitmap(quadKey)).ToArray();
                var resized = ImageUtils.OverlapImages(images);
                var encoder = new PngBitmapEncoder();
                var frame = BitmapFrame.Create(resized);
                encoder.Frames.Add(frame);
                encoder.Save(memoryStream);
                context.Response.ContentType = "image/png";
                var buffer = new byte[memoryStream.Length];
                memoryStream.Seek(0, 0);
                memoryStream.Read(buffer, 0, buffer.Length);
                context.Response.BinaryWrite(buffer);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}