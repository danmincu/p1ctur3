using System;
using System.Text;
using System.Threading.Tasks;

namespace Evasearch
{
    public abstract class FieldMetaBase
    {
        public abstract Type Type { get; }
        public string Name { set; get; }
    }
}
