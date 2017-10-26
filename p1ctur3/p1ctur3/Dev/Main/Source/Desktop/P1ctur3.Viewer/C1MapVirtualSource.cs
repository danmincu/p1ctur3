using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows;
using System.Windows.Threading;
using C1.WPF.Maps;
using Telekad.Ux;
using Telekad.Mapping;


namespace P1ctur3.Viewer
{

    /// <summary>
    /// Base class for virtual sources for the ComponentOne map.  This class uses Reactive Framework to group requests and fire them to the data source as one request.
    /// </summary>
    public abstract class C1MapVirtualSource : NotifyObject, IMapVirtualSource
    {

        #region Fields

        /// <summary>
        /// Whether or not this source is currently enabled.  An enabled source will actively fetch data from the data source.
        /// </summary>
        private bool isEnabled = true;


        /// <summary>
        /// The comparer used for filtering out duplicate map requests.
        /// </summary>
        private MapRequestComparer requestComparer = new MapRequestComparer();

        /// <summary>
        /// This subject is used as the observable item for Rx extensions.
        /// </summary>
        private Subject<MapRequestEventArgs> observableSubject;

        private IMappingSettings settings;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="C1MapVirtualSource"/> class.
        /// </summary>
        protected C1MapVirtualSource(IMappingSettings settings, IScheduler observeOnScheduler, Dispatcher dispatcher)
        {
            this.observableSubject = new Subject<MapRequestEventArgs>();
            this.ObserveOnScheduler = observeOnScheduler;
            this.Dispatcher = dispatcher;
            this.settings = settings;
        }

        #endregion

        #region Properties

        protected IMappingSettings Settings
        {
            get
            {
                return this.settings;
            }
        }

        public IScheduler ObserveOnScheduler
        {
            get;
            protected set;
        }


        public Dispatcher Dispatcher
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the interval in ms of how long map requests for data are buffered before being processed.
        /// </summary>
        /// <value>The buffer interval in ms.</value>
        protected abstract double BufferThrottle
        {
            get;
        }

        /// <summary>
        /// Gets the max number of tiles.
        /// </summary>
        /// <value>The max number of tiles.</value>
        protected abstract int MaxNumberOfTiles
        {
            get;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled. 
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is enabled; otherwise, <c>false</c>.  The default value is <c>true</c>.
        /// </value>
        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                if (this.isEnabled == value)
                {
                    return;
                }

                this.isEnabled = value;
                RaisePropertyChanged(() => IsEnabled);
            }
        }



