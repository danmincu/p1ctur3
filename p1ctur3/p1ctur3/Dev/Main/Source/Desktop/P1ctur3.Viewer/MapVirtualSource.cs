using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Telekad.Mapping;
using Telekad.Utils;

namespace P1ctur3.Viewer
{
    ///// </summary>
    public class MapVirtualSource : C1MapVirtualSource
    {
    //    #region Fields

        private readonly IMapPerspectiveViewModel presenter;
    //    private readonly IMapCollectionsSynchronizer mapCollectionsSynchronizer;
        private readonly IMemoryDataSource dataSource;
    //    private readonly ITrackpointLayerPresenter trackpointLayerPresenter;
    //    private readonly IAccessPointLayerPresenter accessPointLayerPresenter;

    //    #endregion

    //    #region Constructors

    //    [ExcludeFromCodeCoverage]
    //    public MapVirtualSource(IMapPerspectivePresenter presenter,
    //        IMappingDataSource dataSource,
    //        IMappingSettings settings,
    //        IMapCollectionsSynchronizer mapCollectionsSynchronizer,
    //        ITrackpointLayerPresenter trackpointLayerPresenter,
    //        IAccessPointLayerPresenter accessPointLayerPresenter,
    //        Dispatcher dispatcher) :
    //        this(presenter, dataSource, settings, mapCollectionsSynchronizer, trackpointLayerPresenter, accessPointLayerPresenter, DispatcherScheduler.Instance, dispatcher) { }

    //    [ExcludeFromCodeCoverage]
    //    public MapVirtualSource(IMapPerspectivePresenter presenter,
    //        IMappingDataSource dataSource,
    //        IMappingSettings settings,
    //        IMapCollectionsSynchronizer mapCollectionsSynchronizer,
    //        Dispatcher dispatcher) :
    //        this(presenter, dataSource, settings, mapCollectionsSynchronizer, null, null, DispatcherScheduler.Instance, dispatcher) { }

        public MapVirtualSource(
           IMapPerspectiveViewModel presenter,
           IMemoryDataSource dataSource,
           IMappingSettings settings,
          //IMapCollectionsSynchronizer mapCollectionsSynchronizer,
          // ITrackpointLayerPresenter trackpointLayerPresenter,
          // IAccessPointLayerPresenter accessPointLayerPresenter,
           Dispatcher dispatcher) :
            this(
            presenter, 
            dataSource, 
            settings, 
            //mapCollectionsSynchronizer, 
            //trackpointLayerPresenter, 
            //accessPointLayerPresenter, 
            DispatcherScheduler.Current, 
            dispatcher) { }

        internal MapVirtualSource(
            IMapPerspectiveViewModel presenter,
            IMemoryDataSource dataSource,
            IMappingSettings settings,
            //IMapCollectionsSynchronizer mapCollectionsSynchronizer,
            //ITrackpointLayerPresenter trackpointLayerPresenter,
            //IAccessPointLayerPresenter accessPointLayerPresenter,
            IScheduler observeOnScheduler,
            Dispatcher dispatcher)
            : base(settings, observeOnScheduler, dispatcher)
        {
            //ArgumentValidation.CheckArgumentForNull(presenter, "presenter");
            ArgumentValidation.CheckArgumentForNull(settings, "settings");
            ArgumentValidation.CheckArgumentForNull(dataSource, "dataSource");
            this.presenter = presenter;
            //this.mapCollectionsSynchronizer = mapCollectionsSynchronizer;
            this.dataSource = dataSource;
            //this.trackpointLayerPresenter = trackpointLayerPresenter;
            //this.accessPointLayerPresenter = accessPointLayerPresenter;

            this.Initialize();
        }

    //    #endregion

    //    #region Properties

    //    /// <summary>
    //    /// Gets the max number of tiles.
    //    /// </summary>
    //    /// <value>The max number of tiles.</value>
        protected override int MaxNumberOfTiles
        {
            get
            {
                return this.Settings.VirtualSourceMaxNumberOfTiles;
            }
        }

        protected override double BufferThrottle
        {
            get
            {
                return this.Settings.VirtualSourceBufferThrottle;
            }
        }

    //    #endregion

    //    #region Methods

        /// <summary>
        /// Requests the data from the data source for the filtered list of event arguments.  This method is only called when the source is enabled.
        /// </summary>
        /// <param name="filteredList">The filtered list.</param>
        protected override void RequestData(IList<MapRequestEventArgs> filteredList)
        {
            System.Diagnostics.Trace.WriteLine("------------------------------>>>>>>");
            var transformedList = filteredList.Select((requestItem) => QuadKey.ConvertCoordinateToQuadKey(requestItem.Quadrangle.Centre, (int)requestItem.MinZoom));
            foreach (var item in transformedList)
            {
                System.Diagnostics.Trace.Write(item + ";");
            }

            if (transformedList.Any())
            {
                GetBuckets(transformedList);
            }
            System.Diagnostics.Trace.WriteLine("<<<<<<------------------------------");
        }

        private void GetBuckets(IEnumerable<QuadKey> transformedList)
        {

            //use the this.settings.BucketDrilldownDepth to find drill deeper than the requested quadkey

            var childList = transformedList.SelectMany(q => q.Children(this.Settings.BucketCountBucketDrilldownDepth));

            var mapbucketList = this.dataSource.Search(childList).ToList();
            this.presenter.Clear();
            foreach (var item in mapbucketList)
            {
                System.Diagnostics.Trace.WriteLine(item.QuadKey + " " + item.Count);
                
                if (item.Count <= 50)
                {
                    foreach (var meta in item.Metadatas)
                    {
                        this.presenter.Add(new MapCardPresenter(meta));    
                    }                  
                
                }
                else
                    this.presenter.Add(new MapBucketPresenter( new MapBucket(item.QuadKey, item.AverageCoordinate, item.Count)));

            }                
        }


