using System;
using System.Linq;
using Evasearch.Ranges;
using Evasearch.Structures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telekad.Mapping;
using P1ctur3.Storage;
using System.Collections.Generic;
using P1ctur3.Types;


namespace Evasearch.Tests
{
    [TestClass]
    public class LoadTest
    {
        //public static string StorageServiceUri = "http://ownmeca0.w15.wh-2.com/p1ctur3/Services/P1ctur3DataService.svc/";
        public static string StorageServiceUri = "http://i7/P1ctur3.Web/Services/P1ctur3DataService.svc/";


        [ClassInitialize()]      
        public static void ClassInit(TestContext context)
        {
            LoadDataFromCloud();
        }

        static IEnumerable<ImageMetadata> cloudCache;
        public static IEnumerable<ImageMetadata> LoadDataFromCloud()
        {
            if (cloudCache == null)
            {
                var imsr = new P1ctur3.Storage.DataService.ImageMetadataDataServiceStorage(new Uri(StorageServiceUri), null);
                cloudCache = imsr.Get(new IImageMetadataFilter() { Email = "danemincu@yahoo.com" })./*Take(10000).*/ToList();
            }
            return cloudCache;
        }

        public TimeStructure DateTimeSubject(IEnumerable<ImageMetadata> data)
        {
            var result = new TimeStructure(5);
            var ticks = DateTime.Now.Ticks;
            System.Diagnostics.Trace.WriteLine("Tree creation start");
            long i = 0;
            //var r = new Random(23141);
            foreach (var item in data)
            {
                result.Add(1, new DateTimeWrapper(item.BestDate), i++);
                //result.Add(1, new DateTimeWrapper(item.BestDate + TimeSpan.FromDays(r.Next(1000) + 1)), i++);
                //result.Add(1, new DateTimeWrapper(item.BestDate + TimeSpan.FromDays(r.Next(1000) + 1)), i++);
                //result.Add(1, new DateTimeWrapper(item.BestDate + TimeSpan.FromDays(r.Next(1000) + 1)), i++);
                //result.Add(1, new DateTimeWrapper(item.BestDate + TimeSpan.FromDays(r.Next(1000) + 1)), i++);
                //result.Add(1, new DateTimeWrapper(item.BestDate + TimeSpan.FromDays(r.Next(1000) + 1)), i++);
                //result.Add(1, new DateTimeWrapper(item.BestDate + TimeSpan.FromDays(r.Next(1000) + 1)), i++);
                //result.Add(1, new DateTimeWrapper(item.BestDate + TimeSpan.FromDays(r.Next(1000) + 1)), i++);
                //result.Add(1, new DateTimeWrapper(item.BestDate + TimeSpan.FromDays(r.Next(1000) + 1)), i++);
                //result.Add(1, new DateTimeWrapper(item.BestDate + TimeSpan.FromDays(r.Next(1000) + 1)), i++);
                //result.Add(1, new DateTimeWrapper(item.BestDate + TimeSpan.FromDays(r.Next(1000) + 1)), i++);
                //result.Add(1, new DateTimeWrapper(item.BestDate + TimeSpan.FromDays(r.Next(1000) + 1)), i++);
                //result.Add(1, new DateTimeWrapper(item.BestDate + TimeSpan.FromDays(r.Next(1000) + 1)), i++);
            }
            var afterTicks = DateTime.Now.Ticks;
            System.Diagnostics.Trace.WriteLine(TimeSpan.FromTicks(afterTicks - ticks).TotalSeconds);

            return result;
        }

        public LocationStructure LocationSubject(IEnumerable<ImageMetadata> data)
        {
            var result = new LocationStructure(18);

            var ticks = DateTime.Now.Ticks;
            System.Diagnostics.Trace.WriteLine("Tree creation start");

            long i = 0;
            foreach (var item in data.Where(a => a.Coordinate != null))
            {
                result.Add(1, item.Coordinate, i++);
            }

            var afterTicks = DateTime.Now.Ticks;
            System.Diagnostics.Trace.WriteLine(string.Format("Durration to create the search tree {0} seconds",TimeSpan.FromTicks(afterTicks - ticks).TotalSeconds));

            return result;
        }

        
        [TestMethod]
        public void DateTimeSubjectSimpleTest()
        {
            var subject = DateTimeSubject(cloudCache);            
            System.Diagnostics.Trace.WriteLine(subject.Count(1).ToString());
            int totalCount = 0;
            foreach (var item in subject.ForEach())
            {
                //System.Diagnostics.Trace.WriteLine(item.ToString());
                totalCount++;
            }
            
            Assert.AreEqual(totalCount, 67338);
        }

