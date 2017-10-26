using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Telekad.Mapping
{
    [Serializable]
    [ExcludeFromCodeCoverage] // Nothing to test
    public class MappingException : Exception
    {
        public MappingException() : base() { }
        public MappingException(string message) : base(message) { }
        public MappingException(string message, Exception exception) : base(message, exception) { }
        protected MappingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
