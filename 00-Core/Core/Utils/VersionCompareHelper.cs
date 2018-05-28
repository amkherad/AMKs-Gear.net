using System;
using AMKsGear.Architecture;

namespace AMKsGear.Core.Utils
{
    public static class VersionCompareHelper
    {
        public static bool CheckVersion(string version, Version versionToBeSatisfied,
            Compare compare, Version epsilon = null)
            => CheckVersion(Version.Parse(version), versionToBeSatisfied, compare, epsilon);
        public static bool CheckVersion(Version version, Version versionToBeSatisfied, Compare compare, Version epsilon = null)
        {
            var result = version.CompareTo(versionToBeSatisfied);

            switch (compare)
            {
                case Compare.Equal:
                    return result == 0;
                case Compare.Lesser:
                    return result < 0;
                case Compare.Greater:
                    return result > 0;
                case Compare.LesserEqual:
                    return result <= 0;
                case Compare.GreaterEqual:
                    return result >= 0;
                case Compare.NotEqual:
                    return result != 0;
                case Compare.Near:
                    return version.Major == versionToBeSatisfied.Major;
                default:
                    throw new ArgumentOutOfRangeException(nameof(compare), compare, null);
            }
        }
    }
}