using System.Diagnostics;
using ir.amkdp.gear.arch.Trace.Annotations;
using ir.amkdp.gear.core.Trace;
using ir.amkdp.gear.core.Trace.PerObjectTracing;
using ir.amkdp.gear.desktop.win.Trace;

namespace Gear.UnitTests.Core
{
    [TestClass]
    public class TraceTests
    {
        [TestMethod]
        public void Test()
        {
            var obj = new object();
            
            PerObjectTrace.SetPerObjectLogEngine(obj, new ConsoleLogger());
//            PerObjectTrace.SetPerObjectLogCategories(obj, "sql", LogCategoryOverrideStrategy.OnlyOverride);
//            PerObjectTrace.SetPerObjectLogCategories(obj, "clr", LogCategoryOverrideStrategy.OnlyOverride);
//            PerObjectTrace.SetPerObjectLogCategories(obj, "win", LogCategoryOverrideStrategy.OnlyOverride);
//            PerObjectTrace.SetPerObjectLogCategories(obj, "detector", LogCategoryOverrideStrategy.OnlyOverride);
            
            Logger.WritePrivateLog(obj, "Hello");
        }

        [TestMethod]
        public void DumpStackTrace()
        {
            int xx = 34;
            int y = 3;
            _call();
        }

        private void _call()
        {
            int t = 34;
            var j = "ali";

            new StackTrace().DumpStackTrace(new LocalLogger());
        }
    }
}