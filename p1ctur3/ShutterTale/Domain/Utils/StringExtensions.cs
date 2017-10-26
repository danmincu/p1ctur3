using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Telekad.Utils1
{
    /// <summary>
    /// Convenience extension methods.
    /// </summary>
    public static class StringExtensions
    {
        internal static readonly Regex SurroundedBySquareBracketsRegex = 
            new Regex(@"^\[(.+)\]$", RegexOptions.Compiled);
        internal static readonly Regex SurroundedByCurlyBracketsRegex =
            new Regex(@"^\{.+\}$", RegexOptions.Compiled);

        public static bool Contains(this string original, string value, StringComparison comparisionType)
        {
            if (original == null)
                return false;

            return original.IndexOf(value, comparisionType) >= 0;
        }

        /// <summary>
        /// Convenience shortcut for <see cref="String.Format(IFormatProvider, String, Object[])"/> where
        /// the first parameter is <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        public static string FormatInvariantCulture(this string format, params object[] args)
        {
            ArgumentValidation.CheckArgumentForNull(format, "format");
            ArgumentValidation.CheckArgumentForNull(args, "args");

            return String.Format(CultureInfo.InvariantCulture, format, args);
        }

        /// <summary>
        /// Convenience shortcut for <see cref="StringBuilder.AppendFormat(IFormatProvider, String, Object[])"/>
        /// followed by a call to <see cref="StringBuilder.AppendLine()"/>.
        /// </summary>
        /// 
        /// <param name="target">
        /// The <see cref="StringBuilder"/> instance to append to.
        /// </param>
        /// 
        /// <param name="format">
        /// A composite format string.
        /// </param>
        /// 
        /// <param name="args">
        /// An array of objects to format.
        /// </param>
        public static void AppendFormatInvariantCultureLine
            (this StringBuilder target, string format, params object[] args)
        {
            ArgumentValidation.CheckArgumentForNull(format, "format");
            ArgumentValidation.CheckArgumentForNull(args, "args");

            target.AppendFormat(CultureInfo.InvariantCulture, format, args);
            target.AppendLine();
        }

        /// <summary>
        /// Convenience shortcut for <see cref="DateTime.Parse(string, IFormatProvider, DateTimeStyles)"/> where
        /// the second parameter is <see cref="CultureInfo.InvariantCulture"/> and the third is
        /// <see cref="DateTimeStyles.None"/>.
        /// </summary>
        /// 
        /// <param name="date">
        /// A string representation of a date.
        /// </param>
        /// 
        /// <returns>
        /// A <see cref="DateTime"/> representing the date in <paramref name="date"/>.
        /// </returns>
        public static DateTime ParseDateTimeInvariantCulture(this string date)
        {
            ArgumentValidation.CheckArgumentForNull(date, "date");

            var result = DateTime.Parse(date, CultureInfo.InvariantCulture, DateTimeStyles.None);
            return result;
        }


        /// <summary>
        /// Convenience shortcut for <see cref="System.DateTime.TryParse(string, out System.DateTime)"/> o
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static bool TryParseDateTimeInvariantCulture(this string date, out DateTime dateTime)
        {
            ArgumentValidation.CheckArgumentForNull(date, "date");

            var result = DateTime.TryParse(date, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
            return result;
        }

        /// <summary>
        /// Convenience shortcut for <see cref="TimeSpan.Parse(string, IFormatProvider)"/> where
        /// the second parameter is <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        /// 
        /// <param name="time">
        /// A string representation of a time.
        /// </param>
        /// 
        /// <returns>
        /// A <see cref="TimeSpan"/> representing the time in <paramref name="time"/>.
        /// </returns>
        public static TimeSpan ParseTimeSpanInvariantCulture(this string time)
        {
            ArgumentValidation.CheckArgumentForNull(time, "time");

#if DOTNET35
            var result = TimeSpan.Parse(time);
#else
            var result = TimeSpan.Parse(time, CultureInfo.InvariantCulture);
#endif
            return result;
        }


        /// <summary>
        /// Tries the parse time span invariant culture.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="timeSpan">The time span.</param>
        /// <returns></returns>
        public static bool TryParseTimeSpanInvariantCulture(this string time, out TimeSpan timeSpan)
        {
            ArgumentValidation.CheckArgumentForNull(time, "time");

#if DOTNET35
            var result = TimeSpan.TryParse(time, out timeSpan);
#else
            var result = TimeSpan.TryParse(time, CultureInfo.InvariantCulture, out timeSpan);
#endif
            return result;
        }

        /// <summary>
        /// Surrounds the provided <paramref name="input"/> in [square brackets] if none are found.
        /// </summary>
        public static string SurroundWithSquareBrackets(this string input)
        {
            if(!SurroundedBySquareBracketsRegex.IsMatch(input))
            {
                return "[{0}]".FormatInvariantCulture(input);
            }
            return input;
        }

        /// <summary>
        /// Removes the provided <paramref name="input"/> [square brackets] if surrounding square brackets are found.
        /// </summary>
        public static string RemoveSurroundingSquareBrackets(this string input)
        {
            var match = SurroundedBySquareBracketsRegex.Match(input);
            if (match.Success)
            {
                return match.Groups[1].Value; ;
            }
            return input;
        }

        /// <summary>
        /// Surrounds the provided <paramref name="input"/> in {curly brackets} if none are found.
        /// </summary>
        public static string SurroundWithCurlyBrackets(this string input)
        {
            return SurroundWithCurlyBrackets(input, false);
        }

        /// <summary>
        /// Surrounds the provided <paramref name="input"/> in {curly brackets} if none are found
        /// or the caller specifies that the operation should occur regardless.
        /// </summary>
        public static string SurroundWithCurlyBrackets(this string input, bool forceBraces)
        {
            // TODO: This is a workaround to avoid the fact that the SurroundedByCurlyBracketsRegex
            //       thinks that the following pattern is a match:  {x}.{y}

            if (forceBraces || !SurroundedByCurlyBracketsRegex.IsMatch(input))
            {
                return "{{{0}}}".FormatInvariantCulture(input);
            }
            return input;
        }

        /// <summary>
        /// Generates a sequence of the lines from the provided <paramref name="input"/> string.
        /// </summary>
        /// 
        /// <remarks>
        /// This is more efficient than calling <see cref="String.Split(String[], StringSplitOptions)"/>
        /// since it does not allocate an entire array of the lines at once.  It is also more robust in the face of
        /// inconsistent line endings.
        /// </remarks>
        public static IEnumerable<string> SplitLines(this string input)
        {
            using (var sr = new StringReader(input))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        public static bool ToBoolean(this string input)
        {
            return ToBoolean(input, false);
        }

        /// <summary>
        /// Will try to convert a string to a boolean value.
        /// </summary>
        /// <param name="input">The input string that contains a logical true or false value, ie: true, false, yes, no, 1, 0, etc..</param>
        /// <param name="defaultValue">If the conversation fails, this is the value that will be returned.</param>
        /// <returns>The bool value that represents the string passed in.</returns>
        public static bool ToBoolean(this string input, bool defaultValue)
        {
            bool result;

            if (!TryParseBoolean(input, out result))
                result = defaultValue;

            return result;
        }

        /// <summary>
        /// Will try to convert a string to a boolean value.
        /// </summary>
        /// <param name="input">The input string that contains a logical true or false value, ie: true, false, yes, no, 1, 0, etc..</param>
        /// <param name="result">If the return value is true, this field will represent the value parsed from the string.</param>
        /// <returns>True if the value was successfully parsed, otherwise false.</returns>
        public static bool TryParseBoolean(this string input, out bool result)
        {
            result = false;
            bool retVal = true;
            if (string.IsNullOrEmpty(input))
                return false;

            try
            {
                switch (input.ToLower(CultureInfo.InvariantCulture))
                {
                    case "true":
                    case "yes":
                    case "y":
                    case "1":
                        result = true;
                        break;

                    case "false":
                    case "no":
                    case "n":
                    case "0":
                        result = false;
                        break;
                    default:
                        if (!Boolean.TryParse(input, out result))
                            retVal = false;
                        break;
                }

            }
            catch (Exception)
            {
                return false;
            }

            return retVal;
        }

        /// <summary>
        /// Strips the right most length characters from the provided value string. 
        /// </summary>
        /// <param name="value">the string to strip right characters</param>
        /// <param name="length">the number of right characters to remove</param>
        /// <returns></returns>
        public static string StripRight(this string value, int length)
        {
            return value.Substring(0, value.Length - length);
        }

        /// <summary>
        /// Strips the left most length characters from the provided value string. 
        /// </summary>
        /// <param name="value">the string to strip left characters</param>
        /// <param name="length">the number of left characters to remove</param>
        /// <returns></returns>
        public static string StripLeft(this string value, int length)
        {
            return value.Substring(length, value.Length - length);
        }
    }
}
