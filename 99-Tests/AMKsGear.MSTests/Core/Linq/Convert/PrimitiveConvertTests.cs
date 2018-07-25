using System;
using System.Linq.Expressions;
using AMKsGear.Core.Linq.Convert;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMKsGear.MSTests.Core.Linq.Convert
{
    [TestClass]
    public class PrimitiveConvertTests : ConvertTestBase
    {
        [TestMethod]
        public void TestIntConstantToLongConvert()
        {
            var func = CreateConvertHelper<int, long>(new Int64ConvertHelper());
            
            Assert.AreEqual(10, func(10));
            Assert.AreEqual(20, func(20));
            Assert.AreNotEqual(10, func(20));
        }
        
        
    }
}