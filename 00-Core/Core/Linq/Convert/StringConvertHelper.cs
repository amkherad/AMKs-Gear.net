using System;
using System.Linq.Expressions;

namespace AMKsGear.Core.Linq.Convert
{
    public class StringConvertHelper : ITypeConvertHelper
    {
        public bool AllowToString { get; }


        public StringConvertHelper()
        {
            AllowToString = false;
        }
        public StringConvertHelper(bool allowToString)
        {
            AllowToString = allowToString;
        }
        
        
        public bool CanConvert(Type type)
        {
            if (AllowToString) //unlikely
            {
                return true;
            }

            return type == typeof(string) ||
                   type.IsPrimitive ||
                   type == typeof(decimal);
        }

        public Expression CreateInlineConvertExpression(Expression source)
        {
            if (source.Type == typeof(string))
            {
                return source;
            }
            
            source = Expression.Call(source, source.Type.GetMethod("ToString", Type.EmptyTypes));

            return source;
        }

        public Expression CreateInlineConvertExpressionQueryableSafe(Expression source)
        {
            if (source.Type == typeof(string))
            {
                return source;
            }
            
            source = Expression.Call(source, source.Type.GetMethod("ToString", Type.EmptyTypes));

            return source;
        }


        public Expression CreateInlineConvertExpression(Expression source, Type fromType)
        {
        }
    }
}