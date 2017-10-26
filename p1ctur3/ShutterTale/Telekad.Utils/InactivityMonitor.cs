using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telekad.Utils
{
    /// <summary>
    /// Default implementation of IInactivityMonitor used to trigger 
    /// sending of the TimeEvents to Pre-recording service.
    /// </summary>
    public class InactivityMonitor : IInactivityMonitor
    {
        private System.Timers.Timer timer;

        #region Constructor

        /// <summary>
        /// Constructor with interval argument
        /// </summary>
        public InactivityMonitor(double inactivityNotificationInterval)
        {
            this.timer = new System.Timers.Timer();
            this.timer.Enabled = false;
            this.timer.AutoReset = true;
            this.InactivityNotificationInterval = inactivityNotificationInterval;
        }

        #endregion

        #region IDisposable related

        /// <summary>
        /// Dispose method of the <seealso cref="IDisposable"/>
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose will dispose the timer
        /// </summary>
        /// <param name="disposing">Boolean that decides wether we should dispose processor list </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (timer != null)
                {
                    timer.Dispose();
                    timer = null;
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Period of inactivity before monitor reports to the client
        /// </summary>
        public double InactivityNotificationInterval
        {
            get { return timer.Interval; }
            set
            {
                if (value > System.Int32.MaxValue)
                    throw new System.ArgumentException("InactivityNotificationInterval");

                timer.Interval = value;
            }
        }

        /// <summary>
        /// Subscription for Inactivity event
        /// </summary>
        public event System.Timers.ElapsedEventHandler Inactivity
        {
            add
            {
                timer.Elapsed += value;
            }
            remove
            {
                timer.Elapsed -= value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Starts inactivity monitor
        /// </summary>
        public void StartMonitoring()
        {
            this.timer.Start();
        }

        /// <summary>
        /// Stops previously started inactivity monitor
        /// </summary>
        public void StopMonitoring()
        {
            this.timer.Stop();
        }

        /// <summary>
        /// Client of the Inactivity Monitor reports latest activity
        /// </summary>
        public void OnActivity()
        {
            this.timer.Stop();
            this.timer.Start();
        }

        #endregion
    }
}
