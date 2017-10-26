using C1.WPF.Maps;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Telekad.Mapping;

namespace P1ctur3.Viewer
{


    public class MapChangedEventArgs : EventArgs
    {
        public double ZoomLevel { set; get; }
        public Point Center { set; get; }
    }

    public class MapPerspectiveViewModel : IMapPerspectiveViewModel
    {
        IMemoryDataSource dataSource;

        public MapPerspectiveViewModel(IMemoryDataSource dataSource)
        {
            this.Settings = new MapSettings();
            this.dataSource = dataSource;
            this.MapSource = this.GetMapVirtualSource(dataSource);

            //test
            //var quadkey = new Telekad.Mapping.QuadKey("020323");
            //var coordinate = Telekad.Mapping.TileSystem.QuadKeyToQuadrangle(quadkey).Centre;
            //this.Add(new MapBucket(quadkey, coordinate, 100));
        }

        /// <summary>
        /// Updates the map coordinates (animate).  Used when the data source changes.
        /// </summary>
        protected void UpdateMapViewport()
        {
            // Clean up items before loading new items.  This handles a null filter(no sessions)!
            //if (this.mapCollectionsSynchronizer != null)
            //{
            //    this.mapCollectionsSynchronizer.Clear(presenterDispatcher);
            //}

            double newZoomLevel = 0.0;
            var newCenter = new Point(0, 0);

            //try
            //{
            //    if (dataSource.SouthwestCoordinate.HasValue && dataSource.NortheastCoordinate.HasValue)
            //    {
            //        var sw = dataSource.SouthwestCoordinate.Value;
            //        var ne = dataSource.NortheastCoordinate.Value;

            //        // Calculate the maximum zoom level that encompasses the entire quadrangle
            //        if (sw.Equals(new Coordinate(0, 0)) && sw.Equals(ne))
            //        {
            //            newZoomLevel = 0;
            //        }
            //        else
            //        {
            //            newZoomLevel = new Quadrangle(sw, ne).GetViewportZoomLevel();
            //        }

            //        newCenter = new Point((sw.Longitude + ne.Longitude) / 2.0, (sw.Latitude + ne.Latitude) / 2.0);
            //    }
            //}
            //catch (Exception e)
            //{
            //    var logException = new ApplicationException("Exception when Updating MapCoordinates", e);
            //    ExceptionPolicy.HandleException(logException, "Default Policy");
            //}

            this.ZoomLevel = (int)newZoomLevel;
            this.MapCenter = newCenter;

            OnMapChanged(new MapChangedEventArgs { ZoomLevel = newZoomLevel, Center = newCenter });
        }

        public event EventHandler<MapChangedEventArgs> MapChanged;

        protected void OnMapChanged(MapChangedEventArgs eventArgs)
        {
            if (MapChanged != null)
                MapChanged(this, eventArgs);
        }

        public bool ZoomToResult { set; get; }
        public Point MapCenter { set; get; }
        public double ZoomLevel { set; get; }

        public IMappingSettings Settings { set; get; }
        public IMapVirtualSource MapSource { set; get; }

        public Quadrangle GeographicalViewport { get; set; }

        ObservableCollection<MapItemPresenterBase> mapItemPresenters = new ObservableCollection<MapItemPresenterBase>();
        public ObservableCollection<MapItemPresenterBase> MapItemPresenters
        {
            get { return this.mapItemPresenters; }
        }

        public void Clear()
        {
            this.MapItemPresenters.Clear();
        }

        public void Add(MapItemPresenterBase bucket)
        {
            this.MapItemPresenters.Add(bucket);
        }

        protected MapVirtualSource GetMapVirtualSource(IMemoryDataSource dataSource)
        {
            return new MapVirtualSource(
                this,
                dataSource,
                this.Settings,
                //this.CollectionSynchronizer, 
                //this.trackpoints, 
                //this.accessPoints, 
                //this.PresenterDispatcher
                Dispatcher.CurrentDispatcher);
        }

        public double CurrentZoom { get; set; }

        public double TargetZoom { get; set; }
    }


    //public enum MapSelectionMode
    //{
    //    None,
    //    Inclusive,
    //    Exclusive
    //}

    //public abstract class MapPerspectiveViewModelBase : ViewModelBase, IPerspective, IFilmstripRequiredPerspective, IMapViewportNotify, IMapPerspectivePresenter, IActiveAware
    //{
    //    const double DefaultHeatMapIntensity = 0.198;

    //    #region Events

    //    public event EventHandler<MapChangedEventArgs> MapChanged;
    //    public event EventHandler<EventArgs> HeatMapChanged;
    //    public event EventHandler<MapViewportEventArgs> OnViewportChanged;

    //    #endregion

    //    #region Fields

    //    private bool showLocations = true;
    //    private int zoomLevel;
    //    private double heatMapIntesity = DefaultHeatMapIntensity;
    //    private Point mapCenter;
    //    private readonly IMappingDataSource dataSource;
    //    private readonly IFilterBuilder filterBuilder;
    //    private bool showCards;
    //    private readonly IMapTileSourceProvider tileSourceProvider;
    //    private MapTileSourceInfo selectedTileSource;
    //    private readonly IMapCollectionsSynchronizer mapCollectionsSynchronizer;
    //    private readonly IMappingSettings mappingSettings;
    //    private readonly IMapPerspectiveSettings clientSettings;
    //    private readonly IList<IMapPointSettings> mapPointSettings;
    //    private readonly Dictionary<string, AttributeDefinition> mapBucketAttributeDefinitions;
    //    private readonly Dispatcher presenterDispatcher;
    //    private MapVirtualSource mapVirtualSource;
    //    private int drilldownDepth;
    //    private int maxBucketSize;
    //    private MapBucketPresenter exploredBucket;
    //    private CancellationTokenSource itemCountTokenSource;
    //    private readonly AsyncValue<long> itemCount;
    //    private bool isDirty;
    //    private readonly IWorkspace workspace;
    //    private readonly IEventAggregator eventAggregator;

    //    #endregion

    //    #region Properties

    //    [ExcludeFromCodeCoverage]
    //    public abstract object View
    //    //TODO this property was created temporarily to allow smooth removal of PresenterBase 
    //    { get; }

    //    public IEnumerable<SortOrderCriteria> ItemSortOrderCriteria { get; private set; }

    //    public bool ShowLocations
    //    {
    //        get { return showLocations; }
    //        set
    //        {
    //            showLocations = value;
    //            RaisePropertyChanged(() => ShowLocations);
    //        }
    //    }

    //    protected IMapPerspectiveSettings ClientSettings
    //    {
    //        get { return this.clientSettings; }
    //    }

