using System;
using System.Globalization;
using Telekad.Types;

namespace Evasearch.Ranges
{
    public class DateTimeRange : IRangeExtended<DateTimeWrapper>
    {
        private readonly TimeBucketType timeBucketType;
        public object Value { private set; get; }
        private DateTime dateValue;

        public int Level { private set; get; }

        public DateTimeRange(DateTime value, int level)
        {
            this.Value = value;
            this.dateValue = value;
            this.Level = level;
            this.timeBucketType = (TimeBucketType)Enum.ToObject(typeof(TimeBucketType), level);
        }

        public string Name
        {
            get
            {
                switch (this.timeBucketType)
                {
                    case TimeBucketType.Year:
                        return this.dateValue.Year.ToString();
                    case TimeBucketType.Month:
                        return this.dateValue.Year + "/" +
                               this.dateValue.Month;
                    case TimeBucketType.Day:
                        return this.dateValue.Date.ToString(CultureInfo.InvariantCulture);
                    case TimeBucketType.Hour:
                        return this.dateValue.Date + " H:" +
                               this.dateValue.Hour;
                    case TimeBucketType.Minute:
                        return this.dateValue.Date + " H:" +
                               this.dateValue.Hour + " mm:" + this.dateValue.Minute;
                    case TimeBucketType.Second:
                        return this.dateValue.Date + " H:" +
                               this.dateValue.Hour + " mm:" + this.dateValue.Minute + " s:" + this.dateValue.Second;
                    case TimeBucketType.Millisecond:
                        return this.dateValue.Date + " H:" +
                               this.dateValue.Hour + " mm:" + this.dateValue.Minute + " s:" + this.dateValue.Second + " msec:" + this.dateValue.Millisecond;
                    case TimeBucketType.Tick:
                        return "Ticks:" + this.dateValue.Ticks;
                    default:
                        return null;
                }
            }
        }

        public bool OverlapsWith(IRange range)
        {
            return Contains(range) || range.Contains(this);
        }

        public bool Contains(IRange value)
        {
            var innerRange = value as DateTimeRange;
            if (innerRange == null || this.Level > innerRange.Level)
                return false;
            return this.Contains(new DateTimeWrapper((DateTime)innerRange.Value));
        }

        public bool Contains(DateTimeWrapper innerValue)
        {
            switch (this.timeBucketType)
            {
                case TimeBucketType.Year:
                    return innerValue.Value.Year == this.dateValue.Year;
                case TimeBucketType.Month:
                    return innerValue.Value.Year == this.dateValue.Year &&
                           innerValue.Value.Month == this.dateValue.Month;
                case TimeBucketType.Day:
                    return innerValue.Value.Date == this.dateValue.Date;
                case TimeBucketType.Hour:
                    return innerValue.Value.Date == this.dateValue.Date &&
                           innerValue.Value.Hour == this.dateValue.Hour;
                case TimeBucketType.Minute:
                    return innerValue.Value.Date == this.dateValue.Date &&
                           innerValue.Value.Hour == this.dateValue.Hour &&
                           innerValue.Value.Minute == this.dateValue.Minute;
                case TimeBucketType.Second:
                    return innerValue.Value.Date == this.dateValue.Date &&
                           innerValue.Value.Hour == this.dateValue.Hour &&
                           innerValue.Value.Minute == this.dateValue.Minute &&
                           innerValue.Value.Second == this.dateValue.Second;
                case TimeBucketType.Millisecond:
                    return innerValue.Value.Date == this.dateValue.Date &&
                           innerValue.Value.Hour == this.dateValue.Hour &&
                           innerValue.Value.Minute == this.dateValue.Minute &&
                           innerValue.Value.Second == this.dateValue.Second &&
                           innerValue.Value.Millisecond == this.dateValue.Millisecond;
                case TimeBucketType.Tick:
                    return innerValue.Value.Ticks == this.dateValue.Ticks;
                default:
                    return false;
            }
        }
    }
}