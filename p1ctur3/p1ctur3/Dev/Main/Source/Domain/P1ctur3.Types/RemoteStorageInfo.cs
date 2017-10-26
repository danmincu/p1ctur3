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
    public class RemoteStorageInfo
    {
        [DataMember]        
        public string Type { set; get; }

        [DataMember]
        public string Version { set; get; }

        //[NotMapped]
        [DataMember]
        public Dictionary<string, string> Properties { set; get; }

        //[DataMember]
        //public string PropertiesAsJson
        //{
        //    get
        //    {
        //        return JsonSerializer.ToJson(this.Properties);
        //    }
        //    set
        //    {
        //        this.Properties = JsonSerializer.FromJson(value);
        //    }
        //}

        [NotMapped]
        public string OriginalUrl
        {
            get
            {
                var id = this.Properties["PhotoId"];
                var osecret = this.Properties["OriginalSecret"];
                var farmid = this.Properties["Farm"];
                var serverid = this.Properties["Server"];
                var originalFormat = this.Properties["OriginalFormat"];

                return "http://farm{0}.staticflickr.com/{1}/{2}_{3}_o.{4}".FormatInvariantCulture(farmid, serverid, id, osecret, originalFormat);

            }
        }

        //Size Suffixes
        //The letter suffixes are as follows:
        //s	small square 75x75
        //q	large square 150x150
        //t	thumbnail, 100 on longest side
        //m	small, 240 on longest side
        //n	small, 320 on longest side
        //-	medium, 500 on longest side
        //z	medium 640, 640 on longest side
        //c	medium 800, 800 on longest side†
        //b	large, 1024 on longest side*

        public string GetUrl(string sizeSuffix)
        {
            var acceptedSuffixes = new string[] { "s", "q", "t", "m", "n", "-", "z", "c", "b" };

            if (!acceptedSuffixes.Any(v => v == sizeSuffix))
                throw new ArgumentException("Unsupported suffix");
            var id = this.Properties["PhotoId"];
            var secret = this.Properties["Secret"];
            var farmid = this.Properties["Farm"];
            var serverid = this.Properties["Server"];

            // http://farm{farm-id}.staticflickr.com/{server-id}/{id}_{secret}_[mstzb].jpg
            return "http://farm{0}.staticflickr.com/{1}/{2}_{3}_{4}.jpg".FormatInvariantCulture(farmid, serverid, id, secret, sizeSuffix);
        }

    }

}
