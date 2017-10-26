using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Net;
using Telekad.Mapping;

namespace Telekad.Services.MapTilesAdapter.Bing
{
    public abstract class BingBaseMapLayer : BaseMapLayer
    {
        private static string configApplicationId;
        protected BingBaseMapLayer(IConfigurationElement configuration)
            : base(configuration)
        {
            configApplicationId = configuration["applicationId"];
        }

        public static string GetApplicationId()
        {
            if (!string.IsNullOrEmpty(configApplicationId))
                return configApplicationId;
            //if the applicationId for bing maps is NOT supplied via configuration we used one of the ours
            var mapLicense = new List<string>
                                 {
                                     "AmdSFgu2R1Vpb9-vYmfQ_F9tnnxS_ZC7GUequ0Y-PyqJnOO00L7n_ZmeJeKsfJGS",
                                     "ApHMn2WUA5OEzxk8DYpQ_mNnyzI5_SYu3KdWeHBaZuGKGCJX8H4v6S96MymCU3aR",
                                     "Ar9se8LvuDdwS6Y5WeU4l8n1fYI09oRt93TBSeegzo8q7FteU4co-BTD19Z2CePd",
                                     "AhhFfpFmteQ3-8S9apKrMrQl-6Y_40EsYat19V6xRzmJsCFlHLMcsMKLHl-UUtsr"
                                 };

            // Randomly grab of the license keys...
            var r = new Random();
            var randomKey = r.Next(0, 3);

            var applicationId = mapLicense[randomKey];

            return applicationId;
        }

        static int currentTileServer;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        protected void GetResponse(QuadKey key, string serverUrlFormat, Stream stream)
        {
            var request = (HttpWebRequest)WebRequest.Create(new Uri(
                string.Format(CultureInfo.InvariantCulture, serverUrlFormat, (currentTileServer++) % 4, key, GetApplicationId())));
            request.Timeout = 10 * 1000;
            request.KeepAlive = true;
            WebRequestToStream(request, stream, key);
        }
    }
}