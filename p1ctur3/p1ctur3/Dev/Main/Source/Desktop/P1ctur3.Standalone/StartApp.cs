using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace P1ctur3.Standalone
{
    /// <summary>
    /// App
    /// </summary>
    public partial class App : System.Windows.Application
    {

        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main()
        {
            Logger.SetLogWriter(new LogWriterFactory().Create());
            var wi = WindowsIdentity.GetCurrent();
            var wp = new WindowsPrincipal(wi);

            bool runAsAdmin = wp.IsInRole(WindowsBuiltInRole.Administrator);

            if (!runAsAdmin)
            {
                // It is not possible to launch a ClickOnce app as administrator directly,
                // so instead we launch the app as administrator in a new process.
                var processInfo = new ProcessStartInfo(Assembly.GetExecutingAssembly().CodeBase);

                // The following properties run the new process as administrator
                processInfo.UseShellExecute = true;
                processInfo.Verb = "runas";

                // Start the new process
                try
                {
                    Process.Start(processInfo);
                }
                catch (Exception e)
                {
                    Logger.Write(Telekad.Utils.ExceptionUtils.CreateExceptionString(e));                    
                    // The user did not allow the application to run as administrator
                    MessageBox.Show("Sorry, but I don't seem to be able to start " +
                       "this program with administrator rights! I need admin rights to be able to scan your entire drive looking for pictures!");
                }

                // Shut down the current process
                if (Application.Current != null)
                  Application.Current.Shutdown();
            }
            else
            {
                // We are running as administrator
                P1ctur3.Standalone.App app = new P1ctur3.Standalone.App();
                app.StartupUri = new System.Uri("MainWindow.xaml", UriKind.Relative);                
                app.Run();
            }


        }
    }
}
