using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Telekad.Mapping;
using Telekad.Utils;

namespace P1ctur3.Types
{
    [DataContract]
    public class ImageMetadata
    {
        [DataMember]
        [Key]
        public Guid Id { set; get; }

        //     [DataMember]
        //     [ForeignKey("RemoteInfoId")]        
        public Guid RemoteInfoId { get; set; }
        public virtual RemoteInfo RemoteInfo { set; get; }

        [DataMember]
        public Guid? CollectionId { set; get; }

        [DataMember]
        public DateTime? DateTime { set; get; }

        [DataMember]
        public DateTime ImportDateTime { set; get; }

        [DataMember]
        public DateTime? FileDateTime { set; get; }

        [DataMember]
        public string OriginalFileName { set; get; }


        [DataMember]
        public string FullPath { set; get; }

        public DateTime BestDate
        {
            get
            {
                return DateTime ?? (FileDateTime ?? ImportDateTime);
            }
        }

        [DataMember, NotMapped]
        public Dictionary<string, string> Tags { set; get; }


        public string TagsAsJson
        {
            get
            {
                return JsonSerializer.ToJson(this.Tags);
            }
            set
            {
                this.Tags = JsonSerializer.FromJson(value);
            }
        }

        public string Quadkey
        {
            get
            {
                if (this.Coordinate == null) return null;
                return TileSystem.CoordinateToQuadKey((Coordinate)this.Coordinate, 20).ToString();
            }
        }

        [NotMapped]
        public Coordinate Coordinate
        {
            get
            {
                if (this.Latitude == null || this.Longitude == null)
                    return null;
                else
                    return new Coordinate((double)this.Latitude, (double)this.Longitude);
            }

            set
            {
                if (value != null)
                {
                    this.Latitude = value.Latitude;
                    this.Longitude = value.Longitude;
                }
                else
                {
                    this.Latitude = null;
                    this.Longitude = null;
                }
            }
        }

        [DataMember]
        public double? Latitude
        {
            set;
            get;
        }

        [DataMember]
        public double? Longitude
        {
            set;
            get;
        }

        [DataMember]
        public byte[] Thumb { set; get; }

        [NotMapped]
        public RemoteStorageInfo StorageInfo { set; get; }

        [DataMember]
        public string StorageInfoAsJson
        {
            set
            {
                this.StorageInfo = JsonSerializer<RemoteStorageInfo>.FromJson(value);
            }
            get
            {
                return JsonSerializer<RemoteStorageInfo>.ToJson(this.StorageInfo);
            }
        }


    }
}
