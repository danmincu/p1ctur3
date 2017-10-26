using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telekad.Ux;

namespace MediaImport
{
    public class ViewModel : NotifyObject
    {

        public ViewModel()
        {
            this.StartImportCommand = new DelegateCommand(
                () =>
                {
                    
                }
                ,
                () =>
                { return Directory.Exists(this.ImportFolder); }
                );

            this.ChooseFolderCommand = new DelegateCommand(
                () =>
                {
                    this.ImportFolder = "tyest";
                });
            



        
        }
               
        public string ImportFolder
        {
            get { return Get(() => ImportFolder); }
            set { Set(() => ImportFolder, value); }
        }

        public bool UseFlickrStorage
        {
            get { return Get(() => UseFlickrStorage); }
            set { Set(() => UseFlickrStorage, value); }
        }

        public string FlickrKey
        {
            get { return Get(() => FlickrKey); }
            set { Set(() => FlickrKey, value); }
        }

        public string FlickrSecret
        {
            get { return Get(() => FlickrSecret); }
            set { Set(() => FlickrSecret, value); }
        }

        public string FlickrVerification
        {
            get { return Get(() => FlickrVerification); }
            set { Set(() => FlickrVerification, value); }
        }

        public ICommand StartImportCommand { get; private set; }
        public ICommand ChooseFolderCommand { get; private set; }
        


    }
}
