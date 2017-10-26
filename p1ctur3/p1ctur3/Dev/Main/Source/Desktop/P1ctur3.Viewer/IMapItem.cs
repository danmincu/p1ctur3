using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telekad.Mapping;

namespace P1ctur3.Viewer
{
    /// <summary>
    /// An interface for unit testing purposes
    /// </summary>
    public interface IMapItem
    {
        Coordinate Coordinate { get; }
        DateTime Timestamp { get; }
        int TrackpointCount { get; }
        /// <summary>
        /// The MapItem derives from the Item class, which doesn't currently have an interface.  
        /// This is a workaround as it would be significant work to retool the Item class to implement an interface.
        /// </summary>
        /// <returns></returns>
        Object AsItem();
        /// <summary>
        /// Gets or sets the id. Exposed via the Item base class
        /// </summary>
        /// <value>The id.</value>
        Guid Id { get; set; }
    }

    public class MapItem : IMapItem
    {
        public Coordinate Coordinate
        {
            set;
            get;
        }

        public DateTime Timestamp
        {
            get { throw new NotImplementedException(); }
        }

        public int TrackpointCount
        {
            set;
            get;
        }

        public object AsItem()
        {
            return this;
        }

        public Guid Id
        {
            set;
            get;
        }
    }
}
