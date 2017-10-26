using P1ctur3.Exif;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telekad.Ux;

namespace P1ctur3.Standalone.Models
{
    public class DrivePhotoInfo : Selectable
    {
        public static IEnumerable<DrivePhotoInfo> GetDrives(ExifProcessor exifProcessor, ICallBackNotify parentToBeNotified)
        {
            //temporary disable the F drive due to its large content
            DriveInfo[] drives = DriveInfo.GetDrives();//.Where(d => !(d.Name.Contains("F"))).Where(d => !(d.Name.Contains("E"))).ToArray();            
            return drives.Select(d => new DrivePhotoInfo(exifProcessor, parentToBeNotified) { DriveLetter = d.Name, IsReady = d.IsReady, DriveType = d.DriveType, DriveFormat = (d.IsReady) ? d.DriveFormat : "Not ready" }).ToList();
        }

        ExifProcessor exifProcessor;
        public DrivePhotoInfo(ExifProcessor exifProcessor, ICallBackNotify parentToBeNotified)
        {           
            this.exifProcessor = exifProcessor;
            this.IsSelected = false;
            this.parentToBeNotified = parentToBeNotified;
        }

        void Directories_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("PhotoDirectoriesCount");
            RaisePropertyChanged("PhotoCount");
            RaisePropertyChanged("TotalSize");
            RaisePropertyChanged("TotalSizeFormatted");
        }

        private ICallBackNotify parentToBeNotified;

        //we need to override this since the parent is not selectable

        public override void Notify(object sender)
        {
            base.Notify(sender);
            if (this.parentToBeNotified != null)
                this.parentToBeNotified.Notify(this);
        }

        public override IEnumerable<Selectable> SelectableChildren
        {
            get
            { return this.Directories; }
        }


        public bool IsReady
        {
            set { Set(() => IsReady, value); }
            get { return Get(() => IsReady); }
        }

        public string DriveLetter
        {
            set { Set(() => DriveLetter, value); }
            get { return Get(() => DriveLetter); }
        }

        public DriveType DriveType
        {
            set { Set(() => DriveType, value); }
            get { return Get(() => DriveType); }
        }

        public string DriveFormat
        {
            set { Set(() => DriveFormat, value); }
            get { return Get(() => DriveFormat); }
        }

        public ObservableCollection<DirectoryPhotoInfo> Directories
        {
            private set;
            get;
        }

        public int PhotoDirectoriesCount
        {
            get { return this.Directories == null ? 0 : this.Directories.Count; }
        }

        public int PhotoCount
        {
            get { return this.Directories == null ? 0 : this.Directories.Sum(d => d.PhotoCount); }
        }

        public long TotalSelectedSize
        {
            get { return this.Directories == null ? 0 : this.Directories.Sum(d => d.TotalSelectedSize); }
        }

        public string TotalSelectedSizeFormatted
        {
            get { return Telekad.Utils.FormatStringFunctions.FormatBytes(this.TotalSelectedSize); }
        }


        public long TotalSize
        {
            get { return this.Directories == null ? 0 : this.Directories.Sum(d => d.TotalSize); }
        }

        public string TotalSizeFormatted
        {
            get { return Telekad.Utils.FormatStringFunctions.FormatBytes(this.TotalSize); }
        }

        public Action WhenScanDone { set; get; }

        public async void Scan(CancellationToken cancelationToken, IProgress<DirectoryPhotoInfo> progress)
        {
            Console.WriteLine("Start scanning {0}", this.DriveLetter);
            try
            {
                var task = Task<List<DirectoryPhotoInfo>>.Run(() =>
                 {
                     int folderCount = 0;
                     var searchResult = new List<DirectoryPhotoInfo>();
                     this.ScanFolders(this.DriveLetter, ref folderCount, searchResult, cancelationToken, progress);
                     Console.WriteLine("FolderCount " + folderCount);
                     return searchResult;
                 });

                var result = await task;
                progress.Report(null);
                var selectedState = this.IsSelected;
                
                this.Directories = new ObservableCollection<DirectoryPhotoInfo>(result);                
                this.Directories.CollectionChanged += Directories_CollectionChanged;
                this.RaisePropertyChanged("Directories");
                this.Notify(this);
            }
            catch 
            {             
                //burry
            }
            finally
            {
                if (this.WhenScanDone != null)
                    this.WhenScanDone.Invoke();
            }
        }

        string[] excludedFolders = new string[] { @"C:\Windows", @"C:\Program Files", @"C:\Program Files (x86)", @"C:\$Recycle.Bin" };
        string[] excludedNames = new string[] { @"\Temp", @"\Cache", @"\temp", @"\Resource",@"\MKS" };
        private void ScanFolders(string path, ref int folderCount, List<DirectoryPhotoInfo> seachResult, CancellationToken cancelationToken, IProgress<DirectoryPhotoInfo> progress)
        {
            if (excludedFolders.Any(exf => exf == path))
                return;
            if (excludedNames.Any(exf => path.Contains(exf)))
                return;
            if (cancelationToken != null && cancelationToken.IsCancellationRequested)
                return;
            try
            {

                if ((File.GetAttributes(path) & FileAttributes.ReparsePoint)
                    != FileAttributes.ReparsePoint)
                {                  
                    var folders = Directory.GetDirectories(path);
                    foreach (string folder in Directory.GetDirectories(path))
                    {
                        //  Console.WriteLine(System.IO.Path.GetFileName(folder));
                        if (progress != null)
                        {
                            progress.Report(new DirectoryPhotoInfo(this) { Name = folder, DeniedAccess = false });
                        }

                        try
                        {
                            string[] files = Directory.GetFiles(folder, "*.jpg", SearchOption.TopDirectoryOnly);
                            if (files.Count() > 0)
                            {
                                //var properties = this.exifProcessor.Extract(files[0]);

                                //todo - review this code for old scanned pictures where potencially no "make" field is in the place
                                var atLeastOneFileContainsTheExifMake = files.Count() > 5 || files.Select(s => this.exifProcessor.Extract(s)).Any(p => p != null && p.ContainsKey(ExifLibrary.ExifTag.Make));

                                if (atLeastOneFileContainsTheExifMake)
                                {
                                    var directory = new DirectoryPhotoInfo(this) { Name = folder };

                                    foreach (var photo in files)
                                    {
                                        directory.Photos.Add(new PhotoInfo(directory) { Name = photo, Size = new FileInfo(photo).Length});
                                    }

                                    directory.IsSelected = true;

                                    seachResult.Add(directory);
                                    if (progress != null)
                                    {
                                        progress.Report(new DirectoryPhotoInfo(this) { Name = folder });
                                    }
                                }
                            }

                            folderCount++;
                            ScanFolders(folder, ref folderCount, seachResult, cancelationToken, progress);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            progress.Report(new DirectoryPhotoInfo(this) { Name = folder, DeniedAccess = true });
                            Thread.Sleep(10);
                        }
                    }
                }
            }
            catch (System.IO.IOException)
            {
               //burry it         
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.GetType() + " " + e.Message);                               
                throw e;
            }
        }

        public override Selectable SelectableParent
        {
            get { return null; }
        }
    }
}
