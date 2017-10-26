using P1ctur3.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1ctur3.Storage
{
    public interface IImageMetadataStorage
    {
        void Save(ImageMetadata imageMetadata, string registeredEmail);        
        void Save(P1ctur3.Types.RemoteInfo remoteInfo);
        bool IsEmailRegistered(string email);
        ImageMetadata Get(Guid imageMetadataId);
        IEnumerable<ImageMetadata> Get(IImageMetadataFilter filter);
        P1ctur3.Types.RemoteInfo Get(string email);

    }
}