    //    protected IMappingDataSource DataSource
    //    {
    //        get
    //        {
    //            return dataSource;
    //        }
    //    }

    //    protected Dispatcher PresenterDispatcher
    //    {
    //        get
    //        {
    //            return presenterDispatcher;
    //        }
    //    }

    //    public IMappingSettings Settings
    //    {
    //        get
    //        {
    //            return mappingSettings;
    //        }
    //    }


    //    public IList<IMapPointSettings> MapPointSettings
    //    {
    //        get
    //        {
    //            return this.mapPointSettings;
    //        }
    //    }

    //    public IMapPointSettings DefaultMapPointSettings
    //    {
    //        get
    //        {
    //            return this.mapPointSettings[0];
    //        }
    //    }

    //    public MapItemPresenterBase ExploredMapItemPresenter
    //    {
    //        get
    //        {
    //            return MapItemPresenters == null ? null :
    //                MapItemPresenters.FirstOrDefault((x) => x.IsExplored);
    //        }
    //    }

    //    public MapItemPresenterBase HighlightedMapItemPresenter
    //    {
    //        get
    //        {
    //            return MapItemPresenters == null ? null
    //                : MapItemPresenters.FirstOrDefault(x => x.IsHighlighted);
    //        }
    //    }

    //    public Quadrangle GeographicalViewport { get; set; }

    //    public ObservableCollection<MapItemPresenterBase> MapItemPresenters
    //    {
    //        get { return this.mapCollectionsSynchronizer.MapItemPresenters; }
    //    }


    //    public double HeatMapIntensity
    //    {
    //        get
    //        {
    //            return this.heatMapIntesity;
    //        }
    //        set
    //        {
    //            if (value == this.heatMapIntesity)
    //                return;
    //            this.heatMapIntesity = value;
    //            RaisePropertyChanged(() => HeatMapIntensity);
    //        }
    //    }

    //    public int ZoomLevel
    //    {
    //        get
    //        {
    //            return this.zoomLevel;
    //        }
    //        set
    //        {
    //            if (value == this.zoomLevel)
    //                return;
    //            this.zoomLevel = value;
    //            RaisePropertyChanged(() => ZoomLevel);
    //        }
    //    }

    //    public int DrilldownDepth
    //    {
    //        get
    //        {
    //            return this.drilldownDepth;
    //        }
    //        set
    //        {
    //            if (value == this.drilldownDepth)
    //                return;
    //            this.drilldownDepth = value;
    //            RaisePropertyChanged(() => DrilldownDepth);
    //        }
    //    }

    //    public int MaxBucketSize
    //    {
    //        get
    //        {
    //            return this.maxBucketSize;
    //        }
    //        set
    //        {
    //            if (value == this.maxBucketSize)
    //                return;
    //            this.maxBucketSize = value;
    //            RaisePropertyChanged(() => MaxBucketSize);
    //        }
    //    }

    //    public IItemStateController ItemStateController
    //    {
    //        get;
    //        private set;
    //    }


    //    public Point MapCenter
    //    {
    //        get
    //        {
    //            return this.mapCenter;
    //        }
    //        set
    //        {
    //            if (value == this.mapCenter)
    //                return;
    //            this.mapCenter = value;
    //            RaisePropertyChanged(() => MapCenter);
    //        }
    //    }

    //    public bool ShowCards
    //    {
    //        get { return this.showCards; }
    //        set
    //        {
    //            if (value == this.showCards)
    //                return;
    //            this.showCards = value;
    //            RaisePropertyChanged(() => ShowCards);
    //        }
    //    }

    //    public MapVirtualSource MapSource
    //    {
    //        get
    //        {
    //            if (this.mapVirtualSource == null)
    //            {
    //                this.mapVirtualSource = this.GetMapVirtualSource();
    //            }
    //            return this.mapVirtualSource;
    //        }
    //    }

    //    protected IMapCollectionsSynchronizer CollectionSynchronizer
    //    {
    //        get { return this.mapCollectionsSynchronizer; }
    //    }

    //    public bool IsFilmstripVisible
    //    {
    //        get { return true; }
    //    }

    //    #region Tile Sources

    //    /// <summary>
    //    /// Gets the list of tile sources that can be selected for the base tiled map layer.
    //    /// </summary>
    //    public IEnumerable<MapTileSourceInfo> TileSources
    //    {
    //        get
    //        {
    //            return this.tileSourceProvider.TileSources;
    //        }
    //    }

    //    /// <summary>
    //    /// Gets or sets the currently selected tile source to view on the map.
    //    /// </summary>
    //    /// <value>The selected tile source.</value>
    //    public MapTileSourceInfo SelectedTileSource
    //    {
    //        get { return this.selectedTileSource; }
    //        set
    //        {
    //            if (value == this.selectedTileSource)
    //                return;

    //            this.selectedTileSource = value;
    //            RaisePropertyChanged(() => SelectedTileSource);
    //        }
    //    }

    //    /// <summary>
    //    /// Gets or sets the heat map presenter.  This property is set in the object constructor and is not changed.
    //    /// </summary>
    //    /// <value>The heat map.</value>
    //    public HeatMapPresenter HeatMap
    //    {
    //        get;
    //        private set;
    //    }

    //    public BucketCountVirtualSource CountVirtualSource
    //    {
    //        get;
    //        private set;
    //    }

    //    #endregion

    //    private IEnumerable<MapViewType> mapViewTypeOptions;
    //    public IEnumerable<MapViewType> MapViewTypeOptions
    //    {
    //        get
    //        {
    //            if (mapViewTypeOptions == null)
    //            {
    //                mapViewTypeOptions = new List<MapViewType>()
    //                                       {
    //                                           new MapViewType(MapViewTypes.Locations, UX.Mapping.Res.MapDetailsScreenTipTitle, true, new Uri("pack://application:,,,/Jsi.UX.Mapping;component/images/Points.png")),
    //                                           new MapViewType(MapViewTypes.HeatMap, UX.Mapping.Res.HeatMap, false, new Uri("pack://application:,,,/Jsi.UX.Mapping;component/images/HeatMap.png")),
    //                                       };
    //            }
    //            return mapViewTypeOptions;
    //        }

    //    }

    //    private MapViewType selectedMapViewTypeOptions;
    //    public MapViewType SelectedMapViewTypeOptions
    //    {

    //        get
    //        {
    //            return selectedMapViewTypeOptions;
    //        }

    //        set
    //        {
    //            if (selectedMapViewTypeOptions != value)
    //            {
    //                selectedMapViewTypeOptions = value;
    //                RaisePropertyChanged(() => SelectedMapViewTypeOptions);

