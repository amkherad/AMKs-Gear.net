using System;
using System.Collections.Generic;
using System.Diagnostics;
using Gear.UnitTests.TypeResolverTests.Models;
using ir.amkdp.gear.arch.Trace.Annotations;
using Container = SimpleInjector.Container;

namespace Gear.UnitTests.TypeResolverTests.SimpleInjector
{
    [TestClass]
    public class SimpleInjectorTest
    {
        [TestMethod]
        public void BatchResolvePerformanceTest()
        {
            var container = new Container();

            container.Register(typeof(ICoffeeMaker), typeof(CoffeeMaker));
            container.Register(typeof(ICoffee), typeof(FrenchCoffee));
            container.Register(typeof(IEnumerable<ISundry>), () => new ISundry[]
            {
                new CoffeeMilk(), new CoffeeCream(), 
            });
            
            var st = Stopwatch.StartNew();

            for (var i = 0; i < 500000; i++)
            {
                var instance = container.GetInstance<ICoffeeMaker>();
                GC.KeepAlive(instance);
            }

            st.Stop();

            Trace.WriteLine($"Ellaped: {st.ElapsedMilliseconds}");
        }
    }
}