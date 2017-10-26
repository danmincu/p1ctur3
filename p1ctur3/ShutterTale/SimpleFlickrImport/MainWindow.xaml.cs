using ShutterTale.Data.WebServiceClient;
using ShutterTale.Entities;
using System;
using System.Collections.Generic;
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

namespace SimpleFlickrImport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WsMediaProvider provider = new WsMediaProvider("http://localhost/ShutterTale.NetTiers/ShutterTaleServices.asmx");
            var media = new Media()
            {
                Id = Guid.NewGuid(),
                CaptureDateTime = DateTime.UtcNow,
                FileDateTime = DateTime.UtcNow,
                ImportDateTime = DateTime.UtcNow,
                Pixelx = 1,
                Pixely = 1,
                Dpi = 1,
                ContentType = @"image/jpeg",
                Make = "",
                Model = "",
                SoftwareVersion = "",
                Origin = "",
                Size = 0

            };

            //provider.Insert(media);
            provider.Save(media);
           


        }
    }
}