    //                SelectedViewOptionsChanged(selectedMapViewTypeOptions);
    //            }
    //        }
    //    }

    //    protected abstract void SelectedViewOptionsChanged(MapViewType selectedMapViewTypeOptions);

    //    /// <summary>
    //    /// The unique name used to define the Map Perspective
    //    /// </summary>
    //    /// <value>The name.</value>
    //    public string Name
    //    {
    //        get { return "Map"; }
    //    }


    //    public IAsyncValue<long> ItemCount
    //    {
    //        get { return this.itemCount; }
    //    }

    //    public DateTime ItemCountTimestamp { get; private set; }

    //    #endregion

    //    #region Constructors

    //    internal MapPerspectiveViewModelBase(IWorkspace workspace,
    //                                   IEventAggregator eventAggregator,
    //                                   IMapTileSourceProvider tileSourceProvider,
    //                                   IMappingDataSource dataSource,
    //                                   IFilterBuilder filterBuilder,
    //                                   IMappingSettings mappingSettings,
    //                                   IMapPerspectiveSettings clientSettings,
    //                                   IList<IMapPointSettings> mapPointSettings,
    //                                   IMapCollectionsSynchronizer collectionSynchronizer,
    //                                   IItemStateController itemStateController,
    //                                   Dispatcher presenterDispatcher,
    //                                   IScheduler observeOnScheduler)
    //    {
    //        this.eventAggregator = eventAggregator;
    //        this.workspace = workspace;
    //        this.ItemStateController = itemStateController;
    //        this.ItemStateController.ExploreChanged += this.SelectorSelectionChangedHandler;
    //        this.ItemStateController.PreviewExploreChanged += this.SelectorPreviewSelectionChangedHandler;
    //        this.ItemStateController.HighlightChanged += this.SelectorHighlightChangedHandler;
    //        this.ItemStateController.PreviewHighlightChanged += this.SelectorPreviewHighlightChangedHandler;

    //        this.mappingSettings = mappingSettings;
    //        this.clientSettings = clientSettings;
    //        this.mapPointSettings = mapPointSettings;
    //        this.presenterDispatcher = presenterDispatcher;
    //        this.mapCollectionsSynchronizer = collectionSynchronizer;

    //        this.drilldownDepth = this.Settings.BucketDrilldownDepth;
    //        this.maxBucketSize = this.Settings.MaximumNumberOfBuckets;

    //        this.filterBuilder = filterBuilder;
    //        this.filterBuilder.FilterChanged += this.FilterBuilderChangedHandler;

    //        this.itemCount = new AsyncValue<long>((sender, e) => this.RaisePropertyChanged(() => this.ItemCount));

    //        this.dataSource = dataSource;
    //        this.dataSource.Changed += this.DataSourceChangedHandler;

    //        if (!string.IsNullOrEmpty(this.DefaultMapPointSettings.QuadTreeAttributeDefinitionId))
    //        {
    //            this.mapBucketAttributeDefinitions = new Dictionary<string, AttributeDefinition>();
    //            foreach (var aMapPointSetting in mapPointSettings)
    //            {
    //                this.mapBucketAttributeDefinitions.Add(aMapPointSetting.Name, new AttributeDefinition(aMapPointSetting.QuadTreeAttributeDefinitionId, string.Empty, AttributeType.Regular));
    //            }
    //        }

    //        // Initialize the source for fetching the map tiles to display.
    //        this.tileSourceProvider = tileSourceProvider;
    //        this.selectedTileSource = tileSourceProvider.DefaultTileSource;
    //        this.CollectionSynchronizer.SelectedItemRemoved += delegate
    //        { this.ItemStateController.SetExploredItem(new AsyncValue<Item> { IsPending = false, Value = null }, this); };

    //        // Initialize the heat map.
    //        var enabledClientPoints = new List<string> { this.DefaultMapPointSettings.Name };
    //        this.HeatMap = new HeatMapPresenter(this.dataSource, this, this.Settings, enabledClientPoints, this);
    //        this.CountVirtualSource = new BucketCountVirtualSource(this.dataSource, this, this.Settings, enabledClientPoints, DispatcherScheduler.Instance, this, presenterDispatcher);

    //        // Notify workspace when viewport changes
    //        this.GetPropertyChangedObserver("Viewport")
    //            .ObserveOn(observeOnScheduler)
    //            .Subscribe(x =>
    //            {
    //                // When we zoom/pan, ensure that the explored bucket is closed since it may not end up 
    //                // in view (pan), or even existing (zoom)
    //                this.exploredBucket = null;
    //                PublishContextualRangeFilter();
    //                ViewportUpdated(this.GeographicalViewport);
    //            });

    //        //register for Close command binding to allow performing clearing of the selection when Esc key is pressed
    //        //the command HAS to be ItemCardNavigationCommands.CloseCommand otherwise that original behaviour of the command is suppressed
    //        workspace.RegisterCommandBindings(new[] { new CommandBinding(ItemCardNavigationCommands.CloseCommand,
    //                                   (sender, e) => this.ClearMostSpecificExploredObject(),
    //                                   (sender, e) => e.CanExecute = this.ExploredMapItemPresenter != null) });
    //    }

    //    /// <summary>
    //    /// Perform initialization work that must be done during the construction of subclasses.
    //    /// See bug 11798 for an example of when this matters.
    //    /// </summary>
    //    protected void InitializeData()
    //    {
    //        this.dataSource.Filter = this.filterBuilder.CurrentFilter;
    //        this.UpdateCount();
    //    }

    //    protected abstract void ViewportUpdated(Quadrangle viewport);
    //    protected abstract MapVirtualSource GetMapVirtualSource();

    //    #endregion

    //    #region Selection

    //    private void SelectorPreviewSelectionChangedHandler(object sender, PreviewExploreChangedEventArgs e)
    //    {
    //        if (e.Source == this || !this.IsActive)
    //            return;

    //        this.ClearHighlightedItem();
    //    }

    //    private void SelectorSelectionChangedHandler(object sender, ExploreChangedEventArgs e)
    //    {
    //        // Do not handle events generated from this presenter.
    //        if (e.Source == this || !this.IsActive)
    //            return;

    //        if (e.ExploredItem == null || e.ExploredItem.Value == null)
    //        {
    //            this.ClearExploredItem(false);
    //        }
    //        else
    //        {
    //            // Explored item will supersede a highlighted item
    //            this.ClearHighlightedItem();

    //            // Clear the internal selection without publishing a new blank item.
    //            this.ClearExploredItem(false);

