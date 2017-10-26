using P1ctur3.Exif;
using P1ctur3.Storage;
using P1ctur3.Storage.Flickr;
using P1ctur3.Storage.Sql;
using P1ctur3.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P1ctur3.Web.Controllers
{
    public class PhotoUploadController : Controller
    {
        //
        // GET: /PhotoUpload/

        public ActionResult Index()
        {
            return View();
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
            get {
                if (imageProcessor == null)
                {
                    imageProcessor = new ImageProcessor(this.ExifProcessor);
                }
                return imageProcessor; }
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
                    var config = new FlickrConfiguration(key,keySecret,requestToken,requestTokenSecret,tokenVerifier);
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

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Upload(UploadFileModel fileModel)//(HttpPostedFileWrapper photo)//
        {    
            //System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<P1ctur3.Storage.Sql.ImageMetadataContext>());
            //var enti = System.Data.Entity.Database.DefaultConnectionFactory;
            //using (var db = new P1ctur3.Storage.Sql.ImageMetadataContext())
            //{
            //    db.Database.Initialize(true);
            //}

            

            if (ModelState.IsValid)
            {
                if (fileModel != null && fileModel.Photo != null)
                {
                    // extract first part of metadata
                    var imageMetadata = this.ImageProcessor.Image(fileModel.Photo.InputStream, fileModel.Photo.FileName, Guid.NewGuid());
                    //Upload to flickr
                    fileModel.Photo.InputStream.Seek(0,0);
                    imageMetadata.StorageInfo = this.CloudStorage.Upload(fileModel.Photo.InputStream, fileModel.Photo.FileName, "", "", "", null);                    
                    //save metadata to database
                    //confirm or redirect or something                
                    this.MetadataStorage.Save(imageMetadata, "danemincu@yahoo.com");
                    return Redirect(imageMetadata.StorageInfo.OriginalUrl);
                }
                
                return RedirectToAction("Index");
            }

            return new ContentResult() { Content = ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage };
        }

    }

    public class UploadFileModel
    {
        //30 mega
        [FileSize(31457280)]
        [FileTypes("jpg,jpeg,png")]
        public HttpPostedFileWrapper Photo { get; set; }
    }

    public class FileSizeAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly int _maxSize;

        public FileSizeAttribute(int maxSize)
        {
            _maxSize = maxSize;
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            return _maxSize > (value as HttpPostedFileWrapper).ContentLength;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("The file size should not exceed {0}", _maxSize);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var returnVar = new ModelClientValidationRule
            {
                ErrorMessage = "File is too large",
                ValidationType = "maxfilesize",
            };
            returnVar.ValidationParameters.Add("size", _maxSize);
            yield return returnVar;
        }
    }

    public class FileTypesAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly List<string> _types;

        public FileTypesAttribute(string types)
        {
            _types = types.Split(',').ToList();
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            var fileExt = System.IO.Path.GetExtension((value as HttpPostedFileWrapper).FileName).Substring(1);
            return _types.Contains(fileExt, StringComparer.OrdinalIgnoreCase);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("Invalid file type. Only the following types {0} are supported.", String.Join(", ", _types));
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var returnVar = new ModelClientValidationRule
            {
                ErrorMessage = "Invalid file type. Only {0} are supported.",
                ValidationType = "filetypes",
            };
            returnVar.ValidationParameters.Add("types", string.Join(",", _types));
            yield return returnVar;
        }
    }

}
