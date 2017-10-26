using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1ctur3.Storage.Flickr
{
    public interface IFlickrConfiguration
    {
        string Key { get; }
        string Secret { get; }
        string RequestToken { get; }
        string RequestTokenSecret { get; }
        string TokenVerifier { get; }

    }

}