    //            // Update any card presenters (there might not be any in the case of a filmstrip click) that match the explored item.
    //            var exploredItem = e.ExploredItem.Value;
    //            this.MapItemPresenters
    //                .OfType<MapCardPresenter>()
    //                .Where(x => x.MapItem.Id == exploredItem.Id)
    //                .ForEach(x => x.IsExplored = true);
    //        }
    //    }

    //    /// <summary>
    //    /// Clears the currently explored item on the map.  If no item is explored, then this method will not do
    //    /// anything.
    //    /// </summary>
    //    /// <param name="publish">
    //    /// If set to <c>true</c>, then a new blank item is published to the rest of the application.
    //    /// If <c>false</c>, then no publish is performed and only the internal state of this perspective is affected.
    //    /// </param>
    //    private void ClearExploredItem(bool publish)
    //    {
    //        if (this.ItemStateController.ExploredItem == null) return;

    //        //get everything that is explored that is not a bucket
    //        var exploredPresenters = this.MapItemPresenters.Where(x => (x != null && x.IsExplored && !(x is MapBucketPresenter)));

    //        exploredPresenters.ForEach(x => x.IsExplored = false);

    //        // If an item is pending for selection, or the explored item exists, then clear it.
    //        if (publish && (this.ItemStateController.ExploredItem.IsPending || this.ItemStateController.ExploredItem.Value != null))
    //        {
    //            this.ItemStateController.SetExploredItem(new AsyncValue<Item>(false, null), this);
    //        }
    //    }

    //    /// <summary>
    //    /// Clears the currently explored bucket for the map.
    //    /// </summary>
    //    protected void ClearExploredBucket()
    //    {
    //        // Unselect all explored buckets in the synchronized collection.
    //        if (this.exploredBucket == null) return;

    //        this.exploredBucket.IsExplored = false;
    //        this.exploredBucket = null;
    //        RaisePropertyChanged("ExploredBucket");
    //        PublishContextualRangeFilter();
    //    }

    //    /// <summary>
    //    /// Clears the IsExplored property for all items in MapItemPresenters and update the view.
    //    /// If a card and a bucket are explored then only the card is closed.
    //    /// </summary>
    //    /// <returns>If selection is found set return <c>true</c></returns>
    //    public void ClearMostSpecificExploredObject()
    //    {
    //        var itemWasExplored = this.ItemStateController.ExploredItem.Value != null;
    //        ClearExploredItem(true);

    //        // If a card was closed, then don't clear the bucket explored state.
    //        if (!itemWasExplored)
    //            ClearExploredBucket();
    //    }

    //    /// <summary>
    //    /// Sets the explored card/Bucket.  Any current selections are cleared and replaced by the new selection.
    //    /// </summary>
    //    /// <param name="presenter">The map item/bucket that is to be explored.</param>
    //    internal virtual void SetExploredMapItem(MapItemPresenterBase presenter)
    //    {
    //        if (MapItemPresenters == null || presenter == null || presenter.IsExplored)
    //            return;

    //        // Remove any existing selections.
    //        ClearExploredItem(false);
    //        ClearExploredBucket();

    //        presenter.IsExplored = true;

    //        if (presenter is MapBucketPresenter)
    //        {
    //            this.exploredBucket = (MapBucketPresenter)presenter;
    //            RaisePropertyChanged("ExploredBucket");
    //            PublishContextualRangeFilter();
    //        }
    //        else if (presenter is MapCardPresenter)
    //        {
    //            var mapItem = (MapItem)presenter.MapItem.AsItem();
    //            this.ClearHighlightedItem();
    //            this.ItemStateController.SetExploredItem(new AsyncValue<Item> { IsPending = false, Value = mapItem.AsItem() }, this);
    //        }
    //    }

    //    #endregion

    //    #region Highlighting

    //    private void SelectorPreviewHighlightChangedHandler(object sender, PreviewHighlightChangedEventArgs e)
    //    {
    //        if (e.Source == this || !this.IsActive)
    //            return;

    //        this.ClearHighlightedItem();
    //    }

    //    private void SelectorHighlightChangedHandler(object sender, HighlightChangedEventArgs e)
    //    {
    //        if (e.Source == this || !this.IsActive)
    //            return;

    //        SetHighlightedItem(e.HighlightedItem);
    //    }

    //    /// <summary>
    //    /// Clears the highlighted item.
    //    /// </summary>
    //    protected virtual void ClearHighlightedItem()
    //    {
    //        // Expect only one or none!
    //        foreach (var item in MapItemPresenters.Where(item => item != null && item.IsHighlighted))
    //        {
    //            item.IsHighlighted = false;
    //        }
    //    }

    //    public virtual void SetHighlightedItem(Item value)
    //    {
    //        // Inform the selector of the change prior to modifying the state
    //        this.ItemStateController.SetHighlightedItem(value, this);

    //        // A null value represents a request to clear the highlight
    //        if (value == null)
    //        {
    //            ClearHighlightedItem();
    //            return;
    //        }

    //        // Retrieve settings from service
    //        var tempHighlightedItem = new MapItem(value, this.DefaultMapPointSettings);

    //        if (this.HighlightedMapItemPresenter != null && this.HighlightedMapItemPresenter.MapItem != null &&
    //            this.HighlightedMapItemPresenter.MapItem.Id == tempHighlightedItem.Id)
    //            return;

    //        var cardToHighlight = this.MapItemPresenters.OfType<MapCardPresenter>().Where(x => x.MapItem.Id == tempHighlightedItem.Id);

    //        if (cardToHighlight.Any())
    //        {
    //            ClearHighlightedItem();
    //            var presenter = cardToHighlight.First();
    //            presenter.IsHighlighted = true;
    //        }
    //        else /* Buckets */
    //        {
    //            var bucketToHighlight =
    //                this.MapItemPresenters
    //                .OfType<MapBucketPresenter>()
    //                .Where(x =>
    //                    QuadKey
    //                    .ConvertCoordinateToQuadKey(tempHighlightedItem.Coordinate, dataSource.MaximumQuadKeyLevel)
    //                    .DescendantOfOrEqual(x.Quadkey)
    //                    )
    //                .FirstOrDefault();

    //            SetHighlightedBucket(bucketToHighlight);
    //        }
    //    }

    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
    //    public void SetHighlightedBucket(MapBucketPresenter bucketToHighlight)
    //    {
    //        ClearHighlightedItem();
    //        if (bucketToHighlight == null)
    //            return;
    //        // we are already explored so no need to highlight
    //        if (bucketToHighlight.IsExplored)
    //            return;
    //        bucketToHighlight.IsHighlighted = true;
    //    }

    //    #endregion

    //    #region Private Methods

    //    #region Event handlers

