using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telekad.Types
{
    public interface IRangeExtended<in T> : IRange<T> where T : IRangeable
    {
        string Name { get; }
        object Value { get; }
        int Level { get; }
    }
}
