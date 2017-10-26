using System;
using System.IO;
using System.Reflection;

namespace Telekad.Utils
{
    /// <summary>
    /// Adds convenience extension methods to <see cref="Object"/>.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Convenience shortcut to <see cref="Assembly.GetManifestResourceStream(Type, String)"/> where the
        /// <see cref="Type"/> is obtained from the specified <paramref name="instance"/> and the <see cref="Assembly"/>
        /// is derived from said <see cref="Type"/>.
        /// </summary>
        /// 
        /// <param name="instance">
        /// An instance of a class that is a sibling to the embedded resource.
        /// </param>
        /// 
        /// <param name="fileName">
        /// The name of the file that is embedded as a resource inside the assembly where <paramref name="instance"/> is
        /// found.
        /// </param>
        /// 
        /// <returns>
        /// A <see cref="Stream"/> to the embedded resource data.
        /// </returns>
        public static Stream GetManifestResourceStream(this Object instance, string fileName)
        {
            var type = instance.GetType();
            return GetManifestResourceStream(type, fileName);
        }

        /// <summary>
        /// Convenience shortcut to <see cref="Assembly.GetManifestResourceStream(Type, String)"/> where the
        /// <see cref="Type"/> is comes from <paramref name="type"/> and the <see cref="Assembly"/>
        /// is derived from said <see cref="Type"/>.
        /// </summary>
        /// 
        /// <param name="type">
        /// A type that is a sibling to the embedded resource.
        /// </param>
        /// 
        /// <param name="fileName">
        /// The name of the file that is embedded as a resource inside the assembly where <paramref name="type"/> is
        /// found.
        /// </param>
        /// 
        /// <returns>
        /// A <see cref="Stream"/> to the embedded resource data.
        /// </returns>
        public static Stream GetManifestResourceStream(this Type type, string fileName)
        {
            var us = Assembly.GetAssembly(type);
            var result = us.GetManifestResourceStream(type, fileName);
            return result;
        }

        /// <summary>
        /// Convenience method to facilitate the computation of a hash code in implementations of
        /// <see cref="Object.GetHashCode()"/>.
        /// </summary>
        /// 
        /// <param name="instance">
        /// The object from which this method is invoked.  It is not used in the computation of the hash code but is
        /// merely there to be able to invoke this method on any object.
        /// </param>
        /// 
        /// <param name="fields">
        /// The values of the object's fields to participate in the computation of the hash code.
        /// </param>
        /// 
        /// <returns>
        /// A hash code combining that of all the supplied <paramref name="fields"/>.
        /// </returns>
        /// 
        /// <example>
        /// <para>
        /// The example below illustrates the typical use of the <see cref="ComputeHashCode(Object, Object[])"/> method:
        /// </para>
        /// <code>
        /// <![CDATA[
        /// internal class ResultRange
        /// {
        ///     private readonly string rangeStart;
        ///     private readonly string rangeEnd;
        ///     private readonly Int64 rangeSize;
        ///     private readonly Int64 numberOfItemsSeen;
        ///     private readonly Int64 numberOfSkippedItems;
        ///     private readonly Int64 offsetInsideBucket;
        ///     private readonly int hashCode;
        ///     private readonly string toString;
        /// 
        ///     public ResultRange
        ///         (string rangeStart, string rangeEnd, long rangeSize, long numberOfItemsSeen, long offsetInsideBucket)
        ///     {
        ///         this.rangeStart = rangeStart;
        ///         this.rangeEnd = rangeEnd;
        ///         this.rangeSize = rangeSize;
        ///         this.numberOfItemsSeen = numberOfItemsSeen;
        ///         this.numberOfSkippedItems = numberOfItemsSeen - rangeSize;
        ///         this.offsetInsideBucket = offsetInsideBucket;
        ///         this.hashCode = this.ComputeHashCode(
        ///                             rangeStart,
        ///                             rangeEnd,
        ///                             rangeSize,
        ///                             numberOfItemsSeen,
        ///                             offsetInsideBucket);
        ///         this.toString =
        ///             "[{0} - {1}] size: {2}, seen: {3}, offset: {4}".FormatInvariantCulture
        ///             (rangeStart, rangeEnd, rangeSize, numberOfItemsSeen, offsetInsideBucket);
        ///     }
        /// 
        ///     public override string ToString()
        ///     {
        ///         return this.toString;
        ///     }
        /// 
        ///     public override bool Equals(object obj)
        ///     {
        ///         var that = obj as ResultRange;
        ///         if (null == that)
        ///         {
        ///             return false;
        ///         }
        ///         return this.rangeStart == that.rangeStart
        ///                && this.rangeEnd == that.rangeEnd
        ///                && this.rangeSize == that.rangeSize
        ///                && this.numberOfItemsSeen == that.numberOfItemsSeen
        ///                && this.offsetInsideBucket == that.offsetInsideBucket;
        ///     }
        /// 
        ///     public override int GetHashCode()
        ///     {
        ///         return this.hashCode;
        ///     }
        /// }]]>
        /// </code>
        /// </example>
        public static int ComputeHashCode(this Object instance, params Object[] fields)
        {
            int result = (fields.Length == 0 || fields[0] == null) ? 0 : fields[0].GetHashCode();

            for (int i = 1; i < fields.Length; i++)
            {
                result = CombineHashCodes(result, (fields[i] == null) ? 0 : fields[i].GetHashCode());
            }

            return result;
        }

        /// <summary>
        /// A utility method for combining hash codes.  This was taken from <see cref="T:System.Tuple" /> since the
        /// method there is declare internal only and to avoid creating Tuples everywhere we want to use it.
        /// </summary>
        /// <param name="h1">The first hash code.</param>
        /// <param name="h2">The second hash code.</param>
        private static int CombineHashCodes(int h1, int h2)
        {
            return (((h1 << 5) + h1) ^ h2);
        }
    }
}
