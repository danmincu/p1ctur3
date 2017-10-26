using P1ctur3.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telekad.Mapping;
using Telekad.Types;
using Telekad.Utils;

namespace P1ctur3.Viewer
{
    /// <summary>
    /// A thin wrapper around an AttributeMember that allows us to
    /// quickly access information necessary for mapping.
    /// </summary>
    public class MapBucket : IRange
    {
        public List<ImageMetadata>  Metadatas{ get; private set; }

        public QuadKey QuadKey { get; private set; }
        public Coordinate AverageCoordinate { get; private set; }
        public string MapPointSource { get; internal set; }

        public string Id
        {
            get
            {
                return this.QuadKey.Key;
            }
        }

        public long Count { get; private set; }

        public long TrackpointCount { get; private set; }

        public MapBucket(QuadKey quadkey)
        {
            this.QuadKey = quadkey;
            this.Count = 0;
            this.AverageCoordinate = default(Coordinate);
            this.TrackpointCount = 0;
        }

        public MapBucket(QuadKey quadkey, Coordinate averageCoorinate, long count)
        {
            this.Metadatas = new List<ImageMetadata>();
            this.QuadKey = quadkey;
            this.AverageCoordinate = averageCoorinate;
            this.Count = count;
            //this.TrackpointCount = trackpointCount;
            //this.MapPointSource = mapPointSourceName;
        }

        private const string ToStringTemplate = "{0}, Count = {1}";
        public override string ToString()
        {

            return ToStringTemplate.FormatInvariantCulture(this.Id, this.Count);
        }

        public bool OverlapsWith(IRange range)
        {
            return this.QuadKey.OverlapsWith(range);
        }

        public bool Contains(IRange innerRange)
        {
            return this.QuadKey.Contains(innerRange);
        }
    }

}
