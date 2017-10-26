using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telekad.Utils
{
    public static class FormatStringFunctions
    {
        public static string FormatBytes(long size)
        {
            return FormatBytes(size, 2);
        }
        public static string FormatBytes(long size, int precision)
        {
            if (size == 0)
                return "0";
            var baze = Math.Log(size) / Math.Log(1024);
            var suffixes = new string[] { "", "k", "M", "G", "T" };

            return string.Format(CultureInfo.InvariantCulture,"{0} {1}bytes", Math.Round(Math.Pow(1024, baze - Math.Floor(baze)), precision).ToString(),suffixes[(int)Math.Floor(baze)]);
        }

    }
}
