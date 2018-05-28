using AMKsGear.Core.Trace;
using System.Collections.Generic;

namespace AMKsGear.Core.TraceTools.TestBuilderPattern
{
    public class TestBuilderContext
    {
        internal LocalLogger LocalLogger;
        internal IDictionary<string, bool> AssertionResults { get; }
        internal IList<string> ActionHistory { get; }

        public string Name { get; }

        public TestBuilderContext(string name)
        {
            AssertionResults = new Dictionary<string, bool>();
            ActionHistory = new List<string>();

            Name = name;
        }

        public object Tag { get; set; }
    }
    public class TestBuilderContext<TArg1> : TestBuilderContext
    {
        public TArg1 Arg1 { get; set; }
        public TestBuilderContext(string name) : base(name) { }
    }
    public class TestBuilderContext<TArg1, TArg2> : TestBuilderContext<TArg1>
    {
        public TArg2 Arg2 { get; set; }
        public TestBuilderContext(string name) : base(name) { }
    }
    public class TestBuilderContext<TArg1, TArg2, TArg3> : TestBuilderContext<TArg1, TArg2>
    {
        public TArg3 Arg3 { get; set; }
        public TestBuilderContext(string name) : base(name) { }
    }
    public class TestBuilderContext<TArg1, TArg2, TArg3, TArg4> : TestBuilderContext<TArg1, TArg2, TArg3>
    {
        public TArg4 Arg4 { get; set; }
        public TestBuilderContext(string name) : base(name) { }
    }
    public class TestBuilderContext<TArg1, TArg2, TArg3, TArg4, TArg5> : TestBuilderContext<TArg1, TArg2, TArg3, TArg4>
    {
        public TArg5 Arg5 { get; set; }
        public TestBuilderContext(string name) : base(name) { }
    }
    public class TestBuilderContext<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> : TestBuilderContext<TArg1, TArg2, TArg3, TArg4, TArg5>
    {
        public TArg6 Arg6 { get; set; }
        public TestBuilderContext(string name) : base(name) { }
    }
    public class TestBuilderContext<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> : TestBuilderContext<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>
    {
        public TArg7 Arg7 { get; set; }
        public TestBuilderContext(string name) : base(name) { }
    }
    public class TestBuilderContext<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> : TestBuilderContext<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>
    {
        public TArg8 Arg8 { get; set; }
        public TestBuilderContext(string name) : base(name) { }
    }
}