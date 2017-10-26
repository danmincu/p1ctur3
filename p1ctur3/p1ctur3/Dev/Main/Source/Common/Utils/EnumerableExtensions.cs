using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telekad.Utils
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Iterates the elements in <paramref name="enumerable" /> and calls <paramref name="action" /> for each element.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in <paramref name="enumerable" />.
        /// </typeparam>
        /// <param name="enumerable">
        /// The elements to perform <paramref name="action" /> on.
        /// </param>
        /// <param name="action">
        /// The <see cref="T:System.Action&lt;TSource&gt;"/> to perform on the elements of <paramref name="enumerable" />.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="enumerable" /> and/or <paramref name="action" /> is null.
        /// </exception>
        public static void ForEach<TSource>(this IEnumerable<TSource> enumerable, Action<TSource> action)
        {
            Utils.ArgumentValidation.CheckArgumentForNull(enumerable, "enumerable");
            Utils.ArgumentValidation.CheckArgumentForNull(action, "action");

            foreach (TSource t in enumerable)
            {
                action(t);
            }
        }

        /// <summary>
        /// Determines if <paramref name="enumerable"/> is empty.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in <paramref name="enumerable" />.
        /// </typeparam>
        /// <param name="enumerable">
        /// The <see cref="T:System.Collections.Generic.IEnumerable&lt;TSource&gt;"/> to check for elements
        /// </param>
        /// <returns>
        /// <c>true</c>, if there are any elements in <paramref name="enumerable"/>; <c>false</c>, otherwise.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="enumerable" /> is null
        /// </exception>
        public static bool Empty<TSource>(this IEnumerable<TSource> enumerable)
        {
            Utils.ArgumentValidation.CheckArgumentForNull(enumerable, "enumerable");

            return !enumerable.Any();
        }


        /// <summary>
        /// Creates an <see cref="T:System.Collections.Generic.IEnumerable&lt;T&gt;"/> from a tree of
        /// <see cref="T:System.Collections.IEnumerable"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of elements to be returned in the <see cref="T:System.Collections.Generic.IEnumerable&lt;T&gt;"/>.
        /// </typeparam>
        /// <param name="enumerable">
        /// The <see cref="T:System.Collections.IEnumerable"/> to check for elements of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// <see cref="T:System.Collections.Generic.IEnumerable&lt;T&gt;"/> containing elements found in the 
        /// <see cref="T:System.Collections.IEnumerable"/> tree.
        /// </returns>
        public static IEnumerable<T> Flatten<T>(this System.Collections.IEnumerable enumerable)
        {
            Utils.ArgumentValidation.CheckArgumentForNull(enumerable, "enumerable");

            foreach (object item in enumerable)
            {
                if (item is T)
                {
                    yield return (T)item;
                }
                else if (item is System.Collections.IEnumerable)
                {
                    foreach (T t in Flatten<T>((System.Collections.IEnumerable)item))
                    {
                        yield return t;
                    }
                }
            }
        }

        /// <summary>
        /// Returns a <see cref="Tuple"/> representing the minimum and maximum values in a sequence of 
        /// <see cref="Int64"/> <paramref name="values"/>.
        /// </summary>
        /// 
        /// <param name="values">
        /// A sequence of <see cref="Int64"/> values to determine the minimum and maximum values of.
        /// </param>
        /// 
        /// <returns>
        /// The minimum and maximum values in the sequence.
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">
        /// <paramref name="values"/> is <see langword="null"/>.
        /// </exception>
        /// 
        /// <exception cref="InvalidOperationException">
        /// <paramref name="values"/> contains no elements.
        /// </exception>
        public static Tuple<long, long> MinMax(this IEnumerable<long> values)
        {
            Utils.ArgumentValidation.CheckArgumentForNull(values, "values");

            var e = values.GetEnumerator();
            if (!e.MoveNext())
            {
                throw new InvalidOperationException("Input sequence contained no values");
            }

            long value = e.Current;
            long min = value;
            long max = value;

            while (e.MoveNext())
            {
                value = e.Current;
                min = Math.Min(min, value);
                max = Math.Max(max, value);
            }

            var result = new Tuple<long, long>(min, max);
            return result;
        }

        /// <summary>
        /// Performs a breadth first search on a set of "trees".
        /// </summary>
        /// <typeparam name="T">The type of the tree node</typeparam>
        /// <param name="source">The set of root elements to be traversed</param>
        /// <param name="childSelector">Selects the child elements of a tree node</param>
        /// <param name="includeRootItems">A flag indicating if the root elements of the tree should be included in the result.</param>
        /// <returns>An enumerable of the breadth first search of the set of trees.</returns>
        public static IEnumerable<T> BreadthFirstSearch<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> childSelector, bool includeRootItems)
        {
            return new BreadthFirstSearch<T>(source, childSelector, includeRootItems);
        }

        public static IEnumerable<T> Return<T>(params T[] values)
        {
            return values;
        }

    }

    internal class BreadthFirstSearch<T> : IEnumerable<T>
    {
        private Func<T, IEnumerable<T>> childSelector;
        private IEnumerable<T> root;
        private bool includeRootItems;

        public BreadthFirstSearch(IEnumerable<T> root, Func<T, IEnumerable<T>> childSelector, bool includeRootItems)
        {
            ArgumentValidation.CheckArgumentForNull(root, "root");
            ArgumentValidation.CheckArgumentForNull(childSelector, "childSelector");

            this.root = root;
            this.childSelector = childSelector;
            this.includeRootItems = includeRootItems;
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

        private class Enumerator : IEnumerator<T>
        {
            private enum EnumeratorState
            {
                BeforeFirst,
                Enumerating,
                AfterLast,
                Disposed
            }

            private BreadthFirstSearch<T> enumerable;
            private EnumeratorState state;
            private Queue<IEnumerable<T>> children;
            private IEnumerator<T> currentEnumerator;

            public Enumerator(BreadthFirstSearch<T> enumerable)
            {
                this.enumerable = enumerable;
                this.state = EnumeratorState.BeforeFirst;
            }

            #region IEnumerator<T> Members

            public T Current
            {
                get
                {
                    if (this.state != EnumeratorState.Enumerating)
                        throw new InvalidOperationException();

                    return this.currentEnumerator.Current;
                }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                this.state = EnumeratorState.Disposed;
            }

            #endregion

            #region IEnumerator Members

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    if (this.state != EnumeratorState.Enumerating)
                        throw new InvalidOperationException();

                    return this.currentEnumerator.Current;
                }
            }

            public bool MoveNext()
            {
                if (state == EnumeratorState.BeforeFirst)
                    return MoveToFirst();
                else if (state == EnumeratorState.Enumerating)
                    return MoveToNext();
                else if (state == EnumeratorState.Disposed)
                    throw new ObjectDisposedException("DepthFirstSearchEnumerableEnumerator");

                return false;
            }

            public void Reset()
            {
                this.state = EnumeratorState.BeforeFirst;
                this.children.Clear();
            }

            #endregion

            private bool MoveToFirst()
            {
                this.state = EnumeratorState.Enumerating;
                this.children = new Queue<IEnumerable<T>>();

                if (enumerable.includeRootItems)
                    this.children.Enqueue(enumerable.root);
                else
                    this.enumerable.root.ForEach(t => this.children.Enqueue(enumerable.childSelector(t)));

                return MoveToNext();
            }

            private bool MoveToNext()
            {
                if (this.state != EnumeratorState.Enumerating)
                    return false;

                while (this.currentEnumerator == null && this.children.Count > 0)
                {
                    // pop the next enumerable off the stack
                    IEnumerable<T> enumerable = this.children.Dequeue();
                    if (this.enumerable == null)
                        continue;

                    // Set the current enumerator
                    this.currentEnumerator = enumerable.GetEnumerator();
                }

                while (this.currentEnumerator != null)
                {
                    if (this.currentEnumerator.MoveNext())
                    {
                        // Success
                        this.children.Enqueue(this.enumerable.childSelector(this.currentEnumerator.Current));
                        return true;
                    }
                    else
                    {
                        // nothing left in this enumerator clear it and move onto the next one
                        this.currentEnumerator.Dispose();
                        this.currentEnumerator = null;

                        while (this.currentEnumerator == null && this.children.Count > 0)
                        {
                            // pop the next enumerable off the stack
                            IEnumerable<T> enumerable = this.children.Dequeue();
                            if (this.enumerable == null)
                                continue;

                            // Set the current enumerator
                            this.currentEnumerator = enumerable.GetEnumerator();
                        }
                    }
                }

                this.state = EnumeratorState.AfterLast;
                return false;
            }
        }
    }
}
