using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1ctur3.Viewer
{
    public interface IViewModelError
    {
        /// <summary>
        /// Gets the Exception for the error that was set (null if no error).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Error")]
        Exception Error { get; }

        /// <summary>
        /// Gets the message for the error that was set (null if no error).
        /// </summary>
        string ErrorMessage { get; }

        /// <summary>
        /// Returns true if an error has been set (and not yet cleared), otherwise false.
        /// </summary>
        bool IsError { get; }

        /// <summary>
        /// Sets an error on the IViewModelError.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Error")]
        void SetError(Exception error);

        /// <summary>
        /// Clears the error from the IViewModelError.
        /// </summary>
        void ClearError();
    }
}
