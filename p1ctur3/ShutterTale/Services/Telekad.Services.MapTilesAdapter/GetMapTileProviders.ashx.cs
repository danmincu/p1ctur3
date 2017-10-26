using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Telekad.Services.MapTilesAdapter
{
    /// <summary>
    /// Summary description for GetMapTileProviders
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GetMapTileProviders : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var mapProviders = Global.MapProviders;
            if (mapProviders == null)
                return;
            var providersSettings = new TileProvidersSettings() { Providers = mapProviders.Select(provider => new TileProvider() { Mode = provider.Name, MapType = provider.MapType}).ToList() };

            XmlSerializer serializer = new XmlSerializer(typeof(TileProvidersSettings));

            using (MemoryStream stream = new MemoryStream())
            {
                // serialize the settings
                serializer.Serialize(stream, providersSettings);
                byte[] data = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(data, 0, (int)stream.Length);

                // write out the settings to the response stream and set the content type
                context.Response.BinaryWrite(data);
                context.Response.ContentType = System.Net.Mime.MediaTypeNames.Text.Xml;
                context.Response.Flush();
                stream.Close();
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