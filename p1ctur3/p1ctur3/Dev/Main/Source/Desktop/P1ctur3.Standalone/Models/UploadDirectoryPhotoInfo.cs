using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1ctur3.Standalone.Models
{
    public class UploadDirectoryPhotoInfo
    {
        public string CurrentFile { set; get; }
        public int CurrentFileCount { set; get; }
        public int TotalFileCount { set; get; }
        public Exception LastException { set; get; }
    }
}
