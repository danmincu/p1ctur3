using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Telekad.Utils
{
    public static class Retry
    {
        public static void Do(Action action, TimeSpan retryInterval, int retryCount = 3)
        {
            Do<object>(() =>
            {
                action();
                return null;
            }, retryInterval, retryCount);
        }


        public static void DoRegardless(Action action, TimeSpan retryInterval, int retryCount, out bool succeeded)
        {
            DoRegardless<object>(() =>
            {
                action();
                return null;
            }, retryInterval, retryCount, out succeeded);
        }

        public static void DoRegardless(Action action, TimeSpan retryInterval, int retryCount,List<Exception> exceptions, out bool succeeded)
        {
            DoRegardless<object>(() =>
            {
                action();
                return null;
            }, retryInterval, retryCount, exceptions, out succeeded);
        }

        public static T Do<T>(Func<T> action, TimeSpan retryInterval, int retryCount = 3)
        {
            var exceptions = new List<Exception>();

            for (int retry = 0; retry < retryCount; retry++)
            {
                try
                {
                    return action();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                    Thread.Sleep(retryInterval);
                }
            }

            throw new AggregateException(exceptions);
        }

        public static T DoRegardless<T>(Func<T> action, TimeSpan retryInterval, int retryCount, out bool succeeded)
        {
            for (int retry = 0; retry < retryCount; retry++)
            {
                try
                {                    
                    T result = action();
                    succeeded = true;
                    return result;
                }
                catch (Exception ex)
                {
                    //exceptions.Add(ex);
                    Thread.Sleep(retryInterval);
                }
            }

            succeeded = false;
            return default(T);            
        }


        public static T DoRegardless<T>(Func<T> action, TimeSpan retryInterval, int retryCount, List<Exception> exceptions, out bool succeeded)
        {
            for (int retry = 0; retry < retryCount; retry++)
            {
                try
                {
                    T result = action();
                    succeeded = true;
                    return result;
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                    Thread.Sleep(retryInterval);
                }
            }

            succeeded = false;
            return default(T);
        }

    }
}
