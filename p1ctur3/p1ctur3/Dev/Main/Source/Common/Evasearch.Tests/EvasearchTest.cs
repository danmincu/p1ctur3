using System;
using System.Linq;
using Evasearch.Ranges;
using Evasearch.Structures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telekad.Mapping;
using System.Collections.Generic;

namespace Evasearch.Tests
{
    [TestClass]
    public class EvasearchTest
    {
        public TimeStructure DateTimeSubject()
        {
            var result = new TimeStructure(5);
            
            var date1 = new DateTime(2013,6,13,10,10,13);
            var date2 = new DateTime(2013,6,13,10,10,10);
            var date3 = new DateTime(1972,6,13,10,10,0);
            var date4 = new DateTime(1987,2,23,23,40,0);
            var date5 = new DateTime(2006, 2, 2, 5, 13, 0);
            var date6 = new DateTime(2014, 2, 7, 19, 8, 0);

            result.Add(1, new DateTimeWrapper(date1), 1);
            result.Add(1, new DateTimeWrapper(date2), 2);
            result.Add(1, new DateTimeWrapper(date3), 3);
            result.Add(1, new DateTimeWrapper(date4), 4);
            result.Add(1, new DateTimeWrapper(date5), 5);
            result.Add(1, new DateTimeWrapper(date6), 6);

            return result;        
        }

        public LocationStructure LocationSubject()
        {
            var result = new LocationStructure(5);

            var coordinate1 = new Coordinate(45, -75);
            var coordinate2 = new Coordinate(45.004, -75.0020);
            var coordinate3 = new Coordinate(43, -72);
            var coordinate4 = new Coordinate(-45, -75);
            var coordinate5 = new Coordinate(46, -72);
            var coordinate6 = new Coordinate(23, -55);

            result.Add(1, coordinate1, 1);
            result.Add(1, coordinate2, 2);
            result.Add(1, coordinate3, 3);
            result.Add(1, coordinate4, 4);
            result.Add(1, coordinate5, 5);
            result.Add(1, coordinate6, 6);

            return result;
        }


        [TestMethod]
        public void SimpleDateTimeSubjectTest()
        {
            var subject = DateTimeSubject();
            int totalCount = 0;
            foreach (var item in subject.ForEach())
            {
                System.Diagnostics.Trace.WriteLine(item.ToString());
                totalCount++;
            }
            Assert.AreEqual(totalCount, 31);
        }


        [TestMethod]
        public void SimpleLocationSubjectTest()
        {
            var subject = LocationSubject();
            int totalCount = 0;
            foreach (var item in subject.ForEach())
            {
                System.Diagnostics.Trace.WriteLine(item.ToString());
                totalCount++;
            }
            Assert.AreEqual(totalCount, 18);
        }


        [TestMethod]
        public void SimpleLocationSubjectSearchTest()
        {
            var subject = LocationSubject();
             var buckets = subject.Search(new List<Coordinate>() { new Coordinate(45.004, -75.0020) });

             foreach (var item in buckets)
             {
                 System.Diagnostics.Trace.WriteLine(item);
             }

             Assert.IsTrue(buckets.Any());
        }


        [TestMethod]
        public void SimpleLocationSubjectRangeSearchTest()
        {
            var subject = LocationSubject();        
            IEnumerable<Telekad.Types.IRangeExtended<Coordinate>> quadkeyList = new List<Telekad.Types.IRangeExtended<Coordinate>>() { new QuadKey("0302") };
            var buckets = subject.Search(quadkeyList);
            Assert.IsTrue(buckets.Any());
            foreach (var item in buckets)
            {
                System.Diagnostics.Trace.WriteLine(item);
            }            
        }

        [TestMethod]
        public void SimpleLocationAgregateTest()
        {
            var subject = LocationSubject();
            IEnumerable<Telekad.Types.IRangeExtended<Coordinate>> quadkeyList = new List<Telekad.Types.IRangeExtended<Coordinate>>() { new QuadKey("0302") };
            var buckets = subject.Search(quadkeyList);
            Assert.IsTrue(buckets.Any());
            foreach (var item in buckets)
            {
                System.Diagnostics.Trace.WriteLine(item);
                foreach (var did in item.DocumentIds(1))
                {
                    System.Diagnostics.Trace.WriteLine(string.Format("{0};",did));
                }
            }
            var averageCoordinate = subject.Aggregations(1, buckets.First()).First();
            Assert.AreEqual(averageCoordinate.Value.Latitude, 44.751, 0.0001);
            Assert.AreEqual(averageCoordinate.Value.Longitude, -73.5005, 0.0001);

        }
    }
}
