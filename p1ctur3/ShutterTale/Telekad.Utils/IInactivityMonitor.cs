using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telekad.Utils
{
    /// <summary>
    /// Base for InactivityMonitor makes testing easier
    /// </summary>
    public interface IInactivityMonitor : IDisposable
    {
        void StartMonitoring();
        void StopMonitoring();
        void OnActivity();

        event System.Timers.ElapsedEventHandler Inactivity;
        double InactivityNotificationInterval { get; set; }
    }
}
