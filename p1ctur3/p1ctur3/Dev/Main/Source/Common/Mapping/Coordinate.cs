using System;
using System.Windows;
using Telekad.Types;
using Telekad.Utils;

namespace Telekad.Mapping
{
    public class Coordinate : IRangeable
    {
        private const double EndOfTheWorldLongitude = 180;

        private readonly double latitude;
        public double Latitude
        {
            get
            {
                return this.latitude;
            }
        }

        private readonly double longitude;
        public double Longitude
        {
            get
            {
                return this.longitude;
            }
        }

        public Coordinate(double latitude, double longitude)
        {
            if (latitude < -90F || latitude > 90F) throw new ArgumentException("Latitude must be between -90 and 90", "latitude");
            if (longitude < -180F || longitude > 180F) throw new ArgumentException("Longitude must be between -180 and 180", "longitude");

            this.latitude = latitude;
            this.longitude = longitude;

            this.hashCode = ObjectExtensions.ComputeHashCode(null, latitude, longitude);
        }

        public Point ToViewport()
        {
            return TileSystem.CoordinateToViewport(this);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <remarks>
        /// Comparing doubles is very CPU dependent!!!
        /// </remarks>
        /// <param name="lhp">The LHP.</param>
        /// <param name="rhp">The RHP.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Coordinate lhp, Coordinate rhp)
        {
            if (((object)lhp) == null && ((object)rhp) == null)
                return true;
            if (((object)lhp) == null || ((object)rhp) == null)
                return false;
            // TODO: this does not respect the end of the world longitude (180 == -180)
            var llat = (int)Math.Truncate(lhp.latitude * Math.Pow(10, 7));
            var llong = (int)Math.Truncate(lhp.longitude * Math.Pow(10, 7));
            var rlat = (int)Math.Truncate(rhp.latitude * Math.Pow(10, 7));
            var rlong = (int)Math.Truncate(rhp.longitude * Math.Pow(10, 7));
            return llat.Equals(rlat) && llong.Equals(rlong);
        }

        public static bool operator !=(Coordinate c1, Coordinate c2)
        {
            return !(c1 == c2);
        }

        private const string ToStringTemplate = "({0}, {1})";
        public override string ToString()
        {
            return ToStringTemplate.FormatInvariantCulture(this.Latitude, this.Longitude);
        }

        private readonly int hashCode;
        public override int GetHashCode()
        {
            return hashCode;
        }

        public bool Equals(Coordinate other)
        {
            if (this.latitude == other.latitude)
            {
                if (Math.Abs(this.longitude) == EndOfTheWorldLongitude)
                {
                    return Math.Abs(other.longitude) == EndOfTheWorldLongitude;
                }
                return this.Longitude == other.Longitude;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (!(obj is Coordinate)) return false;
            return Equals((Coordinate)obj);
        }

        public IRange RangeOf(int level)
        {
            return TileSystem.CoordinateToQuadKey(this, level + 1);
        }
    }
}