    //    private async void GetBuckets(IEnumerable<QuadKey> transformedList)
    //    {
    //        var task = dataSource.GetBucketsAsync(transformedList.ToList(),
    //                                   new CancellationToken(),
    //                                   new Tuple<Dispatcher, Quadrangle>(this.Dispatcher, this.presenter.GeographicalViewport),
    //                                   this.Settings.MaximumNumberOfBuckets,
    //                                   this.Settings.BucketDrilldownDepth);
    //        await task;
    //        GetBucketsAsyncCallback(task.Result);
    //    }


    //    /// <summary>
    //    /// Called when buckets are returned from the data source
    //    /// </summary>
    //    /// <param name="args">The <see cref="Jsi.UX.DataAccess.Repository.Mapping.GetMapBucketsCompletedEventArgs"/> instance containing the event data.</param>
    //    private void GetBucketsAsyncCallback(GetMapBucketsCompletedEventArgs args)
    //    {
    //        if (args.Error != null)
    //        {
    //            this.presenter.SetError(new ApplicationException(Res.ErrorGettingMapBuckets, args.Error));
    //            return;
    //        }
    //        if (args.Cancelled)
    //            return;

    //        var userStateTuple = args.UserState as Tuple<Dispatcher, Quadrangle>;
    //        if (userStateTuple == null)
    //            throw new ApplicationException(Res.ErrorBadState);

    //        //clean the items outside of the presenter
    //        if (this.presenter != null)
    //        {
    //            this.mapCollectionsSynchronizer.RemoveOutsideOfMapItems(this.presenter.GeographicalViewport, userStateTuple.Item1);
    //        }

    //        if (!args.Buckets.Any())
    //        {
    //            this.mapCollectionsSynchronizer.RemoveParentBuckets(args.QuadKeys, userStateTuple.Item1);

    //            var quadrangle = userStateTuple.Item2;
    //            if (this.trackpointLayerPresenter != null)
    //                this.trackpointLayerPresenter.RequestTrackPoints(quadrangle, null, new CancellationToken());
    //            if (this.accessPointLayerPresenter != null)
    //                this.accessPointLayerPresenter.RequestAccessPoints(quadrangle, null, new CancellationToken());

    //            return;
    //        }

    //        var distinctBuckets = args.Buckets.Distinct(new MapBucketComparer());

    //        var addBuckets = distinctBuckets.Where(mapBucket => (mapBucket.Count >= this.Settings.MinimumItemsPerBucket)).ToList();
    //        var itemsBuckets = distinctBuckets.Where(mapBucket => (mapBucket.Count < this.Settings.MinimumItemsPerBucket)).ToList();

    //        if (addBuckets.Any())
    //        {
    //            this.mapCollectionsSynchronizer.Add(addBuckets, userStateTuple.Item1);
    //        }

    //        var filteredBuckets = this.mapCollectionsSynchronizer.FilterManagedCardBuckets(itemsBuckets);

    //        if (filteredBuckets.Any())
    //        {
    //            GetItems(args, filteredBuckets);
    //        }
    //        else
    //        {
    //            var quadrangle = userStateTuple.Item2;

    //            if (this.trackpointLayerPresenter != null)
    //                this.trackpointLayerPresenter.RequestTrackPoints(quadrangle, null, new CancellationToken());
    //            if (this.accessPointLayerPresenter != null)
    //                this.accessPointLayerPresenter.RequestAccessPoints(quadrangle, null, new CancellationToken());
    //        }
    //    }

    //    private async void GetItems(GetMapBucketsCompletedEventArgs args, IEnumerable<MapBucket> filteredBuckets)
    //    {
    //        var task = dataSource.GetItemsAsync(filteredBuckets.ToList(), new CancellationToken(), args.UserState);
    //        await task;
    //        GetItemsAsyncCallback(task.Result);
    //    }

    //    private void GetItemsAsyncCallback(GetMapBucketItemsCompletedEventArgs arg)
    //    {
    //        if (arg.Error != null)
    //        {
    //            this.presenter.SetError(new ApplicationException(Res.ErrorGettingItemCount, arg.Error));
    //            return;
    //        }
    //        if (arg.Cancelled)
    //            return;
    //        if (!arg.BucketItems.Any())
    //            return;

    //        var userState = arg.UserState as Tuple<Dispatcher, Quadrangle>;
    //        if (userState == null)
    //            throw new ApplicationException(Res.ErrorBadState);

    //        var cancellationTokenSource = new CancellationTokenSource();

    //        // Process trackpoints before adding new items to the map.  This is necessary since it considers the items currently on the map
    //        // for fetching etc...  mapCollectionsSynchronizer.Add is async since it does a Dispatcher.BeginInvoke
    //        if (this.trackpointLayerPresenter != null)
    //            this.trackpointLayerPresenter.RequestTrackPoints(userState.Item2, arg.BucketItems.SelectMany(bucket => bucket.Value), cancellationTokenSource.Token);

    //        // Process accessPoints before adding new items to the map.  This is necessary since it considers the items currently on the map
    //        // for fetching etc...  mapCollectionsSynchronizer.Add is async since it does a Dispatcher.BeginInvoke
    //        if (this.accessPointLayerPresenter != null)
    //            this.accessPointLayerPresenter.RequestAccessPoints(userState.Item2, arg.BucketItems.SelectMany(bucket => bucket.Value), cancellationTokenSource.Token);

    //        foreach (var bucket in arg.BucketItems)
    //        {
    //            this.mapCollectionsSynchronizer.Add(bucket.Value, bucket.Key, userState.Item1, cancellationTokenSource);
    //        }
    //    }

    //    #endregion

    }
}
