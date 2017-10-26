using System;

namespace Evasearch
{
    public class FieldMeta<T> : FieldMetaBase
    {
        public override Type Type
        {
            get { return typeof(T); }
        }
    }
}