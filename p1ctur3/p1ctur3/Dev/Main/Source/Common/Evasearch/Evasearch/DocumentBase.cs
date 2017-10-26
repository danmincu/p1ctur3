using System;
using System.Collections.Generic;

namespace Evasearch
{
    public class DocumentBase
    {
        public DocumentBase()
        {
            this.Data = new Dictionary<string, FieldMetaBase>();
        }
        public Int64 ID { set; get; }
        public Dictionary<string, FieldMetaBase> Data { private set; get; }
    }
}