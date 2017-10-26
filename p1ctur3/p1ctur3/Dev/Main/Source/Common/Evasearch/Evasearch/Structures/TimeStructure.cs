using System;
using Evasearch.Ranges;
using Evasearch.Trees;

namespace Evasearch.Structures
{
    public class TimeStructure : RangeStructure<DateTimeWrapper>
    {
        public TimeStructure(int maxLevel) : base(maxLevel) {

            this.RegisterFunction("MinDate", AddMinDate, GetMinDate);
            this.RegisterFunction("MaxDate", AddMaxDate, GetMaxDate);
        }

        private static DateTimeWrapper AddMinDate(TreeCore<DateTimeWrapper> core, DateTimeWrapper value)
        {
            object aggregateValue = null;
            if (core.PreAggregations.TryGetValue(0, out aggregateValue))
            {
                var date = (DateTimeWrapper)aggregateValue;
                if (date == null)
                    throw new InvalidProgramException("The agregate values are not of the expected type. DateTime");
                return (date.Value < value.Value) ? date : value;
            }
            return value;
        }

        private static DateTimeWrapper GetMinDate(TreeCore<DateTimeWrapper> core)
        {
            object aggregateValue = null;
            if (core.PreAggregations.TryGetValue(0, out aggregateValue))
            {
                var date = (DateTimeWrapper)aggregateValue;
                if (date == null)
                    throw new InvalidProgramException("The agregate values are not of the expected type. DateTime");
                return date;
            }
            return null;
        }

        private static DateTimeWrapper AddMaxDate(TreeCore<DateTimeWrapper> core, DateTimeWrapper value)
        {
            object aggregateValue = null;
            if (core.PreAggregations.TryGetValue(1, out aggregateValue))
            {
                var date = (DateTimeWrapper)aggregateValue;
                if (date == null)
                    throw new InvalidProgramException("The agregate values are not of the expected type. DateTime");
                return (date.Value > value.Value) ? date : value;
            }
            return value;
        }

        private static DateTimeWrapper GetMaxDate(TreeCore<DateTimeWrapper> core)
        {
            object aggregateValue = null;
            if (core.PreAggregations.TryGetValue(1, out aggregateValue))
            {
                var date = (DateTimeWrapper)aggregateValue;
                if (date == null)
                    throw new InvalidProgramException("The agregate values are not of the expected type. DateTime");
                return date;
            }
            return null;
        }
    }
}