using System;
using System.Collections.Generic;
using System.Linq;
using Evasearch.Ranges;
using Evasearch.Trees;
using Telekad.Types;

namespace Evasearch.Structures
{
    public class RangeStructure<T> : StructureBase<T> where T : IRangeable
    {
        public RangeStructure(int maxLevel)
        {
            this.MaxLevel = maxLevel;
            this.Buckets = new List<RangeBucket<T>>();
        }

        /// <summary>
        /// Method to return top buckets intersecting the list of values in the input
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public IEnumerable<RangeBucket<T>> Search(IEnumerable<T> values)
        {
            return this.Buckets.SelectMany(rb => rb.Search(values).OfType<RangeBucket<T>>());
        }

        public IEnumerable<RangeBucket<T>> Search(IEnumerable<IRangeExtended<T>> values)
        {
            var ticks = DateTime.Now.Ticks;            
            var result = this.Buckets.SelectMany(rb => rb.Search(values).OfType<RangeBucket<T>>());
            System.Diagnostics.Trace.WriteLine(string.Format("Search {0} items took {1} milliseconds",values.Count(), TimeSpan.FromTicks(DateTime.Now.Ticks - ticks).TotalMilliseconds));
            return result;
        }

        public int MaxLevel { set; private get; }

        public new List<RangeBucket<T>> Buckets { set; get; }

        public void Clean(int searchId)
        {
            foreach (var bucket in this.ForEach())
            {
                bucket.TreeCore.Remove(searchId);
            }
        }

        public virtual void Add(int searchId, T value, long documentId)
        {
            //insure there is at least the 0 level 
            RangeBucket<T> zeroLevel = this.Buckets.FirstOrDefault(b => b.Contains(value));
            if (zeroLevel == null)
            {
                zeroLevel = new RangeBucket<T>(value, 0);
                int funcIndex = 0;
                foreach (var func in this.Functions)
                {
                    var treeCore = zeroLevel[searchId];
                    //treeCore.PreAggregations.Add(funcIndex++, func(treeCore, value));
                    treeCore[funcIndex++] = func.PreaggregationFunc(treeCore, value);
                }
                this.Buckets.Add(zeroLevel);
            }

            RangeBucket<T> lastBucket = null;
            foreach (var bucket in this.FollowForEach(value).Where(bucket => bucket.Contains(value)))
            {
                //add documents only at the max level to preserve memory
                if (bucket.Level == this.MaxLevel)
                    bucket.Increase(searchId, documentId);
                else
                    bucket.Increase(searchId);

                //todo to be optimized
                int funcIndex = 0;
                var treeCore = bucket[searchId];
                foreach (var func in this.Functions)
                {   
                    //treeCore.PreAggregations.Add(funcIndex++, func(treeCore, value));
                    treeCore[funcIndex++] = func.PreaggregationFunc(treeCore, value);
                }


                if (lastBucket == null || lastBucket.Level <= bucket.Level)
                    lastBucket = bucket;
            }
            //add the missing buckets
            if (lastBucket == null || lastBucket.Level >= this.MaxLevel) return;
            var child = lastBucket;

            for (var i = lastBucket.Level; i < this.MaxLevel; i++)
            {
                var grandchild = new RangeBucket<T>(value, i + 1);
                //add documents only at the max level to preserve memory
                if (grandchild.Level == this.MaxLevel)
                    grandchild.Increase(searchId, documentId);
                else
                    grandchild.Increase(searchId);

                //to be optimized
                var funcIndex = 0;
                var treeCore = grandchild[searchId];
                foreach (var func in this.Functions)
                {                        
                    //treeCore.PreAggregations.Add(funcIndex++, func(treeCore, value));
                    treeCore[funcIndex++] = func.PreaggregationFunc(treeCore, value);
                }

                child.Children.Add(grandchild);
                child = grandchild;
            }
        }

        public IEnumerable<RangeBucket<T>> ForEach()
        {
            return this.Buckets.SelectMany(bucket => bucket.ForEach()).Cast<RangeBucket<T>>();
        }

        class AFunction
        {
            public string Name { set; get; }
            public Func<TreeCore<T>, T, object> PreaggregationFunc { set; get; }
            public Func<TreeCore<T>, T> AggregationFunc { set; get; }

        }

        IList<AFunction> functions;
        IList<AFunction> Functions
        {
            get { return this.functions ?? (this.functions = new List<AFunction>()); }
        }

        public void RegisterFunction(string name, Func<TreeCore<T>, T, object> preAggregateFunc, Func<TreeCore<T>, T> aggregateFunc)
        {
            this.Functions.Add(new AFunction() { Name = name, PreaggregationFunc = preAggregateFunc, AggregationFunc = aggregateFunc });
        }

        public IDictionary<string, T> Aggregations(int searchId, RangeBucket<T> bucket)
        {
            return this.Functions.ToDictionary(f => f.Name, f => f.AggregationFunc(bucket.TreeCore[searchId]));
        }

        public IEnumerable<RangeBucket<T>> FollowForEach(T value)
        {
            return this.Buckets.Where(a => a.Contains(value)).SelectMany(bucket => bucket.FollowForEach(value)).Cast<RangeBucket<T>>();
        }

        //count the documents for a given searchId
        public long Count(int searchId)
        {
            return this.Buckets.Sum(b => b.Count(searchId));
        }
    }
}