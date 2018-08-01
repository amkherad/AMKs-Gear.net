//using AMKsGear.Core.TraceTools.TestBuilderPattern;
//
//namespace AMKsGear.Core.TraceTools
//{
//    public static partial class Test
//    {
//        #region TestBuilderPattern
//        public static TestBuilderContext Create(string name)
//            => new TestBuilderContext(name);
//        public static TestBuilderContext<TArg1> Create<TArg1>(string name)
//            => new TestBuilderContext<TArg1>(name);
//        public static TestBuilderContext<TArg1, TArg2> Create<TArg1, TArg2>(string name)
//            => new TestBuilderContext<TArg1, TArg2>(name);
//        public static TestBuilderContext<TArg1, TArg2, TArg3> Create<TArg1, TArg2, TArg3>(string name)
//            => new TestBuilderContext<TArg1, TArg2, TArg3>(name);
//        public static TestBuilderContext<TArg1, TArg2, TArg3, TArg4> Create<TArg1, TArg2, TArg3, TArg4>(string name)
//            => new TestBuilderContext<TArg1, TArg2, TArg3, TArg4>(name);
//        #endregion
//    }
//}