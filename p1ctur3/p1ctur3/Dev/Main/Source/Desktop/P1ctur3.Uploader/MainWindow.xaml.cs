using P1ctur3.Exif;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
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
using Telekad.Ux;

namespace P1ctur3.Uploader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ExifProcessor exifProcessor = new ExifProcessor();
        public MainWindow()
        {
            InitializeComponent();
            this.Drives = new ObservableCollection<DrivePhotoInfo>();// { new DrivePhotoInfo(){DriveLetter = "C", PhotoDirectoriesCount = 100, PhotoCount=123141}};
            this.DataContext = this;
        }

        public ObservableCollection<DrivePhotoInfo> Drives { set; get; }

        private long totalCount = 0;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fileName = @"c:\temp\photo.jpg";
            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                var memoryStream = new MemoryStream();
                fileStream.CopyTo(memoryStream);
                //this.UploadDirectly(memoryStream, fileName);
                //return;
                try
                {
                    var httpWr = (new Telekad.Utils.HttpUploader()).UploadDirectly(memoryStream, fileName).Result as HttpWebResponse;

                    if (httpWr.StatusCode == HttpStatusCode.Created)
                    {
                        MessageBox.Show("Upload completed.");

                    }
                    else
                    {
                        MessageBox.Show("An error occured during uploading. Please try again later.");
                    }
                }
                catch
                {
                    MessageBox.Show("An error occured during uploading. Please try again later.");
                }

            }
        }

        private string UploadPicture(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                var memoryStream = new MemoryStream();
                fileStream.CopyTo(memoryStream);
                try
                {
                    var httpWr = (new Telekad.Utils.HttpUploader()).UploadDirectly(memoryStream, fileName).Result as HttpWebResponse;

                    if (httpWr.StatusCode == HttpStatusCode.Created)
                    {
                        return "Upload completed.";
                    }
                    else
                    {
                        return "An error occured during uploading. Please try again later.";
                    }
                }
                catch
                {
                    return "An error occured during uploading. Please try again later.";
                }
            }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();



            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"photofolders", false))
            {

                foreach (DriveInfo drive in drives)
                {
                    double fspc = 0.0;
                    double tspc = 0.0;
                    double percent = 0.0;
                    if (drive.IsReady)
                    {
                        fspc = drive.TotalFreeSpace;
                        tspc = drive.TotalSize;
                        percent = (fspc / tspc) * 100;
                        float num = (float)percent;

                        Console.WriteLine("Drive:{0} With {1} % free", drive.Name, num);
                        Console.WriteLine("Space Reamining:{0}", drive.AvailableFreeSpace);
                        Console.WriteLine("Percent Free Space:{0}", percent);
                        Console.WriteLine("Space used:{0}", drive.TotalSize);
                        Console.WriteLine("Type: {0}", drive.DriveType);

                        this.Drives.Add(new DrivePhotoInfo() { DriveLetter = drive.Name + "[" + drive.DriveType + ":" + drive.DriveFormat + "]" });

                        if (drive.Name.Contains("C"))
                        {
                            ScanDrive(drive.Name, this.Drives.Last(), file);
                        }


                    }
                    else
                        this.Drives.Add(new DrivePhotoInfo() { DriveLetter = drive.Name + "Not ready" });
                }
                file.WriteLine("TOTAL SIZE OF PICTURES: " + totalCount.ToString());
            }
        }


        public void ScanDrive(string driveName, DrivePhotoInfo info, StreamWriter file)
        {
            ShowAllFoldersUnder(driveName, 0, info, file);
        }


        string[] excludedFolders = new string[] { @"C:\Windows", @"C:\Program Files", @"C:\Program Files (x86)",@"C:\$Recycle.Bin" };
        private void ShowAllFoldersUnder(string path, int indent, DrivePhotoInfo info, StreamWriter file)
        {
            try
            {
                if ((File.GetAttributes(path) & FileAttributes.ReparsePoint)
                    != FileAttributes.ReparsePoint)
                {

                    var folders = Directory.GetDirectories(path);
                    foreach (string folder in Directory.GetDirectories(path))
                    {
                        Console.WriteLine(
                            "{0}{1}", new string(' ', indent), System.IO.Path.GetFileName(folder));

                        try
                        {
                            string[] files = Directory.GetFiles(folder, "*.jpg", SearchOption.TopDirectoryOnly);
                            if (files.Count() > 0)
                            {
                                var properties = this.exifProcessor.Extract(files[0]);

                                if (properties.ContainsKey(ExifLibrary.ExifTag.Make))
                                {
                                    info.PhotoCount += files.Count();
                                    info.PhotoDirectoriesCount += 1;
                                    var directorySize = files.Sum(a => new FileInfo(a).Length);
                                    info.TotalSize = directorySize;
                                    file.WriteLine(folder + "," + directorySize + ",[" + files.Count() + "x files]");
                                    totalCount += directorySize;

                                    foreach (var fileName in files)
                                    {
                                        //string taskResult =  await Task.Run(() => UploadPicture(fileName));
                                        //this.Title = taskResult;
                                        this.Title = UploadPicture(fileName);
                                        DoEvents();
                                    }

                                }
                            }
                        }
                        catch(Exception e)
                        {
                            this.Title = e.Message;
                            DoEvents();
                        }

                        if (!excludedFolders.Any(exf => exf == folder))
                            ShowAllFoldersUnder(folder, indent + 2, info, file);

                    }
                }
            }
            catch (UnauthorizedAccessException) { }
        }

        public static void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                                                  new Action(delegate { }));
        }

        //private void UploadDirectly(Stream stream, string fullFilePath)
        //{
        //    var id = Telekad.Utils.Base64.Base64Encode(fullFilePath);
        //    string serviceUri = string.Format(CultureInfo.InvariantCulture, "http://localhost/P1ctur3.Web/api/photo/{0}/", id);
        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(serviceUri);
        //    request.Method = "POST";
        //    request.BeginGetRequestStream(result =>
        //    {
        //        Stream requestStream = request.EndGetRequestStream(result);
        //        stream.Seek(0, 0);
        //        stream.CopyTo(requestStream);
        //        requestStream.Close();
        //        request.BeginGetResponse(result2 =>
        //        {
        //            try
        //            {
        //                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result2);
        //                if (response.StatusCode == HttpStatusCode.Created)
        //                {
        //                    MessageBox.Show("Upload completed.");

        //                }
        //                else
        //                {
        //                    MessageBox.Show("An error occured during uploading. Please try again later.");

        //                }
        //            }
        //            catch
        //            {
        //                MessageBox.Show("An error occured during uploading. Please try again later.");

        //            }
        //            stream.Close();
        //        }, null);
        //    }, null);
        //}

    }

    public class DrivePhotoInfo : NotifyObject
    {
        public string DriveLetter
        {
            set { Set(() => DriveLetter, value); }
            get { return Get(() => DriveLetter); }
        }

        public int PhotoDirectoriesCount
        {
            set { Set(() => PhotoDirectoriesCount, value); }
            get { return Get(() => PhotoDirectoriesCount); }
        }

        public int PhotoCount
        {
            set { Set(() => PhotoCount, value); }
            get { return Get(() => PhotoCount); }
        }

        public long TotalSize
        {
            set { Set(() => TotalSize, value); }
            get { return Get(() => TotalSize); }
        }
    }

}