    //    protected void TrackpointPresenterExploredHandler(object sender, TrackpointPresenterExploredEventArgs e)
    //    {
    //        var item = this.MapItemPresenters.OfType<MapCardPresenter>().Where(x => x.MapItem.Id == e.ExploredMapItem.Id).FirstOrDefault();
    //        if (item == null)
    //            return;

    //        this.SetExploredMapItem(item);
    //    }

    //    protected void TrackpointPresenterHighlightedHandler(object sender, TrackpointHighlightedEventArgs e)
    //    {
    //        this.SetHighlightedItem(e.HighlightedItem);
    //    }

    //    #endregion

    //    /// <summary>
    //    /// Updates the map coordinates (animate).  Used when the data source changes.
    //    /// </summary>
    //    protected void UpdateMapViewport()
    //    {
    //        // Clean up items before loading new items.  This handles a null filter(no sessions)!
    //        if (this.mapCollectionsSynchronizer != null)
    //        {
    //            this.mapCollectionsSynchronizer.Clear(presenterDispatcher);
    //        }

    //        double newZoomLevel = 0.0;
    //        var newCenter = new Point(0, 0);

    //        try
    //        {
    //            if (dataSource.SouthwestCoordinate.HasValue && dataSource.NortheastCoordinate.HasValue)
    //            {
    //                var sw = dataSource.SouthwestCoordinate.Value;
    //                var ne = dataSource.NortheastCoordinate.Value;

    //                // Calculate the maximum zoom level that encompasses the entire quadrangle
    //                if (sw.Equals(new Coordinate(0, 0)) && sw.Equals(ne))
    //                {
    //                    newZoomLevel = 0;
    //                }
    //                else
    //                {
    //                    newZoomLevel = new Quadrangle(sw, ne).GetViewportZoomLevel();
    //                }

    //                newCenter = new Point((sw.Longitude + ne.Longitude) / 2.0, (sw.Latitude + ne.Latitude) / 2.0);
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            var logException = new ApplicationException("Exception when Updating MapCoordinates", e);
    //            ExceptionPolicy.HandleException(logException, "Default Policy");
    //        }

    //        this.ZoomLevel = (int)newZoomLevel;
    //        this.MapCenter = newCenter;

    //        OnMapChanged(new MapChangedEventArgs { ZoomLevel = newZoomLevel, Center = newCenter });
    //    }

    //    /// <summary>
    //    /// Updates the total item count for the control.
    //    /// </summary>
    //    private async void UpdateCount()
    //    {
    //        this.itemCount.IsPending = true;
    //        // Set up new tokens
    //        this.itemCountTokenSource = new CancellationTokenSource();
    //        // Retrieve the item count
    //        var task = this.dataSource.GetCountAsync(
    //            this.itemCountTokenSource.Token,
    //            presenterDispatcher);
    //        await task;
    //        var args = task.Result;
    //        if (args.Error != null)
    //        {
    //            SetError(new ApplicationException(Res.ErrorGettingItemCount, args.Error));
    //            return;
    //        }
    //        if (args.Cancelled)
    //            return;

    //        // TODO: VS2012 upgrade - Should we await? For now store task in variable to get around CS4014
    //        var loadTask = ((Dispatcher)args.UserState).BeginInvoke(DispatcherPriority.Loaded, new Action(delegate
    //        {
    //            this.itemCount.Value = args.ItemCount;
    //            this.ItemCountTimestamp = args.Timestamp;
    //            this.RaisePropertyChanged(() => ItemCount);
    //        }));
    //    }

    //    protected void OnMapChanged(MapChangedEventArgs eventArgs)
    //    {
    //        if (MapChanged != null)
    //            MapChanged(this, eventArgs);
    //    }

    //    protected void OnHeatMapChanged(EventArgs eventArgs)
    //    {
    //        if (this.HeatMapChanged != null)
    //            this.HeatMapChanged(this, eventArgs);
    //    }

    //    /// <summary>
    //    /// Handles a filter modification. Only updates if we are the active perspective
    //    /// </summary>
    //    /// <remarks>
    //    /// If the filter is changed by another control, we need to set a navigation starting point for this control
    //    /// </remarks>
    //    /// <param name="sender">The Sender.</param>
    //    /// <param name="e">The <see cref="Jsi.UX.DataAccess.Filters.FilterBuilderChangedEventArgs"/> instance containing the event data.</param>
    //    private void FilterBuilderChangedHandler(object sender, FilterChangedEventArgs e)
    //    {
    //        if (this.IsActive)
    //        {
    //            if (e.Sender == this) return;

    //            UpdateDataSource();
    //            Refresh();
    //        }
    //        else
    //            this.isDirty = true;
    //    }

    //    internal abstract void Refresh();

    //    private void DataSourceChangedHandler(object sender, EventArgs e)
    //    {
    //        // Invalidate tokens that relied on the old data source
    //        if (this.itemCountTokenSource != null && !this.itemCountTokenSource.IsCancellationRequested)
    //        {
    //            this.itemCountTokenSource.Cancel();
    //        }
    //        // TODO: Invalidate all other outstanding tokens

    //        UpdateCount();
    //        UpdateMapViewport();
    //    }

    //    private void PublishContextualRangeFilter()
    //    {
    //        const int maximumDrilldown = 5;

    //        // If we're publishing but we're not the active perspective, then do nothing.
    //        if (!this.IsActive) return;

    //        // if the view port has not been set yet than use the base quad keys
    //        var quadKeys = this.GeographicalViewport == default(Quadrangle) ?
    //            new List<QuadKey> { new QuadKey("0"), new QuadKey("1"), new QuadKey("2"), new QuadKey("3") } :
    //            QuadKey.CalculateBestFitQuadKeys(this.GeographicalViewport, maximumDrilldown, this.mappingSettings.MaxCubeQuadkeyHierarchyDepth);

    //        using (var updater = this.filterBuilder.ContextualFilters.GetUpdater(this))
    //        {
    //            updater.ClearFilters(this, new ContextualFilterType[] { ContextualFilterType.Selection, ContextualFilterType.Visibility });

    //            foreach (var attributeCriterial in this.dataSource.QuadKeysToAttributeCriteriaList(quadKeys))
    //            {
    //                updater.Add(new ContextualFilter(attributeCriterial, ContextualFilterType.Visibility, this));
    //            }

    //            if (this.exploredBucket != null)
    //            {
    //                updater.Add(new ContextualFilter(
    //                                new AttributeValueCriteria(
    //                                    this.mapBucketAttributeDefinitions[this.exploredBucket.MapBucket.MapPointSource],
    //                                    this.exploredBucket.MapBucket.AttributeMember),
    //                                ContextualFilterType.Selection,
    //                                this));
    //            }

