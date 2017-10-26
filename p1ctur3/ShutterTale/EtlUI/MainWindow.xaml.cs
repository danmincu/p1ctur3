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

namespace EtlUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            textBoxFolderName.Text = @"\\Xeon\y\Important pictures\iPhone pictures April-July 2011";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var di = new EtlLibrary.DirectoryImport(textBoxFolderName.Text);
            di.Import();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
