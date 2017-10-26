using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1ctur3.Viewer
{
    /// <summary>
    /// Used to define the fields a mapping resource will require to query a cube with mapping related information in it.
    /// </summary>
    public interface IMappingSettings
    {
        /// <summary>
        /// Maximum cube quad key hierarchy depth used when
        /// </summary>
        int MaxCubeQuadkeyHierarchyDepth { get; }

        /// <summary>
        /// Maximum level that the map can zoom
        /// </summary>
        int MaxZoomLevel { get; }

        /// <summary>
        /// The maximum number of buckets to return from the repository
        /// during the drilldown through the levels.
        /// </summary>
        int MaximumNumberOfBuckets { get; }

        /// <summary>
        /// The number of levels for the repository to drill down
        /// when looking for the required number of buckets.
        /// </summary>
        int BucketDrilldownDepth { get; }

        /// <summary>
        /// The default buffer interval between processing of the message queue in the map virtual source
        /// </summary>
        double VirtualSourceBufferThrottle { get; }

        /// <summary>
        /// The max number of tiles that will be used in a request list
        /// </summary>
        int VirtualSourceMaxNumberOfTiles { get; }

        /// <summary>
        /// The maximum number of buckets to return from the repository
        /// during the drilldown through the levels.
        /// </summary>
        int HeatMapMaximumNumberOfBuckets { get; }

        /// <summary>
        /// The number of levels for the repository to drill down
        /// when looking for the required number of buckets.
        /// </summary>
        int HeatMapBucketDrilldownDepth { get; }

        /// <summary>
        /// The default buffer interval  between processing of the message queue in the heat map virtual source
        /// </summary>
        double HeatMapVirtualSourceBufferThrottle { get; }

        /// <summary>
        /// Gets the max number of tiles.
        /// </summary>
        int HeatMapVirtualSourceMaxNumberOfTiles { get; }

        /// <summary>
        /// The minimum zoom level for heat maps
        /// </summary>
        int HeatMapMinimumZoomLevel { get; }

        /// <summary>
        /// The intensity given to maximum bucket in the viewport
        /// </summary>
        double HeatMapMaxBucketIntensity { get; }

        /// <summary>
        /// The throttling delay used when adjusting the map intensity
        /// </summary>
        double HeatMapIntensityThrottleDelay { get; }

        /// <summary>
        /// The maximum number of buckets to return from the repository
        /// during the drilldown through the levels.
        /// </summary>
        int BucketCountMaximumNumberOfBuckets { get; }

        /// <summary>
        /// The number of levels for the repository to drill down
        /// when looking for the required number of buckets.
        /// </summary>
        int BucketCountBucketDrilldownDepth { get; }

        /// <summary>
        /// The default buffer interval  between processing of the message queue in the heat map virtual source
        /// </summary>
        double BucketCountVirtualSourceBufferThrottle { get; }

        /// <summary>
        /// Gets the max number of tiles.
        /// </summary>
        int BucketCountVirtualSourceMaxNumberOfTiles { get; }


        /// <summary>
        /// Minimum number of items in each bucket
        /// </summary>
        int MinimumItemsPerBucket { get; }

        /// <summary>
        /// Maximum number of trackpoints that can be displayed on screen
        /// </summary>
        int MaxTrackpointScreenCount { get; }

        /// <summary>
        /// The min zoom level before we start showing track points
        /// </summary>
        int MinTrackpointZoomLevel { get; }

        /// <summary>
        /// The min zoom level before we start showing access points
        /// </summary>
        int MinAccessPointZoomLevel { get; }

        /// <summary>
        /// The number of items to try to retrieve per request by default.
        /// </summary>
        int ItemPageSize { get; }

        /// <summary>
        /// The maximum number of items that a bucket can contain when optimizing 
        /// the GetItemAsync method call.
        /// 
        /// Setting this property to a non-null value instructs the pre-caching
        /// algorithm to fetch items from buckets this size or smaller in addition
        /// to the current bucket.
        /// </summary>
        int ItemRetrievalThreshold { get; }

        string AttributeMemberTemplate { get; }

        /// <summary>
        /// The ID of the Attribute Definition for the mapping contextual filter
        /// </summary>
        string ContextualFilterAttributeDefinitionId { get; }

        /// <summary>
        /// The name of the Attribute Definition for the mapping contextual filter
        /// </summary>
        string ContextualFilterAttributeDefinitionName { get; }

        /// <summary>
        /// The attribute member key for the mapping contextual filter
        /// </summary>
        string ContextualFilterAttributeMemberKey { get; }

        /// <summary>
        /// The attribute member value for the mapping contextual filter
        /// </summary>
        string ContextualFilterAttributeMemberValue { get; }
    }

    public class MapSettings : IMappingSettings
    {
        public int MaxCubeQuadkeyHierarchyDepth
        {
            get { return 18; }
        }

        public int MaxZoomLevel
        {
            get { return 18; }
        }

        public int MaximumNumberOfBuckets
        {
            get { return 100; }
        }

        public int BucketDrilldownDepth
        {
            get { return 3; }
        }

        public double VirtualSourceBufferThrottle
        {
            get { return 1000; }
        }

        public int VirtualSourceMaxNumberOfTiles
        {
            get
            {

                return 100;
            }
        }

        public int HeatMapMaximumNumberOfBuckets
        {
            get
            {
                return 100;
            }
        }

        public int HeatMapBucketDrilldownDepth
        {
            get
            {
                return 3;
            }
        }

        public double HeatMapVirtualSourceBufferThrottle
        {
            get
            {
                return 1000;
            }
        }

        public int HeatMapVirtualSourceMaxNumberOfTiles
        {
            get
            {
                return 100;
            }
        }

        public int HeatMapMinimumZoomLevel
        {
            get
            {
                return 1;
            }
        }

        public double HeatMapMaxBucketIntensity
        {
            get
            {
                return 100;
            }
        }

        public double HeatMapIntensityThrottleDelay
        {
            get
            {
                return 1000;
            }
        }

        public int BucketCountMaximumNumberOfBuckets
        {
            get
            {
                return 100;
            }
        }

        public int BucketCountBucketDrilldownDepth
        {
            get {
                return 3;
            }
        }

        public double BucketCountVirtualSourceBufferThrottle
        {
            get {
                return 1000;
            }
        }

        public int BucketCountVirtualSourceMaxNumberOfTiles
        {
            get { return 100; }
        }

        public int MinimumItemsPerBucket
        {
            get { return 5; }
        }

        public int MaxTrackpointScreenCount
        {
            get { return 100; }
        }

        public int MinTrackpointZoomLevel
        {
            get { return 6; }
        }

        public int MinAccessPointZoomLevel
        {
            get { return 6; }
        }

        public int ItemPageSize
        {
            get { return 200; }
        }

        public int ItemRetrievalThreshold
        {
            get { return 10; }
        }

        public string AttributeMemberTemplate
        {
            get { return "unknown"; }
        }

        public string ContextualFilterAttributeDefinitionId
        {
            get { return "HasLocation=true"; }
        }

        public string ContextualFilterAttributeDefinitionName
        {
            get { return "HasLocation"; }
        }

        public string ContextualFilterAttributeMemberKey
        {
            get { return "location"; }
        }

        public string ContextualFilterAttributeMemberValue
        {
            get { return "true"; }
        }
    }
}
