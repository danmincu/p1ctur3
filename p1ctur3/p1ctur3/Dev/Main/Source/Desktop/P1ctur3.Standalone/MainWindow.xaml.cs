using P1ctur3.Standalone.Models;
using P1ctur3.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace P1ctur3.Standalone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public static string StorageServiceUri = "http://localhost/P1ctur3.Web/Services/P1ctur3DataService.svc";
        public static string StorageServiceUri = "http://ownmeca0.w15.wh-2.com/p1ctur3/Services/P1ctur3DataService.svc/";

        private MainModel model = new MainModel();
        CancellationTokenSource cancelation;
        public MainWindow()
        {
            InitializeComponent();

            if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("Email"))
                model.Email = System.Configuration.ConfigurationManager.AppSettings["Email"];

            foreach (var item in Models.DrivePhotoInfo.GetDrives(new Exif.ExifProcessor(), this.model).Reverse())
            {
                model.Drives.Insert(0, item);
                item.WhenScanDone = () =>
                {
                    var nextDrive = model.Drives.SkipWhile(d => d.DriveLetter != item.DriveLetter).Skip(1).FirstOrDefault(d => d.IsReady && d.IsSelected != false);
                    if (nextDrive != null)
                        nextDrive.Scan(cancelation.Token, this.model);
                };
            }

            //var imsr = new P1ctur3.Storage.DataService.ImageMetadataDataServiceStorage(new Uri(MainWindow.StorageServiceUri));
            //var pics = imsr.Get(new IImageMetadataFilter() { Email = this.model.Email, DirectoryName = "E:\\Cuba 2011" });
            //this.model.Pictures = pics.Select(p => new Uri(p.StorageInfo.GetUrl("s")));                


            this.DataContext = model;


        }


        private void SearchForPictures()
        {
            foreach (var drive in model.Drives.Where(d => d.Directories != null))
            {
                drive.Directories.Clear();
            }

            if (model.Drives.Count(d => d.IsSelected != false && d.IsReady) > 0)
                model.Drives.First(d => d.IsSelected != false && d.IsReady).Scan(cancelation.Token, this.model);
        }


        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (this.cancelation != null)
                this.cancelation.Cancel();
            this.cancelation = new CancellationTokenSource();
            this.SearchForPictures();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (this.cancelation != null)
                this.cancelation.Cancel();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            foreach (var drive in this.model.Drives.Where(a => a.IsSelected != false && a.IsReady))
            {
                foreach (var directory in drive.Directories.Where(d => d.IsSelected != false))
                {
                    directory.Upload(this.model);
                }

            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var imsr = new P1ctur3.Storage.DataService.ImageMetadataDataServiceStorage(new Uri(MainWindow.StorageServiceUri), null);
            if (string.IsNullOrEmpty(model.Email) || imsr.Get(model.Email) == null)
            {
                MessageBox.Show("Unspecified or unregistered email. Go to http://p1cture.com and register your email first!");
                return;
            }

            ((Button)sender).IsEnabled = false;

            foreach (var drive in this.model.Drives.Where(a => a.IsSelected != false && a.IsReady))
            {
                foreach (var directory in drive.Directories.Where(d => d.IsSelected != false))
                {
                    await directory.DirectUploadTask(this.model);
                    directory.IsSelected = false;
                }
            }
            ((Button)sender).IsEnabled = true;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var imsr = new P1ctur3.Storage.DataService.ImageMetadataDataServiceStorage(new Uri(MainWindow.StorageServiceUri), null);
            //var pictures = imsr.Get(new IImageMetadataFilter() { Email = this.model.Email });
            foreach (var drive in this.model.Drives.Where(a => a.IsSelected != false && a.IsReady))
            {
                foreach (var directory in drive.Directories.Where(d => d.IsSelected != false))
                {
                    var hasSet = new HashSet<String>(imsr.Get(new IImageMetadataFilter() { Email = this.model.Email, DirectoryName = directory.Name }).Select(i => i.OriginalFileName));
                    directory.BulkDeselect(hasSet);
                }
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Contains(pictures))
            {
                var imsr = new P1ctur3.Storage.DataService.ImageMetadataDataServiceStorage(new Uri(MainWindow.StorageServiceUri), null);
                //var pics = imsr.Get(new IImageMetadataFilter() { Email = this.model.Email, DirectoryName = "E:\\NEW PICTURES\\iPhone pictures April-July 2011" });
                var pics = imsr.Get(new IImageMetadataFilter() { Email = this.model.Email });
                this.model.Pictures = pics.Select(p =>
                    new Tuple<Uri, Uri>(new Uri(p.StorageInfo.GetUrl("s")), new Uri(p.StorageInfo.GetUrl("q")))
                    );
            }
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            var image = sender as Image;
            if (image != null)
            {
                image.Width = 100;
                image.Height = 100;
                image.Margin = new Thickness(-80);
                image.BringToFront();
            }
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            var image = sender as Image;
            if (image != null)
            {
                image.Width = 20;
                image.Height = 20;
                image.Margin = new Thickness(0);
            }
        }


    }

    public static class FrameworkElementExt
    {
        public static void BringToFront(this FrameworkElement element)
        {
            if (element == null) return;

            Panel parent = element.Parent as Panel;
            if (parent == null) return;

            var maxZ = parent.Children.OfType<UIElement>()
              .Where(x => x != element)
              .Select(x => Panel.GetZIndex(x))
              .Max();
            Panel.SetZIndex(element, maxZ + 1);
        }
    }

}
