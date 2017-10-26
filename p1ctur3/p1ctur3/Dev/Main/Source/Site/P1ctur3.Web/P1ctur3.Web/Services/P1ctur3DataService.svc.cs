using P1ctur3.Storage.Sql;
using P1ctur3.Types;
//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Data.Services.Providers;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;

namespace P1ctur3.Web.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class P1ctur3DataService : EntityFrameworkDataService<ImageMetadataContext>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {

            //var a = new Types.RemoteStorageInfo() { Type="a", Version="213"};
            //a.Properties.Add("a", "b");
            //System.Data.Services.LiteralFormatter.DefaultLiteralFormatter.FormatLiteralWithTypePrefix


            // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
            // Examples:
            // config.SetEntitySetAccessRule("MyEntityset", EntitySetRights.AllRead);
            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);
            config.SetEntitySetAccessRule("RemoteInfos", EntitySetRights.All);
            config.SetEntitySetAccessRule("Images", EntitySetRights.All);
            config.UseVerboseErrors = true;
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }
    }
}
