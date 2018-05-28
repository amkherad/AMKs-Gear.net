using System;

namespace AMKsGear.Architecture
{
    public enum RelativeOrder
    {
        Before,
        After,
    }
    public enum SortingOrder
    {
        Ascending = 0,
        Descending = 1,
        Unspecified
    }
    public enum CompareResult
    {
        Equal,
        Lesser,
        Greater,
        NotEqual
    }
    public enum Compare
    {
        Equal,
        Lesser,
        Greater,
        LesserEqual,
        GreaterEqual,
        NotEqual,

        Near
    }
    public enum CacheMode
    {
        Cache,
        NoCache,
        Default = NoCache
    }

    public class ConstantTable
    {
        public const AttributeTargets AllMembers = AttributeTargets.Event | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property;
        public const AttributeTargets AllProperties = AttributeTargets.Field | AttributeTargets.Property;
    }
}