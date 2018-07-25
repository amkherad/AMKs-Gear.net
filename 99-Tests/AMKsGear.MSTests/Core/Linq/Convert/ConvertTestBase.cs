using System;
using System.Linq.Expressions;
using AMKsGear.Core.Linq.Convert;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMKsGear.MSTests.Core.Linq.Convert
{
    public class ConvertTestBase : ExpressionTestBase
    {
        public Func<TParam, TResult> CreateConvertHelper<TParam, TResult>(ITypeConvertHelper convert)
        {
            var source = Expression.Parameter(typeof(TParam));
            Assert.IsTrue(convert.CanConvert(source.Type));

            var expression = convert.CreateInlineConvertExpression(source);

            return CreateExpression<TParam, TResult>(expression, source).Compile();
        }
    }
}