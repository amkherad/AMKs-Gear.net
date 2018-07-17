using System;

namespace AMKsGear.Architecture
{
    public static class ConstantTable
    {
        public const AttributeTargets AllMembers = AttributeTargets.Event | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property;
        public const AttributeTargets AllValueMembers = AttributeTargets.Field | AttributeTargets.Property;
    }
}