using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;
using System.Dynamic;

namespace Telekad.Ux
{
    public class DynamicEventArgs : EventArgs
    {
        private Dictionary<string, object> innerDict = new Dictionary<string, object>();

        public DynamicEventArgs(object source, string eventType)
        {
            this.Source = source;
            this.Args = new ExpandoObject();
            this.EventType = eventType;
        }

        public object Source { get; private set; }
        public string EventType { get; private set; }
        public dynamic Args { get; private set; }
    }

    public class DynamicEvent : CompositePresentationEvent<DynamicEventArgs> { }
}
