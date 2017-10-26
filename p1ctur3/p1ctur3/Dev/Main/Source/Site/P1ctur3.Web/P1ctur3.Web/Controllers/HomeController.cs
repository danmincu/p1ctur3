using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace P1ctur3.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "P1ctur3.com [Telekad 2014]";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        static Flickr myFlickr;
        static OAuthRequestToken requestToken;

        public ActionResult FlickrAccount(string key, string keySecret)
        {
            //var key = System.Configuration.ConfigurationManager.AppSettings["FlickrKey"];
            //var keySecret = System.Configuration.ConfigurationManager.AppSettings["FlickrKeySecret"];
            myFlickr = new FlickrNet.Flickr(key, keySecret);
            requestToken = myFlickr.OAuthGetRequestToken("oob");
            string url = myFlickr.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Write | AuthLevel.Read | AuthLevel.Delete);
            this.ViewBag.AuthUrl = url;
            return View();
        }

        public ActionResult FlickrAuthorize(string tokenVerifier)
        {

            var accessToken = myFlickr.OAuthGetAccessToken(requestToken, tokenVerifier);

            try
            {
                var config = WebConfigurationManager.OpenWebConfiguration("~");
                config.AppSettings.Settings.Remove("FlickrKey");
                config.AppSettings.Settings.Add("FlickrKey", myFlickr.ApiKey);

                config.AppSettings.Settings.Remove("FlickrKeySecret");
                config.AppSettings.Settings.Add("FlickrKeySecret", myFlickr.ApiSecret);

                config.AppSettings.Settings.Remove("FlickrRequestToken");
                config.AppSettings.Settings.Add("FlickrRequestToken", accessToken.Token);

                config.AppSettings.Settings.Remove("FlickrRequestTokenSecret");
                config.AppSettings.Settings.Add("FlickrRequestTokenSecret", accessToken.TokenSecret);

                config.AppSettings.Settings.Remove("FlickrTokenVerifier");
                config.AppSettings.Settings.Add("FlickrTokenVerifier", tokenVerifier);
                config.Save();
            }
            catch
            {
                //burry
            };

            this.ViewBag.Key = myFlickr.ApiKey;
            this.ViewBag.KeySecret = myFlickr.ApiSecret;
            this.ViewBag.Token = accessToken.Token;
            this.ViewBag.TokenSecret = accessToken.TokenSecret;
            this.ViewBag.TokenVerifier = tokenVerifier;
            return View();

        }



    }
}
