using AMKsGear.Core.Math.Types;
using AMKsGear.Core.Utils;

namespace AMKsGear.Core.Math.M2D
{
    public static class LineHelper
    {
        /// <summary>
        /// Is BC inline with AC or visa-versa.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static bool IsInLine(Int32Point p1, Int32Point p2, Int32Point p3)
        {
            // if AC is horizontal
            if (p1.X == p3.X) return p2.X == p3.X;
            // if AC is vertical.
            if (p1.Y == p3.Y) return p2.Y == p3.Y;
            // match the gradients
            return (p1.X - p3.X) * (p1.Y - p3.Y) == (p3.X - p2.X) * (p3.Y - p2.Y);
        }
        public static bool IsInLine(Int64Point p1, Int64Point p2, Int64Point p3)
        {
            // if AC is horizontal
            if (p1.X == p3.X) return p2.X == p3.X;
            // if AC is vertical.
            if (p1.Y == p3.Y) return p2.Y == p3.Y;
            // match the gradients
            return (p1.X - p3.X) * (p1.Y - p3.Y) == (p3.X - p2.X) * (p3.Y - p2.Y);
        }
        public static bool IsInLine(FloatPoint p1, FloatPoint p2, FloatPoint p3, FloatComparer comparer = null)
        {
            var fComparer = comparer ?? FloatComparer.Default;
            // if AC is horizontal
            if (fComparer.Equals(p1.X, p3.X))
                return fComparer.Equals(p2.X, p3.X);
            // if AC is vertical.
            if (fComparer.Equals(p1.Y, p3.Y))
                return fComparer.Equals(p2.Y, p3.Y);
            // match the gradients
            return fComparer.Equals((p1.X - p3.X) * (p1.Y - p3.Y), (p3.X - p2.X) * (p3.Y - p2.Y));
        }
    }
}