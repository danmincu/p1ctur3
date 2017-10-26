using System.Collections.Generic;
using System.Linq;
using Evasearch.Trees;
using Telekad.Types;

namespace Evasearch.Ranges
{
    public class RangeBucket<T> : BucketTreeBase<T> where T : IRangeable
    {
        public IRangeExtended<T> Value { protected set; get; }
        public RangeBucket(T value, int level)
        {
            this.Value = (IRangeExtended<T>)value.RangeOf(level);
            this.Level = level;
        }

        public bool Contains(T value)
        {
            return this.Value.Contains(value);
        }

        public bool Contains(IRangeExtended<T> value)
        {
            return this.Value.Contains(value);
        }

        public override string ToString()
        {
            return string.Format("Range bucket:{0} level:{1} counts:{2}", this.Value.Name, this.Value.Level,
                (this.TreeCore.Any()) ? this.TreeCore.FirstOrDefault().Value.Count : 0);
        }

        public IEnumerable<BucketTreeBase<T>> FollowForEach(T value)
        {
            foreach (var child in this.Children.OfType<RangeBucket<T>>().Where(a => a.Contains(value)))
            {
                foreach (var item in child.FollowForEach(value))
                {
                    yield return item;
                }
            }
            yield return this;
        }

        public IEnumerable<BucketTreeBase<T>> Search(IEnumerable<T> values)
        {
            if (!values.Any()) yield break;


            if (values.OfType<IRange>().Select(r => /*r == this.Value ||*/ r.Contains(this.Value)).Any())
                yield return this;


            var candidateValues = values.Where(v => this.Contains(v));
            if (candidateValues.Any())
            {
                if (!this.Children.Any())
                {
                    yield return this;
                }
                else
                {
                    foreach (var child in this.Children.OfType<RangeBucket<T>>())
                    {
                        var childCandidateValues = candidateValues.Where(v => child.Contains(v));
                        if (childCandidateValues.Any())
                        {
                            foreach (var item in child.Search(childCandidateValues))
                            {
                                yield return item;
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<BucketTreeBase<T>> Search(IEnumerable<IRangeExtended<T>> values)
        {
            if (!values.Any()) yield break;

            if (values.Any(r => r.Contains(this.Value)))
            {
                yield return this;
            }

            var candidateValues = values.Where(v => this.Contains(v));
            if (candidateValues.Any())
            {
                if (!this.Children.Any())
                {
                    yield return this;
                }
                else
                {
                    foreach (var child in this.Children.OfType<RangeBucket<T>>())
                    {
                        var childCandidateValues = candidateValues.Where(v => child.Contains(v));
                        if (childCandidateValues.Any())
                        {
                            foreach (var item in child.Search(childCandidateValues))
                            {
                                yield return item;
                            }
                        }
                    }
                }
            }
        }

    }
}