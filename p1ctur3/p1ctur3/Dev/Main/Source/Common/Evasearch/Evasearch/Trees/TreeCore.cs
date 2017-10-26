using System.Collections.Generic;

namespace Evasearch.Trees
{
    /// <summary>
    /// TODO - use this as the core results for a bucket
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeCore<T>
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
        private Dictionary<int, object> preAggregations;
        public Dictionary<int, object> PreAggregations
        {
            get
            {
                if (preAggregations == null)
                {
                    this.preAggregations = new Dictionary<int, object>();
                }
                return preAggregations;
            }
        }

        public object this[int index]
        {
            set
            {
                if (this.PreAggregations.ContainsKey(index))
                    this.PreAggregations[index] = value;
                else this.PreAggregations.Add(index, value);
            }
        }
    }
}