    //        }
    //    }

    //    private void SetContextualImplicitFilter()
    //    {
    //        if (string.IsNullOrEmpty(this.Settings.AttributeMemberTemplate) ||
    //            string.IsNullOrEmpty(this.Settings.ContextualFilterAttributeDefinitionId) ||
    //            string.IsNullOrEmpty(this.Settings.ContextualFilterAttributeDefinitionName) ||
    //            string.IsNullOrEmpty(this.Settings.ContextualFilterAttributeMemberKey) ||
    //            string.IsNullOrEmpty(this.Settings.ContextualFilterAttributeMemberValue))
    //            return;

    //        var hasLocationCriteria =
    //            new AttributeValueCriteria
    //            (
    //                new AttributeDefinition
    //                (
    //                    this.Settings.ContextualFilterAttributeDefinitionId,
    //                    this.Settings.ContextualFilterAttributeDefinitionName,
    //                    AttributeType.None
    //                ),
    //                new AttributeMember
    //                (
    //                    this.Settings.AttributeMemberTemplate.FormatInvariantCulture(this.Settings.ContextualFilterAttributeDefinitionId, this.Settings.ContextualFilterAttributeMemberKey),
    //                    this.Settings.ContextualFilterAttributeMemberValue,
    //                    0
    //                ),
    //                true
    //           );

    //        using (var updater = this.filterBuilder.ContextualFilters.GetUpdater(this))
    //        {
    //            updater.ClearFilters(this, new ContextualFilterType[] { ContextualFilterType.Implicit });
    //            updater.Add(new ContextualFilter<ContextualImplicitFilterPayload>(hasLocationCriteria, ContextualFilterType.Implicit, this, new ContextualImplicitFilterPayload(null)));
    //        }

    //        UpdateDataSource();
    //    }

    //    private void ClearContextualImplicitFilter()
    //    {
    //        using (var updater = this.filterBuilder.ContextualFilters.GetUpdater(this))
    //        {
    //            updater.ClearFilters(this, new ContextualFilterType[] { ContextualFilterType.Implicit });
    //        }
    //    }

    //    private void ClearContextualRangeFilter()
    //    {
    //        using (var updater = this.filterBuilder.ContextualFilters.GetUpdater(this))
    //        {
    //            updater.ClearFilters(this, new ContextualFilterType[] { ContextualFilterType.Selection, ContextualFilterType.Visibility });
    //        }
    //    }

    //    #endregion

    //    #region Public Methods

    //    /// <summary>
    //    /// Handles when the viewport has changed
    //    /// </summary>
    //    /// <param name="zoom">The Zoom level</param>
    //    /// <param name="targetZoom">the target zoom level</param>
    //    /// <param name="viewport">the viewport quadrangle</param>
    //    public void ViewportChanged(double zoom, double targetZoom, Quadrangle viewport)
    //    {
    //        this.CurrentZoom = zoom;
    //        this.TargetZoom = targetZoom;
    //        this.GeographicalViewport = viewport;
    //        RaisePropertyChanged("Viewport");

    //        if (OnViewportChanged != null)
    //            OnViewportChanged(this, new MapViewportEventArgs
    //            {
    //                Zoom = zoom,
    //                TargetZoom = targetZoom,
    //                GeographicalViewport = viewport,
    //                Quadkeys = TileSystem.GetQuadkeysFromQuadrangle(this.GeographicalViewport)
    //            });
    //    }

    //    /// <summary>
    //    /// Navigates the specified direction.  There is a mapping issue that won't allow us to navigate our collection with 
    //    /// the arrow keys. Therefore we delegate navigation to the filmstrip. Filmstrip will notify us with the new explored item.
    //    /// </summary>
    //    /// <param name="direction">The direction.</param>
    //    internal void Navigate(FocusNavigationDirection direction)
    //    {
    //        var viewable = MapItemPresenters.Where(x => GeographicalViewport.Contains(new Coordinate(x.Latitude.GetValueOrDefault(), x.Longitude.GetValueOrDefault())));

    //        // If there are no items or buckets, exit.
    //        if (!viewable.Any())
    //            return;

    //        // Expect only one selection.
    //        if (this.ItemStateController.ExploredItem == null || this.ItemStateController.ExploredItem.Value == null)
    //            return;

    //        // Mapping has delegated navigation to the filmstrip
    //        var payload = new DynamicEventArgs(this, DynamicEventType.Filmstrip.Navigate);
    //        payload.Args.FocusNavigationDirection = direction;
    //        this.eventAggregator.Publish(payload);
    //    }

    //    #endregion

    //    #region IActiveAware

    //    public bool IsActive
    //    {
    //        get { return Get(() => this.IsActive); }
    //        set { Set(() => this.IsActive, value, OnIsActiveChanged); }
    //    }

    //    public event EventHandler IsActiveChanged;

    //    private void OnIsActiveChanged()
    //    {
    //        if (this.IsActive)
    //        {
    //            PublishContextualRangeFilter();
    //            SetContextualImplicitFilter();

    //            if (this.isDirty)
    //                UpdateDataSource();
    //        }
    //        else
    //        {
    //            this.ClearContextualRangeFilter();
    //            this.ClearMostSpecificExploredObject();
    //            this.ClearContextualImplicitFilter();
    //            this.CollectionSynchronizer.ClearSelection();
    //        }

    //        if (this.IsActiveChanged != null)
    //            this.IsActiveChanged(this, EventArgs.Empty);
    //    }

    //    private void UpdateDataSource()
    //    {
    //        this.dataSource.Filter = filterBuilder.CurrentFilter;
    //        this.isDirty = false;
    //    }

    //    #endregion


    //    public double CurrentZoom { get; set; }

    //    public double TargetZoom { get; set; }
    //}

    //public class MapPerspectiveViewModel : MapPerspectiveViewModelBase
    //{
    //    #region Fields

    //    private readonly IItemLinksPresenter itemLinksPresenter;
    //    private bool isTrackingEnabled = true;
    //    private bool isTrackingVisible;
    //    private readonly ISelectionController selectionController;

    //    /// <summary>
    //    /// Presenter for managing all trackpoints for all sessions
    //    /// </summary>
    //    private readonly ITrackpointLayerPresenter trackpoints;

    //    /// <summary>
    //    /// Presenter for managing all accessPoints for all sessions
    //    /// </summary>
    //    private readonly IAccessPointLayerPresenter accessPoints;

    //    #endregion

    //    #region Properties

    //    public bool AllowsMultiSelection
    //    {
    //        get
    //        {
    //            return this.ClientSettings.AllowMultiSelection;
    //        }
    //    }

