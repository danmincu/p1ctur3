using P1ctur3.Types;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1ctur3.Storage.DataService
{
    public class ImageMetadataDataServiceStorage : IImageMetadataStorage
    {
        private Dictionary<string, P1ctur3.Storage.DataService.ImageMetadataServiceReference.RemoteInfo> registrations = new Dictionary<string, P1ctur3.Storage.DataService.ImageMetadataServiceReference.RemoteInfo>();

        private Uri serviceUri;
        private ILogger logger;
        public ImageMetadataDataServiceStorage(Uri serviceUri, ILogger logger)
        {
            this.serviceUri = serviceUri;
            this.logger = logger;
        }
        public void Save(P1ctur3.Types.ImageMetadata imageMetadata, string registeredEmail)
        {
            var imc = new ImageMetadataServiceReference.ImageMetadataContext(this.serviceUri);
            if (!this.registrations.ContainsKey(registeredEmail))
            {
                this.registrations.Add(registeredEmail, this.GetRemoteInfoProxy(registeredEmail));
            }
            var remoteInfo = this.registrations[registeredEmail];
            try
            {
                var imageMtd = FromImageMetadata(imageMetadata, remoteInfo);
                imc.AddToImages(imageMtd);
                imc.AttachTo("RemoteInfos", remoteInfo);
                var e = imc.Entities;
                imc.SetLink(imageMtd, "RemoteInfo", remoteInfo);
                imc.SaveChanges();
            }
            catch (DataServiceRequestException e)
            {
                if (e.InnerException is DataServiceClientException)
                {
                    var dsce = e.InnerException as DataServiceClientException;
                    if (this.logger != null)
                        logger.Write(dsce);
                }                
                else
                {
                    if (this.logger != null)
                        logger.Write(e);
                }
                throw;
            }
            catch (Exception ex)
            {
                if (this.logger != null)
                    logger.Write(ex);
                throw;
            }
        }

        private ImageMetadataServiceReference.ImageMetadata FromImageMetadata(P1ctur3.Types.ImageMetadata imageMetadata, P1ctur3.Storage.DataService.ImageMetadataServiceReference.RemoteInfo remoteInfo)
        {
            return new ImageMetadataServiceReference.ImageMetadata()
            {
                Id = imageMetadata.Id,
                TagsAsJson = imageMetadata.TagsAsJson,
                //RemoteInfoId = remoteInfo.RemoteInfoId,
                //RemoteInfo = remoteInfo,
                Latitude = imageMetadata.Latitude,
                Longitude = imageMetadata.Longitude,
                OriginalFileName = imageMetadata.OriginalFileName,
                DateTime = imageMetadata.DateTime,
                FileDateTime = imageMetadata.FileDateTime,
                ImportDateTime = imageMetadata.ImportDateTime,
                CollectionId = imageMetadata.CollectionId,
                FullPath = imageMetadata.FullPath,
                Thumb = imageMetadata.Thumb,
                StorageInfoAsJson = imageMetadata.StorageInfoAsJson
            };
        }

        public void Save(P1ctur3.Types.RemoteInfo remoteInfo)
        {
            throw new NotSupportedException("this class cannot create remote info entries");
            //var imc = new ImageMetadataServiceReference.ImageMetadataContext(this.serviceUri));
        }

        public bool IsEmailRegistered(string email)
        {
            return this.Get(email) != null;
        }

        public P1ctur3.Types.ImageMetadata Get(Guid imageMetadataId)
        {
            //untested
            var imc = new ImageMetadataServiceReference.ImageMetadataContext(this.serviceUri);
            var proxy = imc.Images.Where(ri => ri.Id == imageMetadataId).FirstOrDefault();
            return ImageMetadataFromProxy(proxy);
        }

        public IEnumerable<P1ctur3.Types.ImageMetadata> Get(IImageMetadataFilter filter)
        {
            if (filter.Email == null)
                return null;
            var imc = new ImageMetadataServiceReference.ImageMetadataContext(this.serviceUri);

            var lowerEmail = filter.Email.ToLowerInvariant();
            var proxyRi = imc.RemoteInfos.Where(ri => ri.Email == lowerEmail).FirstOrDefault();
            if (!string.IsNullOrEmpty(filter.DirectoryName))
            {
                return imc.Images.Where(ri => ri.RemoteInfoId == proxyRi.RemoteInfoId && ri.FullPath == System.Environment.MachineName + "@" + filter.DirectoryName).ToList().Select(p => ImageMetadataFromProxy(p));
            }
            
            return imc.Images.Where(ri => ri.RemoteInfoId == proxyRi.RemoteInfoId).ToList().Select(p => ImageMetadataFromProxy(p));            
        }

        private P1ctur3.Types.ImageMetadata ImageMetadataFromProxy(ImageMetadataServiceReference.ImageMetadata proxy)
        {
            return new Types.ImageMetadata()
            {
                Id = proxy.Id,
                TagsAsJson = proxy.TagsAsJson,
                Latitude = proxy.Latitude,
                Longitude = proxy.Longitude,
                OriginalFileName = proxy.OriginalFileName,
                RemoteInfo = proxy.RemoteInfo == null ? null : RemoteInfoFromProxy(proxy.RemoteInfo),
                StorageInfoAsJson = proxy.StorageInfoAsJson == null ? null : proxy.StorageInfoAsJson,
                DateTime = proxy.DateTime,
                FileDateTime = proxy.FileDateTime,
                ImportDateTime = proxy.ImportDateTime
            };
        }


        public P1ctur3.Types.RemoteInfo Get(string email)
        {
            var imc = new ImageMetadataServiceReference.ImageMetadataContext(this.serviceUri);
            var lowerEmail = email.ToLowerInvariant();
            var proxy = imc.RemoteInfos.Where(ri => ri.Email == lowerEmail).FirstOrDefault();
            return proxy == null ? null : RemoteInfoFromProxy(proxy);
        }

        private ImageMetadataServiceReference.RemoteInfo GetRemoteInfoProxy(string email)
        {
            var imc = new ImageMetadataServiceReference.ImageMetadataContext(this.serviceUri);
            var lowerEmail = email.ToLowerInvariant();
            return imc.RemoteInfos.Where(ri => ri.Email == lowerEmail).FirstOrDefault();
        }


        private Types.RemoteInfo RemoteInfoFromProxy(ImageMetadataServiceReference.RemoteInfo proxy)
        {
            return new Types.RemoteInfo(proxy.RemoteInfoId) { Email = proxy.Email, PropertiesAsJson = proxy.PropertiesAsJson, Type = proxy.Type, Version = proxy.Version, ImageMetadatas = null };
        }

    }
}
