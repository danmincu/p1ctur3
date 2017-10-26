using System;
using System.Globalization;

namespace Telekad.Utils
{
    public static class AttributeUtils
    {
        /// <summary>
        /// Attempts to retrieve an attribute from a type
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static bool TryGetAttribute<TAttribute>(this Type type, out TAttribute attr)
            where TAttribute : Attribute
        {
            object[] attrs = type.GetCustomAttributes(typeof(TAttribute), true);
            if (attrs.Length > 0)
            {
                attr = attrs[0] as TAttribute;
                return true;
            }

            attr = null;
            return false;
        }

        /// <summary>
        /// Retrieve an attribute from a type
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            TAttribute attr;
            if (!type.TryGetAttribute(out attr))
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Expected '{0}' attribute not found!", typeof(TAttribute)));
            return attr;
        }

        /// <summary>
        /// Attempts to retrieve an attribute from an enum field
        /// </summary>
        public static bool TryGetFieldAttribute<TAttribute>(this Enum enumField, out TAttribute attr)
            where TAttribute : Attribute
        {
            var fi = enumField.GetType().GetField(enumField.ToString());
            if (fi != null)
            {
                var attrs = fi.GetCustomAttributes(typeof(TAttribute), true);
                if (attrs.Length > 0)
                {
                    attr = attrs[0] as TAttribute;
                    return true;
                }
            }

            attr = null;
            return false;
        }

        /// <summary>
        /// Retrieves an attribute from an enum field (Throws an exception if not found)
        /// </summary>
        public static TAttribute GetFieldAttribute<TAttribute>(this Enum enumField)
            where TAttribute : Attribute
        {
            TAttribute result;
            if (!TryGetFieldAttribute(enumField, out result))
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Expected '{0}' attribute not found!", typeof(TAttribute)));
            return result;
        }

        /// <summary>
        /// Indicates whether an enum field is decorated with the specified attribute
        /// </summary>
        public static bool IsDecoratedWith<TAttribute>(this Enum enumField)
            where TAttribute : Attribute
        {
            var fi = enumField.GetType().GetField(enumField.ToString());
            if (fi != null)
            {
                var attrs = fi.GetCustomAttributes(typeof(TAttribute), true);
                return attrs.Length > 0;
            }
            return false;
        }
    }
}
