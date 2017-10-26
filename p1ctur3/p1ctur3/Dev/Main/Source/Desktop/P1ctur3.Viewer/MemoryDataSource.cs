using Evasearch.Structures;
using P1ctur3.Storage;
using P1ctur3.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using Telekad.Mapping;

namespace P1ctur3.Viewer
{
    //load the whole "docset" in memory and creates a Location search tree
    internal class MemoryDataSource : P1ctur3.Viewer.IMemoryDataSource
    {
        public MemoryDataSource()
        {
            var load = CloudData;
        }
        
        public static string StorageServiceUri = "http://i7/P1ctur3.Web/Services/P1ctur3DataService.svc/";

        ImageMetadata[] cloudCache;
        public ImageMetadata[] CloudData
        {
            get
            {
                if (cloudCache == null)
                {
                    var imsr = new P1ctur3.Storage.DataService.ImageMetadataDataServiceStorage(new Uri(StorageServiceUri), null);
                    cloudCache = imsr.Get(new IImageMetadataFilter() { Email = "danemincu@yahoo.com" }).Where(a => a.Coordinate != null).ToArray();
                }
                return cloudCache;
            }
        }

        private LocationStructure locationStructure;
        public LocationStructure LocationStructure
        {
            get
            {
                if (locationStructure == null)
                {
                    this.locationStructure = LocationTree(CloudData);
                }
                return locationStructure;
            }
        }
        public LocationStructure LocationTree(ImageMetadata[] data)
        {
            var result = new LocationStructure(18);

            var ticks = DateTime.Now.Ticks;
            System.Diagnostics.Trace.WriteLine("Tree creation start");

            for (long i = 0; i < data.Length; i++)
            {
                result.Add(1, data[i].Coordinate, i);
            }
            
            var afterTicks = DateTime.Now.Ticks;
            System.Diagnostics.Trace.WriteLine(string.Format("Durration to create the search tree {0} seconds", TimeSpan.FromTicks(afterTicks - ticks).TotalSeconds));

            return result;
        }


        public IEnumerable<MapBucket> Search(IEnumerable<QuadKey> quadkeyList)
        {            
            var searchBuckets = LocationStructure.Search(quadkeyList.Cast<Telekad.Types.IRangeExtended<Coordinate>>());
            foreach (var item in searchBuckets)
            {
                var mapBucket = new MapBucket((QuadKey)item.Value, locationStructure.Aggregations(1, item).Values.First(), item.Count(1));
                if (mapBucket.Count <= 50)
                {
                    foreach (var did in item.DocumentIds(1))
                    {
                        mapBucket.Metadatas.Add(this.CloudData[did]);
                    }
                }
                yield return mapBucket;
            }
        }

    }
}