        [TestMethod]
        public void DateTimeSubjectAggregateTest()
        {
            var subject = DateTimeSubject(cloudCache);
            System.Diagnostics.Trace.WriteLine(subject.Count(1).ToString());

            var buckets = subject.Search(new List<DateTimeRange>() { new DateTimeRange(new DateTime(2011, 6, 23), 1)  }).ToList();
            var bucket = buckets.First();
                        

            var aggregations = subject.Aggregations(1, bucket);

            foreach (var item in aggregations)
            {               
                System.Diagnostics.Trace.WriteLine(string.Format("{0} -> {1}", item.Key, item.Value));    
            }
        }

        [TestMethod]
        public void SimpleLocationSubjectTest()
        {
            var subject = LocationSubject(cloudCache);
            System.Diagnostics.Trace.WriteLine(subject.Count(1).ToString());
            int totalCount = 0;
            foreach (var item in subject.ForEach())
            {
               // System.Diagnostics.Trace.WriteLine(item.ToString());
                totalCount++;
            }

            var interesting = subject.Buckets.Last().DocumentIds(1).First();
            var imageOfInterest = cloudCache.Where(a => a.Coordinate != null).ToArray()[interesting];
            var url = imageOfInterest.StorageInfo.GetUrl("c");

            Assert.AreEqual(totalCount, 6870);
        }


        [TestMethod]
        public void SimpleLocationSubjectRangeSearchTest()
        {
            var subject = LocationSubject(cloudCache);
            IEnumerable<Telekad.Types.IRangeExtended<Coordinate>> quadkeyList = new List<Telekad.Types.IRangeExtended<Coordinate>>() { new QuadKey("0302") };
            var ticks = DateTime.Now.Ticks;
            var buckets = subject.Search(quadkeyList);
            var afterTicks = DateTime.Now.Ticks;
            System.Diagnostics.Trace.WriteLine(string.Format("Durration to search one quadkey {0} ms",TimeSpan.FromTicks(afterTicks - ticks).TotalMilliseconds));
            Assert.IsTrue(buckets.Any());
            foreach (var item in buckets)
            {
                System.Diagnostics.Trace.WriteLine(item);
                //foreach (var did in item.DocumentIds(1))
                //{
                //    System.Diagnostics.Trace.Write(string.Format("{0};",did));
                //}                    
            }
        }

                
        [TestMethod]
        public void SimpleLocationAggregateTest()
        {
            var subject = LocationSubject(cloudCache);
            IEnumerable<Telekad.Types.IRangeExtended<Coordinate>> quadkeyList = new List<Telekad.Types.IRangeExtended<Coordinate>>() { new QuadKey("0302") };
            var ticks = DateTime.Now.Ticks;
            var buckets = subject.Search(quadkeyList);
            var afterTicks = DateTime.Now.Ticks;
            System.Diagnostics.Trace.WriteLine(string.Format("Durration to search one quadkey {0} ms",TimeSpan.FromTicks(afterTicks - ticks).TotalMilliseconds));
            Assert.IsTrue(buckets.Any());
            foreach (var item in buckets)
            {
                System.Diagnostics.Trace.WriteLine(item);
                //foreach (var did in item.DocumentIds(1))
                //{
                //    System.Diagnostics.Trace.Write(string.Format("{0};",did));
                //}                    
            }

            var averageCoordinate = subject.Aggregations(1, buckets.First()).Values.First();
            Assert.AreEqual(averageCoordinate.Latitude, 45.3249538773457, 0.0000001);
            Assert.AreEqual(averageCoordinate.Longitude, -75.7105467099146, 0.0000001);         
        }


        [TestMethod]
        public void SimpleLocationCoordinateSearchTest()
        {
            var subject = LocationSubject(cloudCache);
            
            var ticks = DateTime.Now.Ticks;
            //var buckets = subject.Search(new List<Coordinate>() { new Coordinate(45.3249538773457, -75.7105467099146) });
            var buckets = subject.Search(new List<Coordinate>() { new Coordinate(45.2835806, -75.8827955) });
            var afterTicks = DateTime.Now.Ticks;
            System.Diagnostics.Trace.WriteLine(string.Format("Durration to search one quadkey {0} ms", TimeSpan.FromTicks(afterTicks - ticks).TotalMilliseconds));
            Assert.IsTrue(buckets.Any());
            foreach (var item in buckets)
            {
                System.Diagnostics.Trace.WriteLine(item);
                //foreach (var did in item.DocumentIds(1))
                //{
                //    System.Diagnostics.Trace.Write(string.Format("{0};",did));
                //}                    
            }        
        }
    }
}
