using FlickrNet;
using P1ctur3.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1ctur3.Storage.Flickr
{
    public class ImageFlickrCloudStorage : IImageCloudStorage
    {
        private IFlickrConfiguration configuration;
        private ILogger logger;
        public ImageFlickrCloudStorage(IFlickrConfiguration config, ILogger logger)
        {
            this.configuration = config;
            this.logger = logger;

        }

        private FlickrNet.Flickr flickr;
        private FlickrNet.Flickr Flickr
        {
            get
            {
                if (flickr == null)
                {                    
                    this.flickr = new FlickrNet.Flickr(this.configuration.Key, this.configuration.Secret) {HttpTimeout = (int)TimeSpan.FromMinutes(30).TotalMilliseconds};                    
                    this.flickr.InstanceCacheDisabled = true;

                }
                return flickr;
            }
        }

        public P1ctur3.Types.RemoteStorageInfo Upload(System.IO.Stream stream, string fileName, string title, string description, string tags, IProgress<long> progress)
        {
            try
            {
                using (stream)
                {
                    //var accessToken = this.Flickr.OAuthGetAccessToken("72157633939557148-5055405cdf089fc5", "402a0be8ed9dbe1f", "440-205-244");
                    var accessToken = this.Flickr.OAuthGetAccessToken(this.configuration.RequestToken, this.configuration.RequestTokenSecret, this.configuration.TokenVerifier);

                    this.Flickr.OAuthAccessTokenSecret = accessToken.TokenSecret;
                    this.Flickr.OAuthAccessToken = accessToken.Token;

                    var action = new EventHandler<UploadProgressEventArgs>(delegate(Object o, UploadProgressEventArgs ea)
                    {
                        progress.Report(ea.ProcessPercentage);
                    });

                    if (progress != null)
                        this.flickr.OnUploadProgress += action;
                                        
                    var photoId = this.Flickr.UploadPicture(stream, fileName, title, description, tags, false, false, false, ContentType.Photo, SafetyLevel.None, HiddenFromSearch.Visible);
                    //string photoId = null;
                    //this.Flickr.UploadPictureAsync(stream, fileName, title, description, tags, false, false, false, ContentType.Photo, SafetyLevel.None, HiddenFromSearch.Visible, 
                    //    r => photoId = r.Result);

                    if (progress != null)
                        this.flickr.OnUploadProgress -= action;

                    var remoteStorageProperties = new Dictionary<string, string>();
                    remoteStorageProperties.Add("PhotoId", photoId);
                    var picture = this.Flickr.PhotosGetInfo(photoId);
                    remoteStorageProperties.Add("OriginalSecret", picture.OriginalSecret);
                    remoteStorageProperties.Add("Secret", picture.Secret);
                    remoteStorageProperties.Add("OwnerUserId", picture.OwnerUserId);
                    remoteStorageProperties.Add("Server", picture.Server);
                    remoteStorageProperties.Add("Farm", picture.Farm);
                    remoteStorageProperties.Add("OriginalFormat", picture.OriginalFormat);

                    if (progress != null)
                        progress.Report(110);

                    return new Types.RemoteStorageInfo()
                    {
                        Type = "Flickr",
                        Version = "3.7.0",
                        Properties = remoteStorageProperties
                    };
                }
            }
            catch(Exception e)
            {
                if (this.logger != null)
                    logger.Write(e);
                throw;
            }
        }


    }
}
