using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telekad.Utils
{
    public class DelegateEqualityComparer<T> : EqualityComparer<T>
    {
        private Func<T, T, bool> predicate;

        public DelegateEqualityComparer(Func<T, T, bool> predicate)
        {
            this.predicate = predicate;
        }

        public override bool Equals(T x, T y)
        {
            return this.predicate(x, y);
        }

        public override int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}
