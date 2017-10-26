using System.Collections.Generic;
using System.Linq;
using Evasearch.Trees;
using Telekad.Types;

namespace Evasearch.Trees
{
    public class BucketTreeBase<T> where T : IRangeable
    {
        public BucketTreeBase()
        {
            this.Children = new List<BucketTreeBase<T>>();
            this.HasDocumentPointers = false;
            this.TreeCore = new Dictionary<int, TreeCore<T>>();
        }

        //holds an id for the search and the count of documents for that particular search
        public Dictionary<int, TreeCore<T>> TreeCore { get; private set; }

        public TreeCore<T> this[int index]
        {
            get
            {
                TreeCore<T> existent;
                if (TreeCore.TryGetValue(index, out existent))
                {
                    return existent;
                }
                var newCore = new TreeCore<T>();
                TreeCore.Add(index, newCore);
                return newCore;
            }

        }

        public long Count(int searchId)
        {
            TreeCore<T> value;
            return TreeCore.TryGetValue(searchId, out value) ? value.Count : 0;
        }

        public void Increase(int searchId)
        {
            TreeCore<T> existent = null;
            if (TreeCore.TryGetValue(searchId, out existent))
            {
                this.TreeCore[searchId].Count = existent.Count + 1;
            }
            else
                this.TreeCore.Add(searchId, new TreeCore<T>() { Count = 1 });
        }

        public bool HasDocumentPointers
        {
            get;
            private set;
        }

        public void Increase(int searchId, long documentId)
        {
            this.HasDocumentPointers = true;
            this.Increase(searchId);

            TreeCore<T> existentCore = null;
            if (TreeCore.TryGetValue(searchId, out existentCore))
            {
                existentCore.Documents.Add(documentId);
            }
            else
            {
                var newCore = new TreeCore<T>();
                newCore.Documents.Add(documentId);
                this.TreeCore.Add(searchId, newCore);
            }
        }


        public int Level { protected set; get; }

        public virtual List<BucketTreeBase<T>> Children { private set; get; }

        public IEnumerable<BucketTreeBase<T>> ForEach()
        {
            foreach (var child in this.Children)
            {
                foreach (var item in child.ForEach())
                {
                    yield return item;
                }
            }
            yield return this;
        }

        public IEnumerable<long> DocumentIds(int searchId)
        {
            return this.ForEach().Where(b => b.HasDocumentPointers).
                SelectMany(b => (b.TreeCore.ContainsKey(searchId)) ? b.TreeCore[searchId].Documents : new HashSet<long>());
        }

    }
}