using System.Collections.Generic;
using AMKsGear.Architecture;
using SMath = System.Math;

namespace AMKsGear.Core.Utils
{
    public class FloatComparer : IComparer<float>, IEqualityComparer<float>
    {
        public static readonly FloatComparer Default = new FloatComparer();
        private readonly float _epsilon;

        private FloatComparer()
        {
            _epsilon = float.Epsilon;
        }
        public FloatComparer(float tolerance)
        {
            _epsilon = tolerance;
        }
        

        public CompareResult Compare(float d1, float d2)
        {
            if (SMath.Abs(d1 - d2) <= _epsilon) return CompareResult.Equal;
            var diff = d1 - d2;
            if (diff < 0) return CompareResult.Greater;
            if (diff > 0) return CompareResult.Lesser;
            return CompareResult.NotEqual;
        }

        int IComparer<float>.Compare(float d1, float d2)
        {
            if (SMath.Abs(d1 - d2) <= _epsilon) return 0;
            var diff = d1 - d2;
            if (diff < 0) return 1;
            if (diff > 0) return -1;
            return 0;
        }

        public bool Equals(float d1, float d2) => SMath.Abs(d1 - d2) <= _epsilon;
        public int GetHashCode(float obj) => obj.GetHashCode();
    }
    public class DoubleComparer : IComparer<double>, IEqualityComparer<double>
    {
        public static readonly DoubleComparer Default = new DoubleComparer();
        private readonly double _epsilon;

        private DoubleComparer()
        {
            _epsilon = double.Epsilon;
        }
        public DoubleComparer(double tolerance)
        {
            _epsilon = tolerance;
        }
        
        public CompareResult Compare(double d1, double d2)
        {
            if (SMath.Abs(d1 - d2) <= _epsilon) return CompareResult.Equal;
            var diff = d1 - d2;
            if (diff < 0) return CompareResult.Greater;
            if (diff > 0) return CompareResult.Lesser;
            return CompareResult.NotEqual;
        }

        int IComparer<double>.Compare(double d1, double d2)
        {
            if (SMath.Abs(d1 - d2) <= _epsilon) return 0;
            var diff = d1 - d2;
            if (diff < 0) return 1;
            if (diff > 0) return -1;
            return 0;
        }

        public bool Equals(double d1, double d2) => SMath.Abs(d1 - d2) <= _epsilon;
        public int GetHashCode(double obj) => obj.GetHashCode();
    }
}