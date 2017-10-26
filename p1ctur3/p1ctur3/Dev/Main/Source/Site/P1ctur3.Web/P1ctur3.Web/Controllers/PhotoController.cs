using P1ctur3.Exif;
using P1ctur3.Storage;
using P1ctur3.Storage.Flickr;
using P1ctur3.Storage.Sql;
using P1ctur3.Types;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace P1ctur3.Web.Controllers
{
    public class PhotoController : ApiController
    {


        #region Temporary factories (to be replaced with Unity)
        private IExifProcessor exifProcessor;
        private IExifProcessor ExifProcessor
        {
            get
            {
                if (exifProcessor == null)
                    exifProcessor = new ExifProcessor();
                return exifProcessor;
            }
        }

        private ImageProcessor imageProcessor;
        private ImageProcessor ImageProcessor
        {
            get
            {
                if (imageProcessor == null)
                {
                    imageProcessor = new ImageProcessor(this.ExifProcessor);
                }
                return imageProcessor;
            }
        }


        private IImageCloudStorage cloudStorage;
        private IImageCloudStorage CloudStorage
        {
            get
            {
                if (cloudStorage == null)
                {
                    var key = System.Configuration.ConfigurationManager.AppSettings["FlickrKey"];
                    var keySecret = System.Configuration.ConfigurationManager.AppSettings["FlickrKeySecret"];
                    var requestToken = System.Configuration.ConfigurationManager.AppSettings["FlickrRequestToken"];
                    var requestTokenSecret = System.Configuration.ConfigurationManager.AppSettings["FlickrRequestTokenSecret"];
                    var tokenVerifier = System.Configuration.ConfigurationManager.AppSettings["FlickrTokenVerifier"];
                    var config = new FlickrConfiguration(key, keySecret, requestToken, requestTokenSecret, tokenVerifier);
                    cloudStorage = new ImageFlickrCloudStorage(config, null);
                }
                return cloudStorage;
            }
        }


        private IImageMetadataStorage metadataStorage;

        private IImageMetadataStorage MetadataStorage
        {
            get
            {
                if (metadataStorage == null)
                {
                    metadataStorage = new ImageMetadataSqlStorage();
                }
                return metadataStorage;
            }
        }


        #endregion


        public HttpResponseMessage Post([FromUri]string id)
        {
            var fullFilePath = Telekad.Utils.Base64.Base64Decode(id);
            var fileName = Path.GetFileName(fullFilePath);

            var task = this.Request.Content.ReadAsStreamAsync();
            task.Wait();
            Stream requestStream = task.Result;

            try
            {
                //Stream fileStream = File.Create(HttpContext.Current.Server.MapPath("~/" + fileName));
                //requestStream.CopyTo(fileStream);
                //fileStream.Close();

                var imageMetadata = this.ImageProcessor.Image(requestStream, fullFilePath, Guid.NewGuid());
                //Upload to flickr
                requestStream.Seek(0, 0);

                var name = Path.GetFileNameWithoutExtension(fullFilePath);
                var individualFolderNames = Path.GetDirectoryName(fullFilePath).TrimEnd(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar).ToArray();
                string tags = string.Empty;
                if (individualFolderNames.Length > 1)
                 tags = individualFolderNames.Last();
                if (individualFolderNames.Length > 2)
                    tags = string.Format(CultureInfo.InvariantCulture, "{0} {1}", tags, individualFolderNames[individualFolderNames.Length - 2]);

                imageMetadata.StorageInfo = this.CloudStorage.Upload(requestStream , fullFilePath, name, "", tags, null);
                //save metadata to database
                //confirm or redirect or something                
                this.MetadataStorage.Save(imageMetadata, "danemincu@yahoo.com");
                requestStream.Close();
            }
            catch (IOException)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.Created;
            return response;
        }


        public HttpResponseMessage Get([FromUri]string id)
        {
            var fullFilePath = Telekad.Utils.Base64.Base64Decode(id);
            var fileName = Path.GetFileName(fullFilePath);

            string path = HttpContext.Current.Server.MapPath("~/" + fileName);
            if (!File.Exists(path))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            try
            {
                MemoryStream responseStream = new MemoryStream();
                Stream fileStream = File.Open(path, FileMode.Open);
                bool fullContent = true;
                if (this.Request.Headers.Range != null)
                {
                    fullContent = false;

                    // Currently we only support a single range.
                    RangeItemHeaderValue range = this.Request.Headers.Range.Ranges.First();


                    // From specified, so seek to the requested position.
                    if (range.From != null)
                    {
                        fileStream.Seek(range.From.Value, SeekOrigin.Begin);

                        // In this case, actually the complete file will be returned.
                        if (range.From == 0 && (range.To == null || range.To >= fileStream.Length))
                        {
                            fileStream.CopyTo(responseStream);
                            fullContent = true;
                        }
                    }
                    if (range.To != null)
                    {
                        // 10-20, return the range.
                        if (range.From != null)
                        {
                            long? rangeLength = range.To - range.From;
                            int length = (int)Math.Min(rangeLength.Value, fileStream.Length - range.From.Value);
                            byte[] buffer = new byte[length];
                            fileStream.Read(buffer, 0, length);
                            responseStream.Write(buffer, 0, length);
                        }
                        // -20, return the bytes from beginning to the specified value.
                        else
                        {
                            int length = (int)Math.Min(range.To.Value, fileStream.Length);
                            byte[] buffer = new byte[length];
                            fileStream.Read(buffer, 0, length);
                            responseStream.Write(buffer, 0, length);
                        }
                    }
                    // No Range.To
                    else
                    {
                        // 10-, return from the specified value to the end of file.
                        if (range.From != null)
                        {
                            if (range.From < fileStream.Length)
                            {
                                int length = (int)(fileStream.Length - range.From.Value);
                                byte[] buffer = new byte[length];
                                fileStream.Read(buffer, 0, length);
                                responseStream.Write(buffer, 0, length);
                            }
                        }
                    }
                }
                // No Range header. Return the complete file.
                else
                {
                    fileStream.CopyTo(responseStream);
                }
                fileStream.Close();
                responseStream.Position = 0;

                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = fullContent ? HttpStatusCode.OK : HttpStatusCode.PartialContent;
                response.Content = new StreamContent(responseStream);
                return response;
            }
            catch (IOException)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

    }
}