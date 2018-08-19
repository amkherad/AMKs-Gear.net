using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AMKsGear.Core.Automation.Reflection;

namespace AMKsGear.Core.Linq.Convert
{
    public class CollectionConvertHelper : ITypeConvertHelper
    {
        /// <summary>
        /// This value is not thread-safe. (doesn't matter)
        /// </summary>
        private static MethodInfo CountMethod;


        public static Expression CreateLengthExpression(Expression source, bool allowEnumerableCount)
        {
            if (source.Type.IsArray)
                return ArrayConvertHelper.CreateLengthExpression(source);

            var countProperty = source.Type.GetProperty(nameof(ICollection.Count));
            if (countProperty != null)
            {
                return Expression.Property(source, countProperty); //source.Count
            }

            if (!allowEnumerableCount)
            {
                return null;
            }

            if (CountMethod == null)
            {
                CountMethod = typeof(Enumerable).GetMethods()
                    .First(m => m.Name == nameof(Enumerable.Count) && m.GetParameters().Length == 1);
            }

            var countMethod = CountMethod.MakeGenericMethod(source.Type.GetEnumerableBaseType());

            return Expression.Call(countMethod, source); //list.Count()
        }


        public bool CanConvert(Type type)
        {
            throw new NotImplementedException();
        }

        public Expression CreateInlineConvertExpression(Expression source, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public Expression CreateInlineConvertExpressionQueryableSafe(Expression source, Type destinationType)
        {
            throw new NotImplementedException();
        }
    }
}