        /// <summary>
        /// Gets the observable subject.
        /// </summary>
        /// <value>The observable subject.</value>
        protected Subject<MapRequestEventArgs> ObservableSubject
        {
            get { return this.observableSubject; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes this instance.  It subscribes to the observable subject to initialize the message stream.
        /// </summary>
        protected void Initialize()
        {
            this.ObservableSubject
                .SubscribeOn(ObserveOnScheduler)
                .BufferWithThresholdTime(TimeSpan.FromMilliseconds(this.BufferThrottle), this.ObserveOnScheduler)
                .ObserveOn(this.ObserveOnScheduler)
                .Subscribe(new Action<IList<MapRequestEventArgs>>(this.ProcessRequests));
        }

        /// <summary>
        /// Processes the requests from the observable subject.
        /// </summary>
        /// <param name="requestList">The request list.</param>
        protected virtual void ProcessRequests(IList<MapRequestEventArgs> requestList)
        {
            var filteredList = FilterEvents(requestList);

            if (this.IsEnabled)
            {
                RequestData(filteredList);
            }
        }

        /// <summary>
        /// Filters the buffered list of events based on the maximum number of tiles visible.
        /// </summary>
        /// <param name="eventList">The event list.</param>
        /// <returns>A collection of <see cref="MapRequestEventArgs"/> where all zoom levels above the minimum and thrown out and duplicates are removed by quadkey.</returns>
        protected virtual IList<MapRequestEventArgs> FilterEvents(IEnumerable<MapRequestEventArgs> eventList)
        {
            var count = Math.Max(eventList.Count() - this.MaxNumberOfTiles, 0);
            var last = eventList.LastOrDefault();

            var requestList = eventList.Skip(count)
                                .Where(args => args.MinZoom == last.MinZoom)
                                .Distinct(this.requestComparer)
                                .Select(args => new MapRequestEventArgs((int)Math.Floor(args.MinZoom) + 1, 20, args.Quadrangle, args.Callback))
                                .ToList();

            return requestList;
        }

        /// <summary>
        /// Requests the data from the data source for the filtered list of event arguments.  This method is only called when the source is enabled.
        /// </summary>
        /// <param name="filteredList">The filtered list.</param>
        protected abstract void RequestData(IList<MapRequestEventArgs> filteredList);

        /// <summary>
        /// Requests the data items for the map splice specified.  This method adds a new item to the observable subject.
        /// </summary>
        /// <param name="minZoom">The min zoom level to display the items.</param>
        /// <param name="maxZoom">The max zoom level for displaying the items.</param>
        /// <param name="lowerLeft">The lower left point in geographical coordinates.</param>
        /// <param name="upperRight">The upper right point in geographical coordinates.</param>
        /// <param name="callback">The callback function that is to be called with the resulting list of items retrieved.</param>
        public virtual void Request(double minZoom, double maxZoom, Point lowerLeft, Point upperRight, Action<ICollection> callback)
        {
            this.observableSubject.OnNext(
                new MapRequestEventArgs(
                    minZoom,
                    maxZoom,
                    new Quadrangle(new Coordinate(lowerLeft.Y, lowerLeft.X), new Coordinate(upperRight.Y, upperRight.X)),
                    callback));
        }

        /// <summary>
        /// This is the list of the quadkeys of the map tiles at their root (most zoomed out).
        /// </summary>
        public static readonly IList<QuadKey> BasicTiles = new List<QuadKey>() { new QuadKey("0"), new QuadKey("1"), new QuadKey("2"), new QuadKey("3") };


        public static List<Tuple<MapRequestEventArgs, IEnumerable<QuadKey>>> ConvertRequestList(IEnumerable<MapRequestEventArgs> requestList, int maxCubeLevel)
        {
            // Parse the Tuple<MapRequest> and create a new type of request where each heat map is fed by the included and surrounding quad keys
            var transformedList = requestList
                                    .Where(tuple => tuple.MinZoom > 2)
                                    .Select(requestArgs =>
                                    {
                                        var minZoom = (int)requestArgs.MinZoom;
                                        if (minZoom > maxCubeLevel)
                                        {
                                            minZoom = maxCubeLevel;
                                        }

                                        return new Tuple<MapRequestEventArgs, IEnumerable<QuadKey>>(
                                                                                        requestArgs,
                                                                                         QuadKey.ConvertCoordinateToQuadKey(requestArgs.Quadrangle.Centre, minZoom - 2).SumOfAllNeighbours(2));

                                    })
                                    .ToList();


            //// Level 1 or 2 request
            if (!transformedList.Any() && requestList.Where(tuple => (int)tuple.MinZoom <= 2).Any())
            {
                var requestItem = requestList.FirstOrDefault();
                if (requestItem == null)
                {
                    return null;
                }

                var quadKeysReq = BasicTiles
                                    .SelectMany(key => key.SumOfAllNeighbours(2))
                                    .Distinct()
                                    .ToList();

                transformedList = new List<Tuple<MapRequestEventArgs, IEnumerable<QuadKey>>>
                                      {
                                          new Tuple<MapRequestEventArgs, IEnumerable<QuadKey>>(
                                              requestItem, quadKeysReq)
                                      };
            }
            return transformedList;
        }

        #endregion
    }

    public class MapRequestEventArgs : System.EventArgs
    {
        public double MinZoom { set; get; }
        public double MaxZoom { set; get; }
        public Quadrangle Quadrangle { set; get; }
        public Action<ICollection> Callback { set; get; }

        public MapRequestEventArgs(double minZoom, double maxZoom, Quadrangle quadrangle, Action<ICollection> callback)
        {
            this.MinZoom = minZoom;
            this.MaxZoom = maxZoom;
            this.Quadrangle = quadrangle;
            this.Callback = callback;
        }
    }

    public class MapRequestComparer : IEqualityComparer<MapRequestEventArgs>
    {
        public bool Equals(MapRequestEventArgs x, MapRequestEventArgs y)
        {
            return x.Quadrangle == y.Quadrangle;
        }

        public int GetHashCode(MapRequestEventArgs obj)
        {
            return obj.Quadrangle.GetHashCode();
        }
    }


}
