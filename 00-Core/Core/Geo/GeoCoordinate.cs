//using System;
//using AMKsGear.Architecture;
//using AMKsGear.Core.Utils;

//namespace AMKsGear.Core.Geo
//{
//    public abstract class GeoCoordinate : IComparable<IGeoCoordinate>, IGeoCoordinate, ICloneable
//    {
//        public double Latitude { get; protected set; }
//        public double Longitude { get; protected set; }

//        protected DoubleComparer InternalComparer;
//        public DoubleComparer Comparer { get { return InternalComparer ?? DoubleComparer.Default; } set { InternalComparer = value; } }

//        public GeoCoordinate() { }

//        public int CompareTo(IGeoCoordinate other)
//        {
//            if (other == null) throw new ArgumentNullException(nameof(other));
//            var thisLat = Latitude;
//            var thisLon = Longitude;
//            var thatLat = other.Latitude;
//            var thatLon = other.Longitude;

//            var latCompare = InternalComparer.Compare(thisLat, thatLat);
//            var lonCompare = InternalComparer.Compare(thisLon, thatLon);

//            if (latCompare == CompareResult.Equal && lonCompare == CompareResult.Equal)
//                return 0;

//            return 1;
//        }


//        public override int GetHashCode()
//        {
//            unchecked
//            {
//                return Latitude.GetHashCode() + Longitude.GetHashCode();
//            }
//        }

//        public abstract object Clone();

//        public override bool Equals(object obj)
//        {
//            var that = obj as IGeoCoordinate;
//            if (that == null) return false;

//            return InternalComparer.Compare(Latitude, that.Latitude) == CompareResult.Equal &&
//                   InternalComparer.Compare(Longitude, that.Longitude) == CompareResult.Equal;
//        }

//        public static bool operator ==(GeoCoordinate a, GeoCoordinate b)
//        {
//            if (ReferenceEquals(a, b)) return true;
//            if (((object)a == null) || ((object)b == null)) return false;
//            return a.Equals(b);
//        }
//        public static bool operator !=(GeoCoordinate a, GeoCoordinate b)
//        { return !(a == b); }
//    }

//    public class LatLon : GeoCoordinate
//    {
//        public LatLon(double latitude, double longitude)
//        {
//            Latitude = latitude;
//            Longitude = longitude;
//        }
//        public override object Clone()
//        {
//            return new LatLon(Latitude, Longitude);
//        }

//        public override string ToString()
//        {
//            return $"{Latitude}, {Longitude}";
//        }
//    }
//    public class LonLat : GeoCoordinate
//    {
//        public LonLat(double longitude, double latitude)
//        {
//            Longitude = longitude;
//            Latitude = latitude;
//        }
//        public override object Clone()
//        {
//            return new LonLat(Longitude, Latitude);
//        }

//        public override string ToString()
//        {
//            return $"{Longitude}, {Latitude}";
//        }
//    }
//}