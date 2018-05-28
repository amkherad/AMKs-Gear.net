using System;
using System.Diagnostics;
using Gear.UnitTests.TypeResolverTests.Models;
using ir.amkdp.gear.arch.Trace.Annotations;

namespace Gear.UnitTests.TypeResolverTests.RawCode
{
    [TestClass]
    public class ResolveWithNewTest
    {
        [TestMethod]
        public void BatchNewPerformanceTest()
        {
            var st = Stopwatch.StartNew();

            for (var i = 0; i < 500000; i++)
            {
                var coffeeMaker = new CoffeeMaker(new FrenchCoffee(), null);
                
                GC.KeepAlive(coffeeMaker);
            }

            st.Stop();

            Trace.WriteLine($"Ellaped: {st.ElapsedMilliseconds}");
        }
    }
}