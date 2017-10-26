using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telekad.Ux;

namespace P1ctur3.Standalone.Models
{
    public class MainModel : NotifyObject, IProgress<DirectoryPhotoInfo>, IProgress<UploadDirectoryPhotoInfo>, ICallBackNotify
    {
        public MainModel()
        {
            this.Drives = new ObservableCollection<DrivePhotoInfo>();
        }

        public IEnumerable<Tuple<Uri, Uri>> Pictures 
        {
            set { Set(() => Pictures, value); }
            get { return Get(() => Pictures); }
        }


        public string Email
        {
            set
            {
                if (!System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("Email") || System.Configuration.ConfigurationManager.AppSettings["Email"] != value)
                {

                    var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                    config.AppSettings.Settings.Remove("Email");
                    config.AppSettings.Settings.Add("Email", value);
                    config.Save(System.Configuration.ConfigurationSaveMode.Modified);
                    System.Configuration.ConfigurationManager.RefreshSection("appSettings");
                }
                Set(() => Email, value);
            }
            get
            {
                return Get(() => Email);
            }
        }

        public string CurrentFolder
        {
            set
            {
                Set(() => CurrentFolder, value);
            }
            get { return Get(() => CurrentFolder); }
        }
        public bool DeniedAccess
        {
            set { Set(() => DeniedAccess, value); }
            get { return Get(() => DeniedAccess); }
        }

        public ObservableCollection<DrivePhotoInfo> Drives { private set; get; }

        public long TotalSelectedSize
        {
            get
            {
                return this.Drives.Sum(a => a.TotalSelectedSize);
            }
        }

        public string TotalSelectedSizeFormatted
        {
            get
            {
                return Telekad.Utils.FormatStringFunctions.FormatBytes(this.TotalSelectedSize);
            }
        }


        public void Notify(object sender)
        {
            this.RaisePropertyChanged("TotalSelectedSizeFormatted");
        }


        public void Report(DirectoryPhotoInfo value)
        {
            if (value == null)
            {
                this.CurrentFolder = string.Empty;
                this.DeniedAccess = false;
                return;
            }

            this.CurrentFolder = value.Name;
            this.DeniedAccess = value.DeniedAccess;
        }

        public void Report(UploadDirectoryPhotoInfo value)
        {
            if (value == null)
            {
                this.CurrentFolder = string.Empty;
                this.DeniedAccess = false;
                return;
            }

            this.CurrentFolder = value.CurrentFileCount + "/" + value.TotalFileCount + " << " + value.CurrentFile;
            this.DeniedAccess = false;
        }


    }
}
