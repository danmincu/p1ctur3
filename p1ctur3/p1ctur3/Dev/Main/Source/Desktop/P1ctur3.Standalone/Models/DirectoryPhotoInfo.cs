using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telekad.Ux;

namespace P1ctur3.Standalone.Models
{
    public class DirectoryPhotoInfo : Selectable, IProgress<UploadDirectoryPhotoInfo>
    {
        DrivePhotoInfo parent;
        public DirectoryPhotoInfo(DrivePhotoInfo parent)
        {
            this.parent = parent;
            this.Photos = new ObservableCollection<PhotoInfo>();
            this.Photos.CollectionChanged += Photos_CollectionChanged;
            this.SilentSet(true);
        }

        void Photos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("PhotoCount");
            RaisePropertyChanged("TotalSize");
            RaisePropertyChanged("TotalSizeFormatted");
        }
        
        public void BulkDeselect(HashSet<string> hash)
        {
            if (hash.Any())
                foreach (var photo in this.Photos)
                {
                    if (hash.Contains(photo.Name))
                    {
                        photo.SilentSet(false);
                    }
                }
            this.EvaluateSelection();
        }

        public ObservableCollection<PhotoInfo> Photos
        {
            private set;
            get;
        }

        public string Name
        {
            set { Set(() => Name, value); }
            get { return Get(() => Name); }
        }

        public bool DeniedAccess
        {
            set { Set(() => DeniedAccess, value); }
            get { return Get(() => DeniedAccess); }
        }


        public int PhotoCount
        {
            get { return this.Photos.Count; }
        }

        public int SelectedPhotoCount
        {
            get { return this.Photos.Where(p => p.IsSelected == true).Count(); }
        }

        public long TotalSelectedSize
        {
            get { return this.Photos.Where(p => p.IsSelected == true).Sum(a => a.Size); }
        }

        public string TotalSelectedSizeFormatted
        {
            get { return Telekad.Utils.FormatStringFunctions.FormatBytes(this.TotalSelectedSize); }
        }

        public long TotalSize
        {
            get { return this.Photos.Sum(a => a.Size); }
        }

        public string TotalSizeFormatted
        {
            get { return Telekad.Utils.FormatStringFunctions.FormatBytes(this.TotalSize); }
        }

        public override Selectable SelectableParent
        {
            get { return this.parent; }
        }

        public int PercentageUpload
        {
            set { Set(() => PercentageUpload, value); }
            get { return Get(() => PercentageUpload); }
        }

        IProgress<UploadDirectoryPhotoInfo> parentProgress;
        public async void Upload(IProgress<UploadDirectoryPhotoInfo> progress)
        {
            //this.parentProgress = progress;
            //var task = Task.Run(() => DoUpload(this));
            //await task;    
        }

        private void DoUpload(IProgress<UploadDirectoryPhotoInfo> progress)
        {
            //int i = 0;
            //foreach (var item in this.Photos.Where(p => p.IsSelected == true))
            //{
            //    item.Upload();

            //    i++;
            //    if (progress != null)
            //        progress.Report(new UploadDirectoryPhotoInfo() { CurrentFile = item.Name, CurrentFileCount = i, TotalFileCount = this.SelectedPhotoCount });
            //}
        }

        public async void DirectUpload(IProgress<UploadDirectoryPhotoInfo> progress)
        {
            this.parentProgress = progress;
            var task = Task.Run(() => DoDirectUpload(this));
            await task;
            this.IsSelected = false;
        }

        public Task DirectUploadTask(IProgress<UploadDirectoryPhotoInfo> progress)
        {
            this.parentProgress = progress;
            return Task.Run(() => DoDirectUpload(this));
        }


        private void DoDirectUpload(IProgress<UploadDirectoryPhotoInfo> progress)
        {
            int i = 0;
            foreach (var item in this.Photos.Where(p => p.IsSelected == true))
            {
                Exception exception = null;
                try
                {
                    item.DirectUpload();
                }
                catch (Exception e)
                {
                    //bury whatever it is                                           
                    Logger.Write(Telekad.Utils.ExceptionUtils.CreateExceptionString(e));
                }

                i++;
                if (progress != null)
                    progress.Report(new UploadDirectoryPhotoInfo() { CurrentFile = item.Name, CurrentFileCount = i, TotalFileCount = this.SelectedPhotoCount, LastException = exception });
            }
        }

        public void Report(UploadDirectoryPhotoInfo value)
        {
            this.PercentageUpload = (int)Math.Round((double)value.CurrentFileCount * 100 / value.TotalFileCount);
            if (this.parentProgress != null)
                this.parentProgress.Report(value);
        }

        public override IEnumerable<Selectable> SelectableChildren
        {
            get { return this.Photos; }
        }

    }
}
