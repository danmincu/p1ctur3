using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1ctur3.Storage.Flickr
{
    public class FlickrConfiguration: IFlickrConfiguration
    {
        public FlickrConfiguration(string key, string secret, string requestToken, string requestTokenSecret, string tokenVerifier)
        {
            this.Key = key;
            this.Secret = secret;
            this.RequestToken = requestToken;
            this.RequestTokenSecret = requestTokenSecret;
            this.TokenVerifier = tokenVerifier;
        }

        public string Key
        {
            get;
            private set;
        }

        public string Secret
        {
            get;
            private set;
        }

        public string RequestToken
        {
            get;
            private set;
        }

        public string RequestTokenSecret
        {
            get;
            private set;
        }

        public string TokenVerifier
        {
            get;
            private set;
        }
    }
}
