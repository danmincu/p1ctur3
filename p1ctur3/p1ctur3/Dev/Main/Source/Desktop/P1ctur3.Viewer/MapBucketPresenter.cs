using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telekad.Utils;

namespace P1ctur3.Viewer
{
    public class MapBucketPresenter : MapItemPresenterBase
    {
        public MapBucket MapBucket { private set; get; }

        public MapBucketPresenter() { }

        public MapBucketPresenter(MapBucket mb)
        {
            ArgumentValidation.CheckArgumentForNull(mb, "mb");

            if (mb.Count == 0)
                throw new ArgumentOutOfRangeException("mb", "empty bucket exception");
            this.Latitude = mb.AverageCoordinate.Latitude;
            this.Longitude = mb.AverageCoordinate.Longitude;
            this.MapBucket = mb;
        }

        public override Point Pinpoint
        {
            get
            {
                return new Point(this.AggregateSize.Width / 2.0, this.AggregateSize.Height / 2.0);
            }
        }



        public string ToolTip
        {
            get
            {
                long partialSelectionCount = 0;
                //if (this.SelectState == SelectionState.PartiallySelected && this.SelectionController != null)
                //{
                //    var selectableGroup = PresenterToSelectableResolver.New(this) as ISelectableGroup;
                //    if (selectableGroup != null)
                //    {
                //        partialSelectionCount = this.SelectionController.GetTotalSelectionGroupCount(selectableGroup);
                //    }
                //}

                var tooltip = partialSelectionCount == 0
                        ? string.Format(CultureInfo.InvariantCulture, "{0} {1}", this.MapBucket.Count, "")
                        : string.Format(CultureInfo.InvariantCulture, "{0}/{1} {2}", partialSelectionCount, this.MapBucket.Count, "");

                return tooltip;
            }
        }


        public Size AggregateSize
        {
            get
            {
                const int MinItemAggregateSize = 10;
                return new Size(16, 16);
                //var ln = Math.Log(this.MapBucket.Count);
                //return new Size(MinItemAggregateSize + 10 * ln, MinItemAggregateSize + 10 * ln);
            }
        }
    }

    
    ///// <summary>
    ///// A presenter for handling buckets of map items. Currently tracks count and the associated lat/longs.
    ///// </summary>
    //public class MapBucketPresenter : MapItemPresenterBase
    //{
    //    public MapBucket MapBucket { private set; get; }

    //    public MapBucketPresenter() { }

    //    public MapBucketPresenter(MapBucket mb)
    //    {
    //        Jsi.Utils.ArgumentValidation.CheckArgumentForNull(mb, "mb");

    //        if (mb.Count == 0)
    //            throw new ArgumentOutOfRangeException("mb", Res.EmptyMapBucketException);
    //        this.Latitude = mb.AverageCoordinate.Latitude;
    //        this.Longitude = mb.AverageCoordinate.Longitude;
    //        this.MapBucket = mb;
    //    }

    //    public override Point Pinpoint
    //    {
    //        get
    //        {
    //            return new Point(this.AggregateSize.Width / 2.0, this.AggregateSize.Height / 2.0);
    //        }
    //    }

    //    internal ISelectionController SelectionController { set; get; }

    //    public string ToolTip
    //    {
    //        get
    //        {
    //            long partialSelectionCount = 0;
    //            if (this.SelectState == SelectionState.PartiallySelected && this.SelectionController != null)
    //            {
    //                var selectableGroup = PresenterToSelectableResolver.New(this) as ISelectableGroup;
    //                if (selectableGroup != null)
    //                {
    //                    partialSelectionCount = this.SelectionController.GetTotalSelectionGroupCount(selectableGroup);
    //                }

    //            }

    //            var tooltip = partialSelectionCount == 0
    //                    ? string.Format(CultureInfo.InvariantCulture, "{0} {1}", this.MapBucket.Count, Res.MapBucketItemName)
    //                    : string.Format(CultureInfo.InvariantCulture, "{0}/{1} {2}", partialSelectionCount, this.MapBucket.Count, Res.MapBucketItemName);

    //            return tooltip;
    //        }
    //    }

    //    public Size AggregateSize
    //    {
    //        get
    //        {
    //            return new Size(16, 16);
    //            //var ln = Math.Log(aggregateCount);
    //            //return new Size(MinItemAggregateSize + 10 * ln, MinItemAggregateSize + 10 * ln);
    //        }
    //    }
    //}

}
