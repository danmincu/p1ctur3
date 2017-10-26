using System;
using System.Collections.Generic;
using Evasearch.Trees;
using Telekad.Types;

namespace Evasearch
{
    public class StructureBase<T>
    {
        public FieldMeta<T> Field { set; get; }
    }

    public class BaseCore<T>
    {
        public long Count { set; get; }

        private HashSet<long> documents;

        public HashSet<long> Documents
        {
            get
            {
                if (documents == null)
                {
                    this.documents = new HashSet<long>();
                }
                return documents;
            }
        }
    }

    public class FacetCore<T> : BaseCore<T>
    {
        public T Value { protected set; get; }
    }

    public class FacetBucket<T>
    {
        public FacetBucket()
        {
            this.TreeCore = new Dictionary<int, FacetCore<T>>();
        }

        public Dictionary<int, FacetCore<T>> TreeCore { get; private set; }
    }

    public class SimpleFacetStructure<T> : StructureBase<T>
    {
        public List<FacetBucket<T>> Buckets { set; get; }
    }


    public interface IMultiTerm
    {
        int Position(IMultiTerm content, IMultiTerm seach);
        Dictionary<int, IMultiTerm> Terms(IMultiTerm content);
        Dictionary<IMultiTerm, IEnumerable<int>> Occurences(IMultiTerm content);
        Boolean IsTerm(IMultiTerm content);
    }

    public class MultiFacetCore<T> : BaseCore<T> where T : IMultiTerm
    {
        public T Value { protected set; get; }
    }

    public class MultiFacetBucket<T> where T : IMultiTerm
    {
        public MultiFacetBucket()
        {
            this.TreeCore = new Dictionary<int, MultiFacetCore<T>>();
        }

        public Dictionary<int, MultiFacetCore<T>> TreeCore { get; private set; }
    }

    public class MultiFacetStructure<T> : StructureBase<T> where T: IMultiTerm
    {
        public List<MultiFacetBucket<T>> Buckets { set; get; }
    }

}