    //    public bool DisplayTrackpointsFeature
    //    {
    //        get { return true; }
    //    }

    //    public IItemLinksPresenter ItemLinks
    //    {
    //        get
    //        {
    //            return this.itemLinksPresenter;
    //        }
    //    }


    //    public bool ZoomToResult
    //    {
    //        get { return Get(() => ZoomToResult, true); }
    //        set { Set(() => ZoomToResult, value); }
    //    }


    //    #endregion

    //    /// <summary>
    //    /// Gets the presenter that controls trackpoints.
    //    /// </summary>
    //    /// <value>The presenter that controls trackpoints.</value>
    //    public ITrackpointLayerPresenter Trackpoints
    //    {
    //        get { return this.trackpoints; }
    //    }

    //    /// <summary>
    //    /// Gets the presenter that controls accessPoints.
    //    /// </summary>
    //    /// <value>The presenter that controls accessPoints.</value>
    //    public IAccessPointLayerPresenter AccessPoints
    //    {
    //        get { return this.accessPoints; }
    //    }

    //    /// <summary>
    //    /// Gets or sets a value indicating whether tracking is currently enabled
    //    /// for the map view.
    //    /// </summary>
    //    /// <value>
    //    /// 	<c>true</c> if tracking is currently enabled; otherwise, <c>false</c>.
    //    /// </value>
    //    public bool IsTrackingEnabled
    //    {
    //        get { return this.isTrackingEnabled; }
    //        set
    //        {
    //            if (this.isTrackingEnabled == value)
    //                return;

    //            this.isTrackingEnabled = value;

    //            this.Trackpoints.Enabled = value;
    //            this.itemLinksPresenter.Enabled = value && this.ItemLinksVisible;
    //            this.AccessPoints.Enabled = value && this.ShapesVisible;
    //            this.RaisePropertyChanged(() => this.IsTrackingEnabled);
    //            this.RaisePropertyChanged(() => this.IsLinkingEnabled);
    //        }
    //    }


    //    public bool IsLinkingEnabled
    //    {
    //        get { return (this.isTrackingEnabled && this.isTrackingVisible); }

    //    }

    //    private bool itemLinksVisible = true;
    //    public bool ItemLinksVisible
    //    {
    //        get { return this.itemLinksVisible; }
    //        set
    //        {
    //            if (this.itemLinksVisible == value)
    //                return;

    //            this.itemLinksVisible = value;
    //            //this line is unifying the visibility of session linking with trackpoint paths
    //            //from the end user point of view we should not differenciate the two types of links.
    //            if (this.trackpoints != null)
    //                this.trackpoints.PathsVisible = value;
    //            this.itemLinksPresenter.Enabled = value;
    //            this.RaisePropertyChanged(() => this.ItemLinksVisible);
    //        }
    //    }

    //    private bool shapesVisible = true;
    //    public bool ShapesVisible
    //    {
    //        get { return this.shapesVisible; }
    //        set
    //        {
    //            if (this.shapesVisible == value)
    //                return;

    //            this.shapesVisible = value;
    //            if (this.trackpoints != null)
    //                this.trackpoints.ShapesVisible = value;
    //            if (this.accessPoints != null)
    //                this.accessPoints.Enabled = value;
    //            this.RaisePropertyChanged(() => this.ShapesVisible);
    //        }
    //    }

    //    private object view;
    //    public override object View
    //    {
    //        get
    //        {
    //            return this.view ?? (this.view = new MapPerspectiveView { ViewModel = this });
    //        }
    //    }

    //    protected override void SelectedViewOptionsChanged(MapViewType selectedMapViewTypeOptions)
    //    {
    //        switch (selectedMapViewTypeOptions.MapType)
    //        {
    //            case MapViewTypes.Locations:
    //                this.IsTrackingEnabled = true;
    //                this.HeatMap.ShowHeatMap = false;
    //                break;
    //            case MapViewTypes.HeatMap:
    //                this.IsTrackingEnabled = false;
    //                this.HeatMap.ShowHeatMap = true;
    //                break;
    //        }
    //    }

    //    /// <summary>
    //    /// Gets or sets whether to show tracking information on the map.  
    //    /// This indicates only the desired value.  If tracking is not enabled, then setting
    //    /// this property to <c>true</c> should not cause the map to render tracking information.
    //    /// </summary>
    //    /// <value>
    //    /// 	<c>true</c> if tracking information should be shown on the map; otherwise, <c>false</c>.
    //    /// </value>
    //    public bool IsTrackingVisible
    //    {
    //        get { return this.isTrackingVisible; }
    //        set
    //        {
    //            if (this.isTrackingVisible == value)
    //                return;

    //            this.isTrackingVisible = value;

    //            this.Trackpoints.ShowTrackpoints = value;
    //            this.ItemLinks.ShowItemLinks = value;
    //            this.AccessPoints.ShowAccessPoints = value;

    //            this.RaisePropertyChanged(() => this.IsTrackingVisible);
    //            this.RaisePropertyChanged(() => this.IsLinkingEnabled);

    //            // Showing/hiding tracking should deselect any previously explored buckets as this will affect the items in the filmstrip
    //            ClearExploredBucket();
    //        }
    //    }

    //    public MapSelectionMode SelectionMode
    //    {
    //        get { return Get(() => this.SelectionMode); }
    //        set { Set(() => this.SelectionMode, value); }
    //    }


    //    #region Constructors

    //    [InjectionConstructor]
    //    [ImportingConstructor]
    //    public MapPerspectiveViewModel(IWorkspace workspace,
    //                                   IEventAggregator eventAggregator,
    //                                   IMapTileSourceProvider tileSourceProvider,
    //                                   IMappingDataSource dataSource,
    //                                   IFilterBuilder filterBuilder,
    //                                   IMappingSettings mappingSettings,
    //                                   IMapPerspectiveSettings clientSettings,
    //                                   IMapPointSettings mapPointSettings,
    //                                   ITrackpointLayerPresenter trackpoints,
    //                                   IAccessPointLayerPresenter accessPoints,
    //                                   IMapCollectionsSynchronizer collectionSynchronizer,
    //                                   IItemLinksPresenter itemLinksPresenter,
    //                                   IItemStateController itemStateController,
    //                                   ISelectionController selectionController)
    //        : this(workspace, eventAggregator, tileSourceProvider, dataSource,
    //        filterBuilder, mappingSettings, clientSettings, mapPointSettings, trackpoints, accessPoints, collectionSynchronizer, itemLinksPresenter, itemStateController, selectionController, Dispatcher.CurrentDispatcher, DispatcherScheduler.Instance)
    //    {
    //        // left empty
    //    }

