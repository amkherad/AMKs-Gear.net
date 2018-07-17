using System;
using AMKsGear.Architecture;
using AMKsGear.Core.Text;

namespace AMKsGear.Core.Automation.Object.Mapper.Annotations
{
    [AttributeUsage(ConstantTable.AllValueMembers)]
    public class MapperCastAttribute : Attribute
    {
        internal readonly Type InternalSource;
        internal readonly Func<object, object> InternalCastExpression;

        internal bool InternalPassNulls;
        public Type Source => InternalSource;
        public Func<object, object> CastExpression => InternalCastExpression;
        public bool PassNulls { get { return InternalPassNulls; } set { InternalPassNulls = value; } }

        public MapperCastAttribute(Func<object, object> castExpression)
        {
            //Source = null;
            InternalCastExpression = castExpression;
        }
        public MapperCastAttribute(Type source, Func<object, object> castExpression)
        {
            InternalSource = source;
            InternalCastExpression = castExpression;
        }
    }

    #region Implementations
    public class MapperCastToStringAttribute : MapperCastAttribute
    {
        public MapperCastToStringAttribute(Type source) : base(source, x => x?.ToString()) { }
        public MapperCastToStringAttribute() : base(null, x => x?.ToString()) { }
    }

    public class MapperCastStringToDoubleAttribute : MapperCastAttribute
    {
        public MapperCastStringToDoubleAttribute() : base(typeof(string), x => (x as string)?.ToDouble(0d)) { }
    }
    public class MapperCastStringToFloatAttribute : MapperCastAttribute
    {
        public MapperCastStringToFloatAttribute() : base(typeof(string), x => (x as string)?.ToFloat(0f)) { }
    }
    public class MapperCastStringToInt32Attribute : MapperCastAttribute
    {
        public MapperCastStringToInt32Attribute() : base(typeof(string), x => (x as string)?.ToInt32(0)) { }
    }
    public class MapperCastStringToInt64Attribute : MapperCastAttribute
    {
        public MapperCastStringToInt64Attribute() : base(typeof(string), x => (x as string)?.ToInt64(0L)) { }
    }
    public class MapperCastStringToDecimalAttribute : MapperCastAttribute
    {
        public MapperCastStringToDecimalAttribute() : base(typeof(string), x => (x as string)?.ToDecimal(0M)) { }
    }
    public class MapperCastStringToUInt32Attribute : MapperCastAttribute
    {
        public MapperCastStringToUInt32Attribute() : base(typeof(string), x => (x as string)?.ToUInt32(0U)) { }
    }
    public class MapperCastStringToUInt64Attribute : MapperCastAttribute
    {
        public MapperCastStringToUInt64Attribute() : base(typeof(string), x => (x as string)?.ToUInt64(0UL)) { }
    }
    #endregion
}