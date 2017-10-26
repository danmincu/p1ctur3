using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Telekad.Utils;

namespace P1ctur3.Viewer
{
    /// <summary>
    /// Observable Extensions
    /// </summary>
    public static class ObservableExtensions
    {
        /// <summary>
        /// Creates an IObservable from a property that implements INotifyPropertyChanged
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        /// <remarks>
        /// Source (and discussion) here:  http://social.msdn.microsoft.com/Forums/en/rx/thread/36bf6ecb-70ea-4be3-aa35-b9a9cbc9a078
        /// </remarks>
        public static IObservable<TReturn> ToObservable<T, TReturn>(this T target, Expression<Func<T, TReturn>> property) where T : INotifyPropertyChanged
        {
            ArgumentValidation.CheckArgumentForNull(target, "target");
            ArgumentValidation.CheckArgumentForNull(property, "property");

            var f = property.Body as MemberExpression;

            if (f == null)
            {
                throw new NotSupportedException("Only use expressions that call a single property");
            }

            var propertyName = f.Member.Name;
            var getValueFunc = property.Compile();


            return Observable.Create<TReturn>(o =>
            {
                var eventHandler = new PropertyChangedEventHandler((s, pce) =>
                {
                    if (pce.PropertyName == null || pce.PropertyName == propertyName)
                        o.OnNext(getValueFunc(target));
                });
                target.PropertyChanged += eventHandler;
                return () => target.PropertyChanged -= eventHandler;
            });
        }

        /// <summary>
        /// Buffers the specified source until threshold time or threshold count.
        /// </summary>
        public static IObservable<IList<TSource>> BufferWithThresholdTimeOrCount<TSource>(this IObservable<TSource> source, TimeSpan thresholdTime, int thresholdCount, IScheduler scheduler)
        {
            return Observable.Create<IList<TSource>>(o =>
            {
                var pendingValues = new List<TSource>();

                IDisposable timerSub = null;

                var sourceSub = source.Subscribe(value =>
                {
                    List<TSource> valuesToPublish = null;
                    lock (pendingValues)
                    {
                        pendingValues.Add(value);
                        if (pendingValues.Count >= thresholdCount)
                        {
                            valuesToPublish = pendingValues.ToList();
                            pendingValues.Clear();
                            if (timerSub != null)
                            {
                                timerSub.Dispose();
                                timerSub = null;
                            }
                        }
                    }

                    if (timerSub == null)
                        timerSub = Observable.Timer(DateTime.Now + thresholdTime, thresholdTime, scheduler).Subscribe(t =>
                        {
                            timerSub = null;
                            List<TSource> valuesCopy2;
                            lock (pendingValues)
                            {
                                if (pendingValues.Count == 0)
                                    return;
                                valuesCopy2 = pendingValues.ToList();
                                pendingValues.Clear();
                            }
                            o.OnNext(valuesCopy2);
                        });

                    if (valuesToPublish != null)
                        o.OnNext(valuesToPublish);

                }, o.OnError, o.OnCompleted);

                return () =>
                {
                    timerSub.Dispose();
                    sourceSub.Dispose();
                };
            });
        }
       
        /// <summary>
        /// Buffers the specified source until throttle time or threshold count.
        /// </summary>
        public static IObservable<IList<TSource>> BufferWithThrottleTimeOrCount<TSource>(this IObservable<TSource> source, TimeSpan throttleTime, int thresholdCount, IScheduler scheduler)
        {
            return Observable.Create<IList<TSource>>(o =>
            {
                var pendingValues = new List<TSource>();

                var sourceSub = source.Subscribe(value =>
                {
                    List<TSource> valuesToPublish;
                    lock (pendingValues)
                    {
                        pendingValues.Add(value);
                        if (pendingValues.Count <= thresholdCount)
                            return;
                        valuesToPublish = pendingValues.ToList();
                        pendingValues.Clear();
                    }
                    o.OnNext(valuesToPublish);
                }, o.OnError, o.OnCompleted);

                var throttleSub = source.Throttle(throttleTime, scheduler).Subscribe(value =>
                {
                    List<TSource> valuesToPublish;
                    lock (pendingValues)
                    {
                        if (pendingValues.Count == 0)
                            return;
                        valuesToPublish = pendingValues.ToList();
                        pendingValues.Clear();
                    }
                    o.OnNext(valuesToPublish);
                });

                return () =>
                {
                    sourceSub.Dispose();
                    throttleSub.Dispose();
                };
            });
        }

        /// <summary>
        /// Buffers items in an observable sequence until the sequence is inactive for a period of time
        /// specified be the <paramref name="thresholdTime"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of element in the observable sequence.</typeparam>
        /// <param name="source">The source observable.</param>
        /// <param name="thresholdTime">The timeout period to wait before publishing the buffer.</param>
        /// <returns>An observable sequence of lists of buffered elements.</returns>
        public static IObservable<IList<TSource>> BufferWithThresholdTime<TSource>(this IObservable<TSource> source, TimeSpan thresholdTime)
        {
            // Windows the source stream based on a throttle of the itself.  This causes the window to close
            // when the throttle time elapses on the original stream, producing the list of items in the sequence.
            return source.BufferWithThresholdTime(thresholdTime, Scheduler.TaskPool);
        }

        /// <summary>
        /// Buffers items in an observable sequence until the sequence is inactive for a period of time
        /// specified be the <paramref name="thresholdTime"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of element in the observable sequence.</typeparam>
        /// <param name="source">The source observable.</param>
        /// <param name="thresholdTime">The timeout period to wait before publishing the buffer.</param>
        /// <param name="scheduler">The scheduler to execute the buffering on.</param>
        /// <returns>An observable sequence of lists of buffered elements.</returns>
        public static IObservable<IList<TSource>> BufferWithThresholdTime<TSource>(this IObservable<TSource> source, TimeSpan thresholdTime, IScheduler scheduler)
        {
            // Windows the source stream based on a throttle of the itself.  This causes the window to close
            // when the throttle time elapses on the original stream, producing the list of items in the sequence.
            return source
                    .Window(() => source.Throttle(thresholdTime, scheduler))
                    .SelectMany(Observable.ToList);
        }
    }
}