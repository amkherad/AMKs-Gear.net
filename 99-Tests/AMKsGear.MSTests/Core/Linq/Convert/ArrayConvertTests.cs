using System;
using System.Linq.Expressions;
using AMKsGear.Core.Linq.Convert;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMKsGear.MSTests.Core.Linq.Convert
{
    [TestClass]
    public class ArrayConvertTests : ConvertTestBase
    {
        [TestMethod]
        public void TestCreateCopyArrayExpression0()
        {
            var src = new[]
            {
                10, 20, 65, 77, 23
            };
            var dest = new int[6];

            var srcParam = Expression.Parameter(typeof(int[]));
            var destParam = Expression.Parameter(typeof(int[]));
            var exp = ArrayConvertHelper.CreateCopyArrayExpression(srcParam, destParam);
            var act = Expression.Lambda<Action<int[], int[]>>(exp, srcParam, destParam).Compile();
            act(src, dest);
            
            Assert.AreEqual(10, dest[0]);
            Assert.AreEqual(20, dest[1]);
            Assert.AreEqual(65, dest[2]);
            Assert.AreEqual(77, dest[3]);
            Assert.AreEqual(23, dest[4]);
            Assert.AreEqual(0, dest[5]);
        }
        
        [TestMethod]
        public void TestCreateCopyArrayExpression1()
        {
            var src = new[]
            {
                10, 20, 65, 77, 23
            };
            var dest = new int[2];

            var srcParam = Expression.Parameter(typeof(int[]));
            var destParam = Expression.Parameter(typeof(int[]));
            var exp = ArrayConvertHelper.CreateCopyArrayExpression(srcParam, destParam);
            var act = Expression.Lambda<Action<int[], int[]>>(exp, srcParam, destParam).Compile();
            act(src, dest);
            
            Assert.AreEqual(10, dest[0]);
            Assert.AreEqual(20, dest[1]);
        }
        
    }
}