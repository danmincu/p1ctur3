using P1ctur3.Exif;
using P1ctur3.Storage;
using P1ctur3.Storage.DataService;
using P1ctur3.Storage.Flickr;
using P1ctur3.Types;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telekad.Utils;
using Telekad.Ux;

namespace P1ctur3.Standalone.Models
{

    public class PhotoInfo : Selectable, IProgress<long>, ILogger
    {

        private DirectoryPhotoInfo parent;
        public PhotoInfo(DirectoryPhotoInfo parent)
        {
            this.parent = parent;
        }


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
                    //var key = System.Configuration.ConfigurationManager.AppSettings["FlickrKey"];
                    //var keySecret = System.Configuration.ConfigurationManager.AppSettings["FlickrKeySecret"];
                    //var requestToken = System.Configuration.ConfigurationManager.AppSettings["FlickrRequestToken"];
                    //var requestTokenSecret = System.Configuration.ConfigurationManager.AppSettings["FlickrRequestTokenSecret"];
                    //var tokenVerifier = System.Configuration.ConfigurationManager.AppSettings["FlickrTokenVerifier"];

                    var email = System.Configuration.ConfigurationManager.AppSettings["Email"];
                    var ri = this.MetadataStorage.Get(email);

                    var key = ri.Properties["FlickrKey"];
                    var keySecret = ri.Properties["FlickrKeySecret"];
                    var requestToken = ri.Properties["FlickrRequestToken"];
                    var requestTokenSecret = ri.Properties["FlickrRequestTokenSecret"];
                    var tokenVerifier = ri.Properties["FlickrTokenVerifier"];

                    var config = new FlickrConfiguration(key, keySecret, requestToken, requestTokenSecret, tokenVerifier);
                    cloudStorage = new ImageFlickrCloudStorage(config, this);
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
                    metadataStorage = new ImageMetadataDataServiceStorage(new Uri(MainWindow.StorageServiceUri), this);
                }
                return metadataStorage;
            }
        }


        #endregion


        public string Name
        {
            set { Set(() => Name, value); }
            get { return Get(() => Name); }
        }

        public long Size
        {
            set { Set(() => Size, value); }
            get { return Get(() => Size); }
        }
        public string SizeFormatted
        {
            get { return Telekad.Utils.FormatStringFunctions.FormatBytes(this.Size); }
        }

        public int PercentageUpload
        {
            set { Set(() => PercentageUpload, value); }
            get { return Get(() => PercentageUpload); }
        }


        public void Upload()
        {
            using (var fileStream = new FileStream(this.Name, FileMode.Open))
            {
                var memoryStream = new MemoryStream();
                fileStream.CopyTo(memoryStream);
                try
                {
                    var httpWr = (new Telekad.Utils.HttpUploader()).UploadDirectly(memoryStream, this.Name, this).Result as HttpWebResponse;

                    if (httpWr.StatusCode != HttpStatusCode.Created)
                    {
                        throw new System.Web.HttpException((int)httpWr.StatusCode, "An error occured during uploading. Please try again later.");
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format(CultureInfo.InvariantCulture, "An error occured during uploading. Please try again later.{0}", e.Message));
                }
            }
        }

        public void DirectUpload()
        {
            try { File.SetAttributes(this.Name, FileAttributes.Normal); }
            catch { };

            Retry.Do(() =>
            {
                using (var fileS = new FileStream(this.Name, FileMode.Open))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        fileS.CopyTo(memoryStream);
                        memoryStream.Position = 0;

                        var imageMetadata = this.ImageProcessor.Image(memoryStream, this.Name, null);
                        //Upload to flickr
                        memoryStream.Position = 0;

                        var name = Path.GetFileNameWithoutExtension(this.Name);
                        var individualFolderNames = Path.GetDirectoryName(this.Name).TrimEnd(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar).ToArray();
                        string tags = string.Empty;
                        if (individualFolderNames.Length > 1)
                            tags = individualFolderNames.Last();
                        if (individualFolderNames.Length > 2)
                            tags = string.Format(CultureInfo.InvariantCulture, "{0} {1}", tags, individualFolderNames[individualFolderNames.Length - 2]);

                        //TODO horrible iteration
                        if (individualFolderNames.Length > 3)
                            tags = string.Format(CultureInfo.InvariantCulture, "{0} {1}", tags, individualFolderNames[individualFolderNames.Length - 3]);

                        if (individualFolderNames.Length > 4)
                            tags = string.Format(CultureInfo.InvariantCulture, "{0} {1}", tags, individualFolderNames[individualFolderNames.Length - 4]);
                        //end horrible iteration

                        bool succeeded = false;
                        var exceptions = new List<Exception>();
                        Retry.DoRegardless(() =>
                        {                            
                            //im doing this copy stream biz because the Flickr API is closing the stream should an error occur
                            using (var safeStream = new MemoryStream())
                            {
                                memoryStream.Position = 0;
                                memoryStream.CopyTo(safeStream);
                                safeStream.Position = 0;
                                imageMetadata.StorageInfo = this.CloudStorage.Upload(safeStream, this.Name, name, "", tags, this);
                            }
                        }, TimeSpan.FromSeconds(20), 10, exceptions, out succeeded);



                        if (succeeded)
                        {
                            imageMetadata.FullPath = System.Environment.MachineName + "@" + Path.GetDirectoryName(this.Name);
                            imageMetadata.FileDateTime = (new FileInfo(this.Name)).CreationTimeUtc;
                            //imageMetadata.RemoteInfo = Factory.RemoteInfo;
                            //todo - this progress shall change to a operation 2/3 or something like that
                            this.Report(120);
                            bool saveSucceeded = false;
                            Retry.DoRegardless(() =>
                            {
                                //save metadata to database
                                //confirm or redirect or something                
                                this.MetadataStorage.Save(imageMetadata, System.Configuration.ConfigurationManager.AppSettings["Email"]);
                            }, TimeSpan.FromSeconds(20), 10, exceptions, out saveSucceeded);

                            if (saveSucceeded)
                                this.Report(130);
                            else
                                this.Report(0);
                        }
                        else
                            this.Report(0);

                        foreach (var exception in exceptions)
                        {
                            this.Write(exception);
                        }
                    }
                }
            }, TimeSpan.FromSeconds(1), 3);
        }

        public void Report(long value)
        {
            this.PercentageUpload = (int)((value * 100) / 130);
        }

        public override IEnumerable<Selectable> SelectableChildren
        {
            get { return null; }
        }

        public override Selectable SelectableParent
        {
            get
            {
                return this.parent;
            }
        }

        public void Write(string message)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(message);
        }

        public void Write(Exception exception)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ExceptionUtils.CreateExceptionString(exception));
        }
    }
}
