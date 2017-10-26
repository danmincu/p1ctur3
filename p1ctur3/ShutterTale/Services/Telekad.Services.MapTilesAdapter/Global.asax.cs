using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Telekad.Services.MapTilesAdapter
{
    [ExcludeFromCodeCoverage]
    public class Global : System.Web.HttpApplication
    {
        public static IEnumerable<IMapProvider> MapProviders { get; private set; }
        protected void Application_Start(object sender, EventArgs e)
        {
            MapProviders = MapsConfigurationSection.GetConfig().MapProviders;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}