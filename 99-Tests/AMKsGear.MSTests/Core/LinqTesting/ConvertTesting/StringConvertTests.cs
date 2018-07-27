using AMKsGear.Core.Linq.Convert;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMKsGear.MSTests.Core.LinqTesting.ConvertTesting
{
    [TestClass]
    public class StringConvertTests : ConvertTestBase
    {
        [TestMethod]
        public void TestIntConstantToLongConvert()
        {
            var func = CreateConvertHelper<int, string>(new StringConvertHelper());
            
            Assert.AreEqual("10", func(10));
            Assert.AreEqual("20", func(20));
            Assert.AreNotEqual("10", func(20));
        }
        
    }
}