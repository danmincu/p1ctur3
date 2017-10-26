using C1.WPF.Maps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Telekad.Mapping;

namespace P1ctur3.Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

         public MainWindow()
        {
            InitializeComponent();

            this.Loaded += (sender, e) =>
            {
                Keyboard.AddPreviewKeyDownHandler(Application.Current.MainWindow, HandleKeyEvent);
                Keyboard.AddPreviewKeyUpHandler(Application.Current.MainWindow, HandleKeyEvent);
            };

            this.Unloaded += (sender, e) =>
            {
                Keyboard.RemovePreviewKeyDownHandler(Application.Current.MainWindow, HandleKeyEvent);
                Keyboard.RemovePreviewKeyUpHandler(Application.Current.MainWindow, HandleKeyEvent);
            };

            this.ViewModel = new MapPerspectiveViewModel(new MemoryDataSource());           

        }
        
        private bool initialized;

        private void HandleKeyEvent(object sender, KeyEventArgs e)
        {
            //if (this.ViewModel.HeatMap.ShowHeatMap || !this.ViewModel.AllowsMultiSelection)
            //    return;

            //if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl || e.Key == Key.LeftShift || e.Key == Key.RightShift)
            //{
            //    this.rectangleSelector.IsSelectionEnabled = Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);

            //    if (this.rectangleSelector.IsSelectionEnabled)
            //    {
            //        if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            //        {
            //            this.ViewModel.SelectionMode = MapSelectionMode.Exclusive;
            //            this.rectangleSelector.Indicator = RectangleSelectionIndicators.Minus;
            //        }
            //        else
            //        {
            //            this.ViewModel.SelectionMode = MapSelectionMode.Inclusive;
            //            this.rectangleSelector.Indicator = RectangleSelectionIndicators.Plus;
            //        }
            //    }
            //    else
            //    {
            //        this.ViewModel.SelectionMode = MapSelectionMode.None;
            //        this.ViewModel.ClearSelectionPreview();
            //    }
            //}
        }

        //public MapPerspectiveViewModel ViewModel
        //{
        //    get { return (MapPerspectiveViewModel)this.DataContext; }
        //    set
        //    {
        //        this.DataContext = value;

        //        // Initialize the control here once the presenter is properly loaded.
        //        Initialize();

        //        this.ViewModel.MapChanged += (sender, e) => RefreshMapControl();
        //    }
        //}

       

        public MapPerspectiveViewModel ViewModel {
            get { return (MapPerspectiveViewModel)this.DataContext; }
            set
            {
                this.DataContext = value;

                // Initialize the control here once the presenter is properly loaded.
                Initialize();

                this.ViewModel.MapChanged += (sender, e) => RefreshMapControl();
            }
        }

        /// <summary>
        /// Refreshes map control
        /// </summary>
        /// <remarks>
        /// We used Component1 TargetZoom instead of just Zoom to "animate" zooming of the map
        /// We didn't use targetCenter for the same reason due to conflict with out BringItemToFront animation
        /// </remarks>
        private void RefreshMapControl()
        {
            if (this.ViewModel.ZoomToResult)
                this.C1Map.SetCenterAndTargetZoom(this.ViewModel.MapCenter, this.ViewModel.ZoomLevel);
            

            itemsLayer.ForceRefresh();
            
        }

        private void ToggleButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.ViewModel.ZoomToResult)
                RefreshMapControl();
        }

        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            // Only fire navigation, if we have item selection
            //if (this.ViewModel.ExploredMapItemPresenter != null)
            //{
            //    switch (e.Key)
            //    {
            //        case Key.Escape:
            //            ViewModel.ClearMostSpecificExploredObject();
            //            break;
            //        case Key.Up:
            //            this.ViewModel.Navigate(FocusNavigationDirection.Up);
            //            break;
            //        case Key.Down:
            //            this.ViewModel.Navigate(FocusNavigationDirection.Down);
            //            break;
            //        case Key.Left:
            //            this.ViewModel.Navigate(FocusNavigationDirection.Left);
            //            break;
            //        case Key.Right:
            //            this.ViewModel.Navigate(FocusNavigationDirection.Right);
            //            break;
            //        case Key.Home:
            //            this.ViewModel.Navigate(FocusNavigationDirection.First);
            //            break;
            //        case Key.End:
            //            this.ViewModel.Navigate(FocusNavigationDirection.Last);
            //            break;
            //        case Key.Space:
            //            this.ViewModel.Navigate(FocusNavigationDirection.Next);
            //            break;
            //        default:
            //            e.Handled = false;
            //            break;
            //    }
            //}

            //if (e.Key == Key.D && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            //{
            //    DebugWindow.Visibility = DebugWindow.Visibility == System.Windows.Visibility.Collapsed ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            //}

            // TODO: Investigate the possible C1Map bug further
            // We mark the event as handled, because we don't want the C1 map to handle
            // the key events (tunnel down). This is in response to bug #3923.
            e.Handled = true;

            base.OnPreviewKeyUp(e);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            // TODO: Investigate the possible C1Map bug further
            // We mark the event as handled, because we don't want the C1 map to handle
            // the key events (tunnel down). This is in response to bug #3923.
            e.Handled = true;
            base.OnPreviewKeyDown(e);
        }

        void ViewModelPropertyChangedHanlder(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DrilldownDepth" || e.PropertyName == "MaxBucketSize")
            {
                this.itemsLayer.ForceRefresh();
            }
        }

        /// <summary>
        /// Initializes this instance.  This method should be called when the view is loaded.
        /// </summary>
        private void Initialize()
        {
            // ensure that initialize is only called once.
            if (initialized)
                return;

            initialized = true;

            //create map slices so for each of the quadkeys in the cube there one and only one callback
            //the size of the slice is 256x256
            ICollection<MapSlice> slices = new List<MapSlice>();

            for (int i = 0; i < this.ViewModel.Settings.MaxCubeQuadkeyHierarchyDepth; i++)
            {
                var numSlices = (int)Math.Pow(2, i + 1);
                slices.Add(new MapSlice(numSlices, numSlices, i));
            }

            foreach (var slice in slices)
            {
                itemsLayer.Slices.Add(slice);
            }


            this.C1Map.ZoomChanged += delegate
            {
                // We force a refresh to invalidate the heatmap slice
                // in order to hide the heatmap while zooming.
                // Fix for bug #4050.
                UpdateViewModel();

            };

            this.C1Map.CenterChanged += delegate { UpdateViewModel(); };

            //this.ViewModel.PropertyChanged += ViewModelPropertyChangedHanlder;

            // Hook up the heat map source here.  We can't do this in SetPresenter because the presenter constructor has not yet run.
            itemsLayer.MapItemsSource = this.ViewModel.MapSource;

            RefreshMapControl();
        }

        private void UpdateViewModel()
        {
            // Don't update until after map has loaded or the app will crash
            if (ViewModel == null || !C1Map.IsLoaded)
                return;
            var topLeft = this.C1Map.ScreenToGeographic(new Point(0, 0));
            var bottomRight = this.C1Map.ScreenToGeographic(new Point(C1Map.ActualWidth, C1Map.ActualHeight));
            var viewPort = Quadrangle.ReversedQuadrangle(TileSystem.ClipToRange(topLeft.Y, topLeft.X), TileSystem.ClipToRange(bottomRight.Y, bottomRight.X));
           // ViewModel.ViewportChanged(this.C1Map.Zoom, this.C1Map.TargetZoom, viewPort);
        }

        private void MapItemContentControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Prevent the map from capturing the mouse and initiating a pan on the map.
            // This way we can handle the selection on mouse up.
            e.Handled = true;
        }

        private void MapItemContentControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            //// Currently only handling MapCardPresenter, how will we handle bucket?
            //var contentControl = sender as ContentControl;
            //if (contentControl == null)
            //    return;

            //var mapBucketPresenter = contentControl.Content as MapBucketPresenter;
            //if (mapBucketPresenter != null)
            //{

            //    if (this.ViewModel.AllowsMultiSelection && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
            //    {
            //        this.ViewModel.ToggleItem(mapBucketPresenter);
            //    }
            //    else
            //    {
            //        this.ViewModel.SetExploredMapItem(mapBucketPresenter);
            //    }
            //}
            //else
            //{
            //    var mapItemPresenter = contentControl.Content as MapCardPresenter;
            //    if (mapItemPresenter != null)
            //    {
            //        if (this.ViewModel.AllowsMultiSelection && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
            //        {
            //            this.ViewModel.ToggleItem(mapItemPresenter);
            //        }
            //        else
            //        {
            //            this.ViewModel.SetExploredMapItem(mapItemPresenter);
            //        }
            //    }
            //}
        }


        /// <summary>
        /// Handles the MouseLeftButtonDown event of the map control. This event unselects the selected mapBucket
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void C1Map_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //var container = itemStaticLayer.ItemContainerGenerator.ContainerFromItem(ViewModel.ExploredMapItemPresenter) as FrameworkElement;
            //if (container != null)
            //    Panel.SetZIndex(container, 1);

            //if (ViewModel.ExploredMapItemPresenter != null)
            //{
            //    ViewModel.ClearMostSpecificExploredObject();
            //}

        }

        private void MapItemContentControl_MouseEnter(object sender, MouseEventArgs e)
        {
            //var content = ((ContentControl)sender).Content;
            //if (content == null)
            //    return;

            //if (content is MapCardPresenter)
            //{
            //    ViewModel.SetHighlightedItem((content as MapCardPresenter).MapItem.AsItem());
            //}
            //else if (content is MapBucketPresenter)
            //{
            //    ViewModel.SetHighlightedBucket(content as MapBucketPresenter);
            //}
        }

        private void MapItemContentControl_MouseLeave(object sender, MouseEventArgs e)
        {
            //var content = ((ContentControl)sender).Content;
            //if (content == null)
            //    return;

            //if (content is MapItemPresenterBase)
            //{
            //    ViewModel.SetHighlightedItem(null);
            //}
        }

        //private void RectangleSelectionDecorator_RectangleSelectionEnd(object sender, UX.Controls.RectangleSelectionEventArgs e)
        //{
        //    OnRectangleSelection(e.SelectionRect);
        //}

        //private void OnRectangleSelection(Rect selectionRect)
        //{
        //    //this.ViewModel.ClearSelectionPreview();
        //    //var bl = this.ConvertToCoordinate(this.C1Map.ScreenToGeographic(selectionRect.BottomLeft));
        //    //var tr = this.ConvertToCoordinate(this.C1Map.ScreenToGeographic(selectionRect.TopRight));
        //    //var quadrangle = new Quadrangle(bl, tr);
        //    //if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
        //    //    this.ViewModel.ClearSelection(quadrangle);
        //    //else
        //    //    this.ViewModel.SetSelection(quadrangle);
        //}

        //private void RectangleSelectionDecorator_RectangleSelectionChanged(object sender, UX.Controls.RectangleSelectionEventArgs e)
        //{
        //    //var bl = this.ConvertToCoordinate(this.C1Map.ScreenToGeographic(e.SelectionRect.BottomLeft));
        //    //var tr = this.ConvertToCoordinate(this.C1Map.ScreenToGeographic(e.SelectionRect.TopRight));
        //    //var quadrangle = new Quadrangle(bl, tr);
        //    //this.ViewModel.SetSelectionPreview(quadrangle);
        //}

        private void C1MapItemsLayer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            //{
            //    var fe = e.OriginalSource as FrameworkElement;
            //    if (fe == null)
            //        return;
            //    var tpPresenter = fe.DataContext as TrackpointPresenter;
            //    if (tpPresenter == null)
            //        return;
            //    this.ViewModel.ToggleItem(tpPresenter);
            //    e.Handled = true;
            //}
        }

        /// <summary>
        /// This constructor is clipping to world coordinate range a given input point 
        /// </summary>
        /// <param name="point">coordinate point</param>
        private Coordinate ConvertToCoordinate(System.Windows.Point point)
        {
            return new Coordinate(Math.Min(Math.Max(point.Y, -90F), 90F), Math.Min(Math.Max(point.X, -180F), 180F));
        }

     
        private List<DependencyObject> hitResultList = new List<DependencyObject>();

        private HitTestResultBehavior RectangleSelectionHitTestResult(HitTestResult result)
        {
            // Add the hit test result to the list that will be processed after the enumeration.
            hitResultList.Add(result.VisualHit);

            // Set the behavior to return visuals at all z-order levels.
            return HitTestResultBehavior.Continue;
        }


    }

}
