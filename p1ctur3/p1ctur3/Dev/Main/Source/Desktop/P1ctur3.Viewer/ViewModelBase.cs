using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telekad.Utils;
using Telekad.Ux;

namespace P1ctur3.Viewer
{
    public class ViewModelBase : NotifyObject, IViewModelError
    {
        #region Fields

        // design mode indicator
        private static readonly bool isInDesignMode;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Static constructor
        /// </summary>
        static ViewModelBase()
        {
            // set our design mode
            DependencyProperty prop = DesignerProperties.IsInDesignModeProperty;
            isInDesignMode
                = (bool)DependencyPropertyDescriptor
                             .FromProperty(prop, typeof(FrameworkElement))
                             .Metadata.DefaultValue;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Are we in design mode
        /// </summary>
        public bool IsInDesignMode
        {
            get { return isInDesignMode; }
        }

        public bool IsInDebugMode
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// Should errors (exceptions) be logged
        /// </summary>
        protected bool LogErrors { get; set; }

        /// <summary>
        /// Should exceptions be thrown
        /// </summary>
        protected bool ThrowExceptionOnError { get; set; }

        #endregion Properties

        #region IViewModelError Members

        public Exception Error
        {
            get { return Get(() => Error); }
            private set { Set(() => Error, value); }
        }

        [DependsUpon("Error")]
        public string ErrorMessage
        {
            get { return Error == null ? null : Error.Message; }
        }

        [DependsUpon("Error")]
        public bool IsError
        {
            get { return Error != null; }
        }

        public virtual void SetError(Exception error)
        {
            ArgumentValidation.CheckArgumentForNull(error, "error");

            if (Error == error)
                return;

            if (error is AggregateException && error.InnerException != null)
            {
                Error = error.InnerException;
            }
            else
            {
                Error = error;
            }

            if (LogErrors)
            {
              //  ExceptionPolicy.HandleException(error, "Default Policy");
            }

            if (ThrowExceptionOnError)
                throw error;
        }

        public void ClearError()
        {
            if (Error == null)
                return;
            Error = null;
        }

        #endregion

    }
}