    //    internal MapPerspectiveViewModel(IWorkspace workspace,
    //                                   IEventAggregator eventAggregator,
    //                                   IMapTileSourceProvider tileSourceProvider,
    //                                   IMappingDataSource dataSource,
    //                                   IFilterBuilder filterBuilder,
    //                                   IMappingSettings mappingSettings,
    //                                   IMapPerspectiveSettings clientSettings,
    //                                   IMapPointSettings mapPointSettings,
    //                                   ITrackpointLayerPresenter trackpoints,
    //                                   IAccessPointLayerPresenter accessPoints,
    //                                   IMapCollectionsSynchronizer collectionSynchronizer,
    //                                   IItemLinksPresenter itemLinksPresenter,
    //                                   IItemStateController itemStateController,
    //                                   ISelectionController selectionController,
    //                                   Dispatcher presenterDispatcher,
    //                                   IScheduler observeOnScheduler)
    //        : base(workspace, eventAggregator, tileSourceProvider, dataSource, filterBuilder, mappingSettings, clientSettings, new List<IMapPointSettings> { mapPointSettings }, collectionSynchronizer, itemStateController, presenterDispatcher, observeOnScheduler)
    //    {
    //        this.trackpoints = trackpoints;
    //        this.trackpoints.ViewportNotifier = this;
    //        this.trackpoints.OnTrackpointPresenterSelected += TrackpointPresenterExploredHandler;
    //        this.trackpoints.OnTrackpointPresenterHighlighted += TrackpointPresenterHighlightedHandler;
    //        this.Trackpoints.ExceptionRaised += delegate(object sender, ExceptionEventArgs e)
    //        {
    //            SetError(e.Exception);
    //        };
    //        // Ensure tracking presenters are synched with current values.
    //        this.trackpoints.Enabled = this.IsTrackingEnabled;
    //        this.trackpoints.ShowTrackpoints = this.IsTrackingVisible;
    //        this.trackpoints.PathsVisible = this.ItemLinksVisible;
    //        this.trackpoints.ShapesVisible = this.ShapesVisible;

    //        this.accessPoints = accessPoints;
    //        this.accessPoints.ViewportNotifier = this;
    //        this.accessPoints.ExceptionRaised += delegate(object sender, ExceptionEventArgs e)
    //        {
    //            SetError(e.Exception);
    //        };
    //        this.accessPoints.Enabled = this.IsTrackingEnabled;
    //        this.accessPoints.ShowAccessPoints = this.IsTrackingVisible;

    //        this.itemLinksPresenter = itemLinksPresenter;
    //        this.itemLinksPresenter.ExceptionRaised += delegate(object sender, ExceptionEventArgs e)
    //        {
    //            SetError(e.Exception);
    //        };
    //        this.itemLinksPresenter.ShowItemLinks = this.IsTrackingVisible;
    //        this.itemLinksPresenter.Enabled = this.IsTrackingEnabled;

    //        // read if the trackpoints feature is enabled/disabled (e.g. Proximity has no trackpoints)
    //        // if the value is missing the default is to turn on the trackpoint functionality
    //        this.selectionController = selectionController;
    //        this.InitializeData();
    //    }

    //    #endregion

    //    protected override void ViewportUpdated(Quadrangle viewport)
    //    {
    //        this.itemLinksPresenter.Update(viewport);
    //    }

    //    protected override MapVirtualSource GetMapVirtualSource()
    //    {
    //        return new MapVirtualSource(this, this.DataSource, this.Settings, this.CollectionSynchronizer, this.trackpoints, this.accessPoints, this.PresenterDispatcher);
    //    }

    //    internal override void SetExploredMapItem(MapItemPresenterBase presenter)
    //    {
    //        base.SetExploredMapItem(presenter);
    //    }

    //    /// <summary>
    //    /// this method selects items
    //    /// </summary>
    //    /// <param name="item"></param>
    //    internal void ToggleItem(MapItemPresenterBase item)
    //    {
    //        this.CollectionSynchronizer.ToggleItemSelection(item);
    //    }

    //    /// <summary>
    //    /// this method is used to selects multiple items
    //    /// </summary>
    //    /// <param name="items"></param>
    //    internal void SelectItems(IEnumerable<MapItemPresenterBase> items)
    //    {
    //        this.selectionController.Add(items.Select(PresenterToSelectableResolver.New));
    //    }

    //    /// <summary>
    //    /// this method "deselects" multiple items (if previously selected)
    //    /// </summary>
    //    /// <param name="items"></param>
    //    internal void DeselectItems(IEnumerable<MapItemPresenterBase> items)
    //    {
    //        this.selectionController.Remove(items.Select(PresenterToSelectableResolver.New));
    //    }

    //    internal override void Refresh()
    //    {
    //        // Only force a refresh if we didn't fire the previous block. 
    //        // Setting this.dataSource.Filter triggers an update as well.
    //        UpdateMapViewport();
    //        this.trackpoints.Refresh();
    //        this.ItemLinks.Update(this.GeographicalViewport);
    //    }

    //    protected override void ClearHighlightedItem()
    //    {
    //        base.ClearHighlightedItem();

    //        if (this.Trackpoints.IsVisible)
    //        {
    //            foreach (var pointPresenter in this.Trackpoints.Points.Where(item => item.IsHighlighted))
    //                pointPresenter.IsHighlighted = false;
    //        }
    //    }

    //    public override void SetHighlightedItem(Item value)
    //    {
    //        base.SetHighlightedItem(value);

    //        if (value != null && this.Trackpoints.IsVisible)
    //        {
    //            var pointsToHighlight = this.Trackpoints.Points.Where(x => x.MapItem.Id == value.Id);
    //            foreach (var pointPresenter in pointsToHighlight)
    //                pointPresenter.IsHighlighted = true;
    //        }
    //    }

    //    public void SetSelection(Quadrangle quadrangle)
    //    {
    //        this.CollectionSynchronizer.SetSelection(quadrangle);
    //    }

    //    public void ClearSelection(Quadrangle quadrangle)
    //    {
    //        this.CollectionSynchronizer.ClearSelection(quadrangle);
    //    }

    //    public void SetSelectionPreview(Quadrangle quadrangle)
    //    {
    //        this.CollectionSynchronizer.SetIsSelectionPreview(quadrangle);
    //    }

    //    public void ClearSelection()
    //    {
    //        this.CollectionSynchronizer.ClearSelection();
    //    }

    //    public void ClearSelectionPreview()
    //    {
    //        this.CollectionSynchronizer.ClearSelectionPreview();
    //    }

    //}
}
