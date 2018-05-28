using System.Collections.Generic;
using System.Linq;
using ir.amkdp.gear.arch.Data.Types;
using ir.amkdp.gear.core.Linq.Expressions.StringExpressions;
using ir.amkdp.gear.core.Trace;

namespace Gear.WinTests.ExpressionTests
{
    public class ExpressionTest
    {
        public static void Test()
        {
            //var list = new[]
            //{
            //    "ali",
            //    "aMk",
            //    "Ali",
            //    "Kherad",
            //    "talion",
            //    "ali mousavi",
            //    "alian",
            //    "Test",
            //    "aloo",
            //}.AsQueryable();
            var list = new TestClass[]
            {
                new TestClass{ Prop = "ali" },
                new TestClass{ Prop = "aMk" },
                new TestClass{ Prop = "Ali" },
                new TestClass{ Prop = "Kherad" },
                new TestClass{ Prop = "talion" },
                new TestClass{ Prop = "ali mousavi" },
                new TestClass{ Prop = "alian" },
                new TestClass{ Prop = "Test" },
                new TestClass{ Prop = "aloo" },
            }.AsQueryable();

            var filter = StringExpression.GetInstanceStringCompareExpression<TestClass>("Prop", "li", StringCompare.Contains,
                null);

            var result = list.Where(filter).Select(x=>x.Prop);

            Logger.Write((IEnumerable<string>)result);
            Logger.Write("---------------");
        }
    }

    internal class TestClass
    {
        public string Prop { get; set; }
    }
}