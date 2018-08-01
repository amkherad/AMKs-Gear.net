using System;
using System.Linq.Expressions;
using System.Threading;

namespace AMKsGear.Core.Linq.Convert
{
    public class StringConvertHelper : ITypeConvertHelper
    {
        private static StringConvertHelper _instance;

        /// <summary>
        /// Default singleton instance.
        /// </summary>
        public static StringConvertHelper Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                return LazyInitializer.EnsureInitialized(ref _instance);
            }
        }
        
        
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

        public Expression CreateInlineConvertExpression(Expression source, Type destinationType)
        {
            var sourceType = source.Type;
            
            if (sourceType == typeof(string))
            {
                return source;
            }
            
            source = Expression.Call(source, sourceType.GetMethod(nameof(object.ToString), Type.EmptyTypes));

            return source;
        }

        public Expression CreateInlineConvertExpressionQueryableSafe(Expression source, Type destinationType)
        {
            var sourceType = source.Type;

            if (sourceType == typeof(string))
            {
                return source;
            }
            
            source = Expression.Call(source, sourceType.GetMethod(nameof(object.ToString), Type.EmptyTypes));

            return source;
        }
    }
}