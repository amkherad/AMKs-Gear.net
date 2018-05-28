using System;
using System.Linq.Expressions;
using ir.amkdp.gear.arch.AssemblyScope.AssemblyLocalization;
using ir.amkdp.gear.arch.Trace.Annotations;
using ir.amkdp.gear.core.Linq.Expressions;
using ir.amkdp.gear.core.Trace;

namespace Gear.UnitTests.Core
{
    [TestClass]
    public class LinqTests
    {
        [TestMethod]
        public void TestExpressionGetMethodName()
        {
            Expression<Func<IActionResultLocalization, object>> x = tx => tx.Failure;
            var methodName = x.GetMemberName();
            //Debug.Write($"{nameof(TestExpressionGetMethodName)}->{nameof(methodName)}: {methodName}");
            Assert.AreEqual(methodName, nameof(IActionResultLocalization.Failure));
        }
    }
}
