﻿using System;
using System.Text;
using System.Collections.Generic;

using NUnit.Framework;
using FlickrNet;
using System.IO;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for CacheTests
    /// </summary>
    [TestFixture]
    public class CacheTests
    {
        [Test]
        public void CacheLocationTest()
        {
            string origLocation = Flickr.CacheLocation;

            Console.WriteLine(origLocation);

            string newLocation = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            Flickr.CacheLocation = newLocation;

            Assert.AreEqual(Flickr.CacheLocation, newLocation);

            Flickr.CacheLocation = origLocation;

            Assert.AreEqual(Flickr.CacheLocation, origLocation);

        }

        [Test]
        public void CacheHitTest()
        {
            Directory.Delete(Flickr.CacheLocation, true);

            Flickr f = TestData.GetInstance();
            Flickr.FlushCache();
            f.InstanceCacheDisabled = false;

            f.PeopleGetPublicPhotos(TestData.TestUserId);

            string lastUrl = f.LastRequest;

            ICacheItem item = Cache.Responses.Get(lastUrl, TimeSpan.MaxValue, false);

            Assert.IsNotNull(item, "Cache should now contain the item.");
            Assert.IsInstanceOf<ResponseCacheItem>(item);

            ResponseCacheItem response = item as ResponseCacheItem;

            Assert.IsNotNull(response.Url, "Url should not be null.");
            Assert.AreEqual(lastUrl, response.Url.AbsoluteUri, "Url should match the url requested from the cache.");
        }
    }
}
