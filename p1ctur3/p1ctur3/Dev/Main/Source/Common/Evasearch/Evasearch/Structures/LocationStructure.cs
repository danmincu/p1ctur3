using System;
using Evasearch.Structures;
using Evasearch.Trees;
using Evasearch.Trees;
using Telekad.Mapping;

namespace Evasearch.Structures
{
    public class LocationStructure : RangeStructure<Coordinate>
    {
        public LocationStructure(int maxLevel)
            : base(maxLevel)
        {
            this.RegisterFunction("Average", AddCoords, AverageCoords);
        }

        private static Tuple<double, double> AddCoords(TreeCore<Coordinate> core, Coordinate value)
        {
            object aggregateValues;
            if (!core.PreAggregations.TryGetValue(0, out aggregateValues))
                return new Tuple<double, double>(value.Latitude, value.Longitude);
            var tuple = aggregateValues as Tuple<double, double>;
            if (tuple == null)
                throw new InvalidProgramException("The agregate values are not of the expected type. Tuple<double, double>");
            return new Tuple<double, double>(tuple.Item1 + value.Latitude, tuple.Item2 + value.Longitude);
        }

        private static Coordinate AverageCoords(TreeCore<Coordinate> core)
        {
            object aggregateValues;
            if (!core.PreAggregations.TryGetValue(0, out aggregateValues)) return null;
            var tuple = aggregateValues as Tuple<double, double>;
            if (tuple == null)
                throw new InvalidProgramException("The agregate values are not of the expected type. Tuple<double, double>");
            return new Coordinate(tuple.Item1 / core.Count, tuple.Item2 / core.Count);
        }
    }
}