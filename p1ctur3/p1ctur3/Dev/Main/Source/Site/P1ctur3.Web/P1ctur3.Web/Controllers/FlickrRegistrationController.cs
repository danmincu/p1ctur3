using FlickrNet;
using P1ctur3.Storage.Sql;
using P1ctur3.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace P1ctur3.Web.Controllers
{
    
    public class FlickrRegistrationController : Controller
    {

        
        static Dictionary<string, Tuple<Flickr, OAuthRequestToken>> Registrations()
        {
            if (registrations == null)
              registrations = new Dictionary<string, Tuple<Flickr, OAuthRequestToken>>();
            return registrations;
        }
        
        static Dictionary<string, Tuple<Flickr, OAuthRequestToken>> registrations;

        //
        // GET: /FlickrRegistration/

        public ActionResult Index()
        {
            return View();
        }


        //static Flickr myFlickr;
        //static OAuthRequestToken requestToken;

        public ActionResult FlickrAccount(string email, string key, string keySecret)
        {
            if (email == null || key.Length < 30 || keySecret.Length < 15)
                return new ContentResult() { Content = "Invalid email or key or secret" };

            var metadataStorage = new ImageMetadataSqlStorage();

            if (metadataStorage.IsEmailRegistered(email))
                return new ContentResult() { Content = "Email already registered." };

            if (Registrations().ContainsKey(email.ToLowerInvariant()))
            {
                Registrations().Remove(email.ToLowerInvariant());
            }

            var myFlickr = new FlickrNet.Flickr(key, keySecret);
            var requestToken = myFlickr.OAuthGetRequestToken("oob");
            registrations.Add(email.ToLowerInvariant(), new Tuple<Flickr, OAuthRequestToken>(myFlickr, requestToken));
            //var key = System.Configuration.ConfigurationManager.AppSettings["FlickrKey"];
            //var keySecret = System.Configuration.ConfigurationManager.AppSettings["FlickrKeySecret"];            
            string url = myFlickr.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Write | AuthLevel.Read | AuthLevel.Delete);
            this.ViewBag.AuthUrl = url;
            this.ViewBag.Email = email.ToLowerInvariant();
            return View();
        }

        public ActionResult FlickrAuthorize(string email, string tokenVerifier)
        {
            if (email == null || tokenVerifier.Length < 10)
                return new ContentResult() { Content = "Invalid email or tokenVerifier" };

            if (Registrations()[email] == null)
                return new ContentResult() { Content = "session was reset; cant find the flickr registration" };
            var myFlickr = Registrations()[email].Item1;
            var requestToken = Registrations()[email].Item2;
            
            var accessToken = myFlickr.OAuthGetAccessToken(requestToken, tokenVerifier);


            var metadataStorage = new ImageMetadataSqlStorage();
            var remoteInfo = new RemoteInfo(Guid.NewGuid()) { Email = email, Type = "Flickr", Version = "3.7.0" };
            remoteInfo.Properties.Add("FlickrKey", myFlickr.ApiKey);
            remoteInfo.Properties.Add("FlickrKeySecret", myFlickr.ApiSecret);
            remoteInfo.Properties.Add("FlickrRequestToken", accessToken.Token);
            remoteInfo.Properties.Add("FlickrRequestTokenSecret", accessToken.TokenSecret);
            remoteInfo.Properties.Add("FlickrTokenVerifier", tokenVerifier);
            metadataStorage.Save(remoteInfo);
            
            //try
            //{
            //    var config = WebConfigurationManager.OpenWebConfiguration("~");
            //    config.AppSettings.Settings.Remove("FlickrKey");
            //    config.AppSettings.Settings.Add("FlickrKey", myFlickr.ApiKey);

            //    config.AppSettings.Settings.Remove("FlickrKeySecret");
            //    config.AppSettings.Settings.Add("FlickrKeySecret", myFlickr.ApiSecret);

            //    config.AppSettings.Settings.Remove("FlickrRequestToken");
            //    config.AppSettings.Settings.Add("FlickrRequestToken", accessToken.Token);

            //    config.AppSettings.Settings.Remove("FlickrRequestTokenSecret");
            //    config.AppSettings.Settings.Add("FlickrRequestTokenSecret", accessToken.TokenSecret);

            //    config.AppSettings.Settings.Remove("FlickrTokenVerifier");
            //    config.AppSettings.Settings.Add("FlickrTokenVerifier", tokenVerifier);
            //    config.Save();
            //}
            //catch
            //{
            //    //burry
            //};

            this.ViewBag.Key = myFlickr.ApiKey;
            this.ViewBag.KeySecret = myFlickr.ApiSecret;
            this.ViewBag.Token = accessToken.Token;
            this.ViewBag.TokenSecret = accessToken.TokenSecret;
            this.ViewBag.TokenVerifier = tokenVerifier;
            this.ViewBag.Email = email;
            return View();

        }


    }
}
