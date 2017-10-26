using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using Telekad.Utils;
using Telekad.Ux;
using Telekad.Mapping;
using System.Windows;
using System.Windows.Media;

namespace P1ctur3.Viewer
{
    
    ///// <summary>
    ///// Base class for map item presenters.  Essentially, anything displayed on the map should derive from this class.
    ///// </summary>
    public abstract class MapItemPresenterBase : ViewModelBase
    {
        #region Fields

        public const int DefaultItemSize = 256;

        private readonly IList<CancellationTokenSource> cancellationTokenSourceList = new List<CancellationTokenSource>();

        #endregion

        #region Contructors

        protected MapItemPresenterBase()
        {
            // this is needed since derived MapItemPresenters don't actually have a map item...yup.
        }

        protected MapItemPresenterBase(IMapItem mapItem)
        {
            ArgumentValidation.CheckArgumentForNull(mapItem, "mapItem");

            // take coordinate from mapItem
            this.Latitude = mapItem.Coordinate.Latitude;
            this.Longitude = mapItem.Coordinate.Longitude;
            this.MapItem = mapItem;
        }

        protected MapItemPresenterBase(double? latitude, double? longitude, IMapItem mapItem)
        {
            ArgumentValidation.CheckArgumentForNull(mapItem, "mapItem");

            this.Latitude = latitude;
            this.Longitude = longitude;
            this.MapItem = mapItem;
        }

        #endregion

        #region Properties

        public IMapItem MapItem
        {
            get { return Get(() => MapItem); }
            set { Set(() => MapItem, value); }
        }

        public double? Latitude
        {
            get { return Get(() => Latitude); }
            set { Set(() => Latitude, value, OnLocationChanged); }
        }

        public double? Longitude
        {
            get { return Get(() => Longitude); }
            set { Set(() => Longitude, value, OnLocationChanged); }
        }

        /// <summary>
        /// This method is meant to be called when either the latitude on longitude changes.
        /// It raises the PropertyChanged event for the position and coordinate properties if
        /// they also change as a result of the operation.
        /// </summary>
        private void OnLocationChanged()
        {
            if (this.Latitude.HasValue && this.Longitude.HasValue)
            {
                RaisePropertyChanged(() => Position);
                RaisePropertyChanged(() => Coordinate);
            }
        }

        protected IList<CancellationTokenSource> CancellationTokenSourceList
        {
            get { return cancellationTokenSourceList; }
        }

        public CancellationToken CancellationToken
        {
            get
            {
                if (cancellationTokenSourceList.Count == 0)
                {
                    cancellationTokenSourceList.Add(new CancellationTokenSource());
                }
                return cancellationTokenSourceList[0].Token;
            }
        }

        public bool IsExplored
        {
            get { return Get(() => IsExplored, false); }
            set { Set(() => IsExplored, value); }
        }

        //public SelectionState SelectState
        //{
        //    get { return Get(() => SelectState, SelectionState.None); }
        //    set { Set(() => SelectState, value); }
        //}

        public bool IsSelectionPreview
        {
            get { return Get(() => IsSelectionPreview, false); }
            set { Set(() => IsSelectionPreview, value); }
        }

        public bool IsHighlighted
        {
            get { return Get(() => IsHighlighted, false); }
            set { Set(() => IsHighlighted, value); }
        }

        public bool IsGlowing
        {
            get { return Get(() => IsGlowing); }
            set { Set(() => IsGlowing, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is external card.
        /// External cards are created on a temporary basis to allow film strip items to be displayed in the map presenter
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is external card; otherwise, <c>false</c>.
        /// </value>
        public bool IsFilmstripSelection
        {
            get { return Get(() => IsFilmstripSelection); }
            set { Set(() => IsFilmstripSelection, value); }
        }

        public virtual Size ItemSize
        {
            get { return Get(() => ItemSize, new Size(DefaultItemSize, DefaultItemSize)); }
            set { Set(() => ItemSize, value); }
        }

        [DependsUpon("ItemSize")]
        public virtual Point Pinpoint
        {
            get { return new Point(ItemSize.Width / 2.0, ItemSize.Height / 2.0); }
        }

        public double MinZoom
        {
            get { return Get(() => MinZoom); }
            set { Set(() => MinZoom, value); }
        }

        public double MaxZoom
        {
            get { return Get(() => MaxZoom); }
            set { Set(() => MaxZoom, value); }
        }

        public QuadKey Quadkey
        {
            get { return Get(() => Quadkey); }
            set { Set(() => Quadkey, value); }
        }

        public string Name
        {
            get { return Get(() => Name); }
            set { Set(() => Name, value); }
        }

        public Point Position
        {
            get
            {
                if (!this.Latitude.HasValue || !this.Longitude.HasValue)
                    return new Point();
                else
                    return new Point(Longitude.Value, Latitude.Value);
            }
        }

        /// <summary>
        /// Returns the coordinate of the Map Item if both the latitude and longitude are defined.
        /// </summary>
        public Coordinate Coordinate
        {
            get
            {
                if (this.Latitude.HasValue && this.Longitude.HasValue)
                    return new Coordinate(this.Latitude.Value, this.Longitude.Value);
                else
                    return null;
            }
        }

        public virtual Color Color
        {
            get { return Get(() => Color, Colors.Black); }
            set { Set(() => Color, value); }
        }

        /// <summary>
        /// Gets the default brush. This value is set by changing the color property
        /// </summary>
        [DependsUpon("Color")]
        public Brush DefaultBrush
        {
            get
            {
                return new SolidColorBrush(this.Color);
            }
        }

        #endregion

        #region Methods

        public virtual string ToolTipInfo()
        {
            return "No tooltip provided";
        }

        public void CancelAll()
        {
            foreach (var cancellationToken in cancellationTokenSourceList)
            {
                cancellationToken.Cancel();
            }
        }

        protected void ClearCancelationTokens()
        {
            this.cancellationTokenSourceList.Clear();
        }

        public void UnionCancellationTokens(MapItemPresenterBase mapItemPresenterBase)
        {
            foreach (var cancellationTokenSource in mapItemPresenterBase.CancellationTokenSourceList)
            {
                if (!cancellationTokenSourceList.Contains(cancellationTokenSource))
                    cancellationTokenSourceList.Add(cancellationTokenSource);
            }
            mapItemPresenterBase.ClearCancelationTokens();
        }

        public void AddCancellationToken(CancellationTokenSource token)
        {
            if (!cancellationTokenSourceList.Contains(token))
                cancellationTokenSourceList.Add(token);
        }

        #endregion
    }
}
