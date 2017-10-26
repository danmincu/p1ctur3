using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telekad.Utils;

namespace Telekad.Utils
{
    public class HttpUploader
    {
        public Task<WebResponse> UploadDirectly(Stream stream, string fullFilePath)
        {
            return UploadDirectly(stream, fullFilePath, null);
        }

        public Task<WebResponse> UploadDirectly(Stream stream, string fullFilePath, IProgress<long> progress)
        {
            var id = Telekad.Utils.Base64.Base64Encode(fullFilePath);
            string serviceUri = string.Format(CultureInfo.InvariantCulture, "http://localhost/P1ctur3.Web/api/photo/{0}/", id);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(serviceUri);
            request.Method = "POST";

            using (var requestStream = request.GetRequestStream())
            {
                stream.Seek(0, 0);
                stream.CopyTo(requestStream, progress);
                requestStream.Close();
                var tsk = request.GetResponseAsync();
                return tsk;
            }

        }

    }
}
