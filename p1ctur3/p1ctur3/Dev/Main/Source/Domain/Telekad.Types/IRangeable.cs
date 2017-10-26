using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telekad.Types
{
    public interface IRangeable
    {
        IRange RangeOf(int level);
    }
}
