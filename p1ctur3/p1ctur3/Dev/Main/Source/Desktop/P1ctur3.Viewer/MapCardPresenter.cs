using P1ctur3.Types;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telekad.Mapping;

namespace P1ctur3.Viewer
{
    /// <summary>
    /// A presenter for individual map items. 
    /// </summary>
    public class MapCardPresenter : MapItemPresenterBase
    {
        private const double DistanceTolerance = 0.01;//10 meters
        private readonly Point pinPoint;
        
        public MapCardPresenter(ImageMetadata  metadata)
           // : base(mapItem)
        {

           this.Latitude = metadata.Coordinate.Latitude;
           this.Longitude = metadata.Coordinate.Longitude;
           this.ImageUri = new Uri(metadata.StorageInfo.GetUrl("m"));
           this.MapItem = new MapItem() { Coordinate = metadata.Coordinate, Id = Guid.NewGuid() };
       
           //this.Name = string.Format(CultureInfo.InvariantCulture, "MapCardName {0} {1}", this.MapItem.Id, this.ImageUri);

            // TODO: Is this really necessary? Should the control offset for the pinpoints?
            this.pinPoint = new Point(5, 5);
        }

        #region Properties

        public Uri ImageUri { get; private set; }

        ///// <summary>
        ///// Gets a value indicating whether this instance is content authorized.
        ///// </summary>
        //public bool IsContentAuthorized { get; private set; }

        public override Point Pinpoint
        {
            get { return pinPoint; }
        }

        #endregion

        public static bool operator ==(MapCardPresenter p1, MapCardPresenter p2)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(p1, p2))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)p1 == null) || ((object)p2 == null))
            {
                return false;
            }

            return p1.Equals(p2);
        }

        public static bool operator !=(MapCardPresenter p1, MapCardPresenter p2)
        {
            return !(p1 == p2);
        }

        public override int GetHashCode()
        {
            if (this.MapItem != null)
                return this.MapItem.Id.GetHashCode() ^ this.ImageUri.GetHashCode();

            return this.ImageUri.GetHashCode();
        }

        public bool Equals(MapCardPresenter other)
        {
            if (other == null)
                return false;

            if (this.MapItem != null && other.MapItem != null)
                return other.MapItem.Id == this.MapItem.Id && other.ImageUri == this.ImageUri;

            return other.ImageUri == this.ImageUri;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is MapCardPresenter)) return false;
            return Equals((MapCardPresenter)obj);
        }

        /// <summary>
        /// Gets the tool tip info.
        /// </summary>
        
        public new string ToolTipInfo
        {
            get
            {
                
                string toolTip;

                // If both case and line names are null, don't bother listing them in the tool tip
                
                    toolTip = string.Format(CultureInfo.InvariantCulture,
                        "{0} {1}",
                        this.MapItem.Coordinate.Latitude,
                        this.MapItem.Coordinate.Longitude);
                return toolTip;
            }
        }

        public virtual bool OverlapsWith(IMapItem mapItem)
        {
            if (this.MapItem.Id == mapItem.Id)
                return false;
            if (!(this.Latitude.HasValue && this.Longitude.HasValue))
                return false;
            return (CoordinateTransformations.Distance((double)this.Latitude, (double)this.Longitude, mapItem.Coordinate.Latitude, mapItem.Coordinate.Longitude) <= DistanceTolerance);
        }

        public bool OverlapsWith(MapItemPresenterBase cardPresenterBase)
        {
            if (cardPresenterBase == null || cardPresenterBase.MapItem == null)
                return false;
            return this.OverlapsWith(cardPresenterBase.MapItem);
        }

        public virtual bool ContainsMapItem(IMapItem mapItem)
        {
            if (mapItem == null)
                return false;
            return this.MapItem == null ? false : this.MapItem.Id == mapItem.Id;
        }
    }
}
