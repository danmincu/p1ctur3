using System;
using System.Globalization;
using Telekad.Types;

namespace Evasearch.Ranges
{
    public class DateTimeWrapper : IRangeable
    {
        public DateTimeWrapper(DateTime value)
        {
            this.Value = value;
        }

        public DateTime Value { get; private set; }

        public IRange RangeOf(int level)
        {
            return new DateTimeRange(this.Value, level);
        }
        public override string ToString()
        {
            return this.Value.ToString(CultureInfo.InvariantCulture);
        }

    }
}