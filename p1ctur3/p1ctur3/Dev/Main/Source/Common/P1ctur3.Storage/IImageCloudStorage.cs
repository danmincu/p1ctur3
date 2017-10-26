using P1ctur3.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1ctur3.Storage
{
    public interface IImageCloudStorage
    {
        RemoteStorageInfo Upload(Stream stream, string fileName, string title, string description, string tags, IProgress<long> progress);
        
    }
}
