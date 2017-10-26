using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Telekad.Utils;

namespace P1ctur3.Types
{
    [DataContract]
    public class RemoteInfo
    {
        public RemoteInfo()
        {
            this.Properties = new Dictionary<string, string>();
        }
        public RemoteInfo(Guid id)
            : this()
        {
            this.RemoteInfoId = id;
        }

        [DataMember]
        [Key]
        public Guid RemoteInfoId { get; set; }
        
        [DataMember]
        public string Email { get; set; }

        public virtual List<ImageMetadata> ImageMetadatas { get; set; }

        [DataMember]
        public string Type { set; get; }

        [DataMember]
        public string Version { set; get; }

        [DataMember, NotMapped]
        public Dictionary<string, string> Properties { set; get; }

        public string PropertiesAsJson
        {
            get
            {
                return JsonSerializer.ToJson(this.Properties);
            }
            set
            {
                this.Properties = JsonSerializer.FromJson(value);
            }
        }
    }

    //public class FlickrInfo : RemoteInfo
    //{
    //    public FlickrInfo() : base() { }
        
    //    public FlickrInfo(RemoteInfo remoteInfo)
    //        : base(remoteInfo.RemoteInfoId)
    //    {
    //        this.Type = remoteInfo.Type;
    //        this.Version = remoteInfo.Version;
    //        foreach (var kvp in remoteInfo.Properties)
    //        {
    //            this.Properties.Add(kvp.Key, kvp.Value);
    //        }
    //    }

    //    public string FlickrKey
    //    {
    //        get
    //        {
    //            return this.Properties["FlickrKey"];
    //        }
    //        set
    //        {
    //            this.Properties.Add("FlickrKey", value);
    //        }
    //    }


    //    public string FlickrKeySecret
    //    {
    //        get
    //        {
    //            return this.Properties["FlickrKeySecret"];
    //        }
    //        set
    //        {
    //            this.Properties.Add("FlickrKeySecret", value);
    //        }
    //    }


    //    public string FlickrRequestToken
    //    {
    //        get
    //        {
    //            return this.Properties["FlickrRequestToken"];
    //        }
    //        set
    //        {
    //            this.Properties.Add("FlickrRequestToken", value);
    //        }
    //    }


    //    public string FlickrRequestTokenSecret
    //    {
    //        get
    //        {
    //            return this.Properties["FlickrRequestTokenSecret"];
    //        }
    //        set
    //        {
    //            this.Properties.Add("FlickrRequestTokenSecret", value);
    //        }
    //    }


    //    public string FlickrTokenVerifier
    //    {
    //        get
    //        {
    //            return this.Properties["FlickrTokenVerifier"];
    //        }
    //        set
    //        {
    //            this.Properties.Add("FlickrTokenVerifier", value);
    //        }
    //    }

    //}

}
