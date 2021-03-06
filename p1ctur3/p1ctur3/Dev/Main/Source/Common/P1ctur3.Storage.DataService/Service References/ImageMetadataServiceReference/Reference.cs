//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Original file name:
// Generation date: 2014-02-01 1:54:18 AM
namespace P1ctur3.Storage.DataService.ImageMetadataServiceReference
{
    
    /// <summary>
    /// There are no comments for ImageMetadataContext in the schema.
    /// </summary>
    public partial class ImageMetadataContext : global::System.Data.Services.Client.DataServiceContext
    {
        /// <summary>
        /// Initialize a new ImageMetadataContext object.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public ImageMetadataContext(global::System.Uri serviceRoot) : 
                base(serviceRoot, global::System.Data.Services.Common.DataServiceProtocolVersion.V3)
        {
            this.ResolveName = new global::System.Func<global::System.Type, string>(this.ResolveNameFromType);
            this.ResolveType = new global::System.Func<string, global::System.Type>(this.ResolveTypeFromName);
            this.OnContextCreated();
            this.Format.LoadServiceModel = GeneratedEdmModel.GetInstance;
        }
        partial void OnContextCreated();
        /// <summary>
        /// Since the namespace configured for this service reference
        /// in Visual Studio is different from the one indicated in the
        /// server schema, use type-mappers to map between the two.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected global::System.Type ResolveTypeFromName(string typeName)
        {
            global::System.Type resolvedType = this.DefaultResolveType(typeName, "P1ctur3.Storage.Sql", "P1ctur3.Storage.DataService.ImageMetadataServiceReference");
            if ((resolvedType != null))
            {
                return resolvedType;
            }
            return null;
        }
        /// <summary>
        /// Since the namespace configured for this service reference
        /// in Visual Studio is different from the one indicated in the
        /// server schema, use type-mappers to map between the two.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected string ResolveNameFromType(global::System.Type clientType)
        {
            if (clientType.Namespace.Equals("P1ctur3.Storage.DataService.ImageMetadataServiceReference", global::System.StringComparison.Ordinal))
            {
                return string.Concat("P1ctur3.Storage.Sql.", clientType.Name);
            }
            return null;
        }
        /// <summary>
        /// There are no comments for Images in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<ImageMetadata> Images
        {
            get
            {
                if ((this._Images == null))
                {
                    this._Images = base.CreateQuery<ImageMetadata>("Images");
                }
                return this._Images;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<ImageMetadata> _Images;
        /// <summary>
        /// There are no comments for RemoteInfos in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<RemoteInfo> RemoteInfos
        {
            get
            {
                if ((this._RemoteInfos == null))
                {
                    this._RemoteInfos = base.CreateQuery<RemoteInfo>("RemoteInfos");
                }
                return this._RemoteInfos;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<RemoteInfo> _RemoteInfos;
        /// <summary>
        /// There are no comments for Images in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToImages(ImageMetadata imageMetadata)
        {
            base.AddObject("Images", imageMetadata);
        }
        /// <summary>
        /// There are no comments for RemoteInfos in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToRemoteInfos(RemoteInfo remoteInfo)
        {
            base.AddObject("RemoteInfos", remoteInfo);
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private abstract class GeneratedEdmModel
        {
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private static global::Microsoft.Data.Edm.IEdmModel ParsedModel = LoadModelFromString();
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private const string ModelPart0 = "<edmx:Edmx Version=\"1.0\" xmlns:edmx=\"http://schemas.microsoft.com/ado/2007/06/edm" +
                "x\"><edmx:DataServices m:DataServiceVersion=\"1.0\" m:MaxDataServiceVersion=\"3.0\" x" +
                "mlns:m=\"http://schemas.microsoft.com/ado/2007/08/dataservices/metadata\"><Schema " +
                "Namespace=\"P1ctur3.Storage.Sql\" xmlns=\"http://schemas.microsoft.com/ado/2009/11/" +
                "edm\"><EntityType Name=\"ImageMetadata\"><Key><PropertyRef Name=\"Id\" /></Key><Prope" +
                "rty Name=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" /><Property Name=\"RemoteInfoId\" T" +
                "ype=\"Edm.Guid\" Nullable=\"false\" /><Property Name=\"CollectionId\" Type=\"Edm.Guid\" " +
                "/><Property Name=\"DateTime\" Type=\"Edm.DateTime\" /><Property Name=\"ImportDateTime" +
                "\" Type=\"Edm.DateTime\" Nullable=\"false\" /><Property Name=\"FileDateTime\" Type=\"Edm" +
                ".DateTime\" /><Property Name=\"OriginalFileName\" Type=\"Edm.String\" /><Property Nam" +
                "e=\"FullPath\" Type=\"Edm.String\" /><Property Name=\"TagsAsJson\" Type=\"Edm.String\" /" +
                "><Property Name=\"Latitude\" Type=\"Edm.Double\" /><Property Name=\"Longitude\" Type=\"" +
                "Edm.Double\" /><Property Name=\"Thumb\" Type=\"Edm.Binary\" /><Property Name=\"Storage" +
                "InfoAsJson\" Type=\"Edm.String\" /><NavigationProperty Name=\"RemoteInfo\" Relationsh" +
                "ip=\"P1ctur3.Storage.Sql.ImageMetadata_RemoteInfo_RemoteInfo_ImageMetadatas\" ToRo" +
                "le=\"RemoteInfo_ImageMetadatas\" FromRole=\"ImageMetadata_RemoteInfo\" /></EntityTyp" +
                "e><EntityType Name=\"RemoteInfo\"><Key><PropertyRef Name=\"RemoteInfoId\" /></Key><P" +
                "roperty Name=\"RemoteInfoId\" Type=\"Edm.Guid\" Nullable=\"false\" /><Property Name=\"E" +
                "mail\" Type=\"Edm.String\" /><Property Name=\"Type\" Type=\"Edm.String\" /><Property Na" +
                "me=\"Version\" Type=\"Edm.String\" /><Property Name=\"PropertiesAsJson\" Type=\"Edm.Str" +
                "ing\" /><NavigationProperty Name=\"ImageMetadatas\" Relationship=\"P1ctur3.Storage.S" +
                "ql.ImageMetadata_RemoteInfo_RemoteInfo_ImageMetadatas\" ToRole=\"ImageMetadata_Rem" +
                "oteInfo\" FromRole=\"RemoteInfo_ImageMetadatas\" /></EntityType><Association Name=\"" +
                "ImageMetadata_RemoteInfo_RemoteInfo_ImageMetadatas\"><End Type=\"P1ctur3.Storage.S" +
                "ql.RemoteInfo\" Role=\"RemoteInfo_ImageMetadatas\" Multiplicity=\"0..1\" /><End Type=" +
                "\"P1ctur3.Storage.Sql.ImageMetadata\" Role=\"ImageMetadata_RemoteInfo\" Multiplicity" +
                "=\"*\" /></Association><EntityContainer Name=\"ImageMetadataContext\" m:IsDefaultEnt" +
                "ityContainer=\"true\"><EntitySet Name=\"Images\" EntityType=\"P1ctur3.Storage.Sql.Ima" +
                "geMetadata\" /><EntitySet Name=\"RemoteInfos\" EntityType=\"P1ctur3.Storage.Sql.Remo" +
                "teInfo\" /><AssociationSet Name=\"RemoteInfo_ImageMetadatas\" Association=\"P1ctur3." +
                "Storage.Sql.ImageMetadata_RemoteInfo_RemoteInfo_ImageMetadatas\"><End Role=\"Image" +
                "Metadata_RemoteInfo\" EntitySet=\"Images\" /><End Role=\"RemoteInfo_ImageMetadatas\" " +
                "EntitySet=\"RemoteInfos\" /></AssociationSet></EntityContainer></Schema></edmx:Dat" +
                "aServices></edmx:Edmx>";
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private static string GetConcatenatedEdmxString()
            {
                return string.Concat(ModelPart0);
            }
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            public static global::Microsoft.Data.Edm.IEdmModel GetInstance()
            {
                return ParsedModel;
            }
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private static global::Microsoft.Data.Edm.IEdmModel LoadModelFromString()
            {
                string edmxToParse = GetConcatenatedEdmxString();
                global::System.Xml.XmlReader reader = CreateXmlReader(edmxToParse);
                try
                {
                    return global::Microsoft.Data.Edm.Csdl.EdmxReader.Parse(reader);
                }
                finally
                {
                    ((global::System.IDisposable)(reader)).Dispose();
                }
            }
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private static global::System.Xml.XmlReader CreateXmlReader(string edmxToParse)
            {
                return global::System.Xml.XmlReader.Create(new global::System.IO.StringReader(edmxToParse));
            }
        }
    }
    /// <summary>
    /// There are no comments for P1ctur3.Storage.Sql.ImageMetadata in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("Images")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Id")]
    public partial class ImageMetadata : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Create a new ImageMetadata object.
        /// </summary>
        /// <param name="ID">Initial value of Id.</param>
        /// <param name="remoteInfoId">Initial value of RemoteInfoId.</param>
        /// <param name="importDateTime">Initial value of ImportDateTime.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static ImageMetadata CreateImageMetadata(global::System.Guid ID, global::System.Guid remoteInfoId, global::System.DateTime importDateTime)
        {
            ImageMetadata imageMetadata = new ImageMetadata();
            imageMetadata.Id = ID;
            imageMetadata.RemoteInfoId = remoteInfoId;
            imageMetadata.ImportDateTime = importDateTime;
            return imageMetadata;
        }
        /// <summary>
        /// There are no comments for Property Id in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Guid Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();
        /// <summary>
        /// There are no comments for Property RemoteInfoId in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Guid RemoteInfoId
        {
            get
            {
                return this._RemoteInfoId;
            }
            set
            {
                this.OnRemoteInfoIdChanging(value);
                this._RemoteInfoId = value;
                this.OnRemoteInfoIdChanged();
                this.OnPropertyChanged("RemoteInfoId");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Guid _RemoteInfoId;
        partial void OnRemoteInfoIdChanging(global::System.Guid value);
        partial void OnRemoteInfoIdChanged();
        /// <summary>
        /// There are no comments for Property CollectionId in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<global::System.Guid> CollectionId
        {
            get
            {
                return this._CollectionId;
            }
            set
            {
                this.OnCollectionIdChanging(value);
                this._CollectionId = value;
                this.OnCollectionIdChanged();
                this.OnPropertyChanged("CollectionId");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<global::System.Guid> _CollectionId;
        partial void OnCollectionIdChanging(global::System.Nullable<global::System.Guid> value);
        partial void OnCollectionIdChanged();
        /// <summary>
        /// There are no comments for Property DateTime in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<global::System.DateTime> DateTime
        {
            get
            {
                return this._DateTime;
            }
            set
            {
                this.OnDateTimeChanging(value);
                this._DateTime = value;
                this.OnDateTimeChanged();
                this.OnPropertyChanged("DateTime");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<global::System.DateTime> _DateTime;
        partial void OnDateTimeChanging(global::System.Nullable<global::System.DateTime> value);
        partial void OnDateTimeChanged();
        /// <summary>
        /// There are no comments for Property ImportDateTime in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.DateTime ImportDateTime
        {
            get
            {
                return this._ImportDateTime;
            }
            set
            {
                this.OnImportDateTimeChanging(value);
                this._ImportDateTime = value;
                this.OnImportDateTimeChanged();
                this.OnPropertyChanged("ImportDateTime");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.DateTime _ImportDateTime;
        partial void OnImportDateTimeChanging(global::System.DateTime value);
        partial void OnImportDateTimeChanged();
        /// <summary>
        /// There are no comments for Property FileDateTime in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<global::System.DateTime> FileDateTime
        {
            get
            {
                return this._FileDateTime;
            }
            set
            {
                this.OnFileDateTimeChanging(value);
                this._FileDateTime = value;
                this.OnFileDateTimeChanged();
                this.OnPropertyChanged("FileDateTime");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<global::System.DateTime> _FileDateTime;
        partial void OnFileDateTimeChanging(global::System.Nullable<global::System.DateTime> value);
        partial void OnFileDateTimeChanged();
        /// <summary>
        /// There are no comments for Property OriginalFileName in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string OriginalFileName
        {
            get
            {
                return this._OriginalFileName;
            }
            set
            {
                this.OnOriginalFileNameChanging(value);
                this._OriginalFileName = value;
                this.OnOriginalFileNameChanged();
                this.OnPropertyChanged("OriginalFileName");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _OriginalFileName;
        partial void OnOriginalFileNameChanging(string value);
        partial void OnOriginalFileNameChanged();
        /// <summary>
        /// There are no comments for Property FullPath in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string FullPath
        {
            get
            {
                return this._FullPath;
            }
            set
            {
                this.OnFullPathChanging(value);
                this._FullPath = value;
                this.OnFullPathChanged();
                this.OnPropertyChanged("FullPath");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _FullPath;
        partial void OnFullPathChanging(string value);
        partial void OnFullPathChanged();
        /// <summary>
        /// There are no comments for Property TagsAsJson in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string TagsAsJson
        {
            get
            {
                return this._TagsAsJson;
            }
            set
            {
                this.OnTagsAsJsonChanging(value);
                this._TagsAsJson = value;
                this.OnTagsAsJsonChanged();
                this.OnPropertyChanged("TagsAsJson");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _TagsAsJson;
        partial void OnTagsAsJsonChanging(string value);
        partial void OnTagsAsJsonChanged();
        /// <summary>
        /// There are no comments for Property Latitude in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<double> Latitude
        {
            get
            {
                return this._Latitude;
            }
            set
            {
                this.OnLatitudeChanging(value);
                this._Latitude = value;
                this.OnLatitudeChanged();
                this.OnPropertyChanged("Latitude");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<double> _Latitude;
        partial void OnLatitudeChanging(global::System.Nullable<double> value);
        partial void OnLatitudeChanged();
        /// <summary>
        /// There are no comments for Property Longitude in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<double> Longitude
        {
            get
            {
                return this._Longitude;
            }
            set
            {
                this.OnLongitudeChanging(value);
                this._Longitude = value;
                this.OnLongitudeChanged();
                this.OnPropertyChanged("Longitude");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<double> _Longitude;
        partial void OnLongitudeChanging(global::System.Nullable<double> value);
        partial void OnLongitudeChanged();
        /// <summary>
        /// There are no comments for Property Thumb in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public byte[] Thumb
        {
            get
            {
                if ((this._Thumb != null))
                {
                    return ((byte[])(this._Thumb.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnThumbChanging(value);
                this._Thumb = value;
                this.OnThumbChanged();
                this.OnPropertyChanged("Thumb");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private byte[] _Thumb;
        partial void OnThumbChanging(byte[] value);
        partial void OnThumbChanged();
        /// <summary>
        /// There are no comments for Property StorageInfoAsJson in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string StorageInfoAsJson
        {
            get
            {
                return this._StorageInfoAsJson;
            }
            set
            {
                this.OnStorageInfoAsJsonChanging(value);
                this._StorageInfoAsJson = value;
                this.OnStorageInfoAsJsonChanged();
                this.OnPropertyChanged("StorageInfoAsJson");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _StorageInfoAsJson;
        partial void OnStorageInfoAsJsonChanging(string value);
        partial void OnStorageInfoAsJsonChanged();
        /// <summary>
        /// There are no comments for RemoteInfo in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public RemoteInfo RemoteInfo
        {
            get
            {
                return this._RemoteInfo;
            }
            set
            {
                this._RemoteInfo = value;
                this.OnPropertyChanged("RemoteInfo");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private RemoteInfo _RemoteInfo;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
    /// <summary>
    /// There are no comments for P1ctur3.Storage.Sql.RemoteInfo in the schema.
    /// </summary>
    /// <KeyProperties>
    /// RemoteInfoId
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("RemoteInfos")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("RemoteInfoId")]
    public partial class RemoteInfo : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Create a new RemoteInfo object.
        /// </summary>
        /// <param name="remoteInfoId">Initial value of RemoteInfoId.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static RemoteInfo CreateRemoteInfo(global::System.Guid remoteInfoId)
        {
            RemoteInfo remoteInfo = new RemoteInfo();
            remoteInfo.RemoteInfoId = remoteInfoId;
            return remoteInfo;
        }
        /// <summary>
        /// There are no comments for Property RemoteInfoId in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Guid RemoteInfoId
        {
            get
            {
                return this._RemoteInfoId;
            }
            set
            {
                this.OnRemoteInfoIdChanging(value);
                this._RemoteInfoId = value;
                this.OnRemoteInfoIdChanged();
                this.OnPropertyChanged("RemoteInfoId");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Guid _RemoteInfoId;
        partial void OnRemoteInfoIdChanging(global::System.Guid value);
        partial void OnRemoteInfoIdChanged();
        /// <summary>
        /// There are no comments for Property Email in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Email
        {
            get
            {
                return this._Email;
            }
            set
            {
                this.OnEmailChanging(value);
                this._Email = value;
                this.OnEmailChanged();
                this.OnPropertyChanged("Email");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Email;
        partial void OnEmailChanging(string value);
        partial void OnEmailChanged();
        /// <summary>
        /// There are no comments for Property Type in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this.OnTypeChanging(value);
                this._Type = value;
                this.OnTypeChanged();
                this.OnPropertyChanged("Type");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Type;
        partial void OnTypeChanging(string value);
        partial void OnTypeChanged();
        /// <summary>
        /// There are no comments for Property Version in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Version
        {
            get
            {
                return this._Version;
            }
            set
            {
                this.OnVersionChanging(value);
                this._Version = value;
                this.OnVersionChanged();
                this.OnPropertyChanged("Version");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Version;
        partial void OnVersionChanging(string value);
        partial void OnVersionChanged();
        /// <summary>
        /// There are no comments for Property PropertiesAsJson in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string PropertiesAsJson
        {
            get
            {
                return this._PropertiesAsJson;
            }
            set
            {
                this.OnPropertiesAsJsonChanging(value);
                this._PropertiesAsJson = value;
                this.OnPropertiesAsJsonChanged();
                this.OnPropertyChanged("PropertiesAsJson");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _PropertiesAsJson;
        partial void OnPropertiesAsJsonChanging(string value);
        partial void OnPropertiesAsJsonChanged();
        /// <summary>
        /// There are no comments for ImageMetadatas in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceCollection<ImageMetadata> ImageMetadatas
        {
            get
            {
                return this._ImageMetadatas;
            }
            set
            {
                this._ImageMetadatas = value;
                this.OnPropertyChanged("ImageMetadatas");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceCollection<ImageMetadata> _ImageMetadatas = new global::System.Data.Services.Client.DataServiceCollection<ImageMetadata>(null, global::System.Data.Services.Client.TrackingMode.None);
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
}
