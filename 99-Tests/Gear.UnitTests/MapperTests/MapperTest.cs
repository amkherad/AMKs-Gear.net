//using ir.amkdp.gear.arch.Modeling.Annotations;
//using ir.amkdp.gear.core.Automation.Object.Mapper;
//using ir.amkdp.gear.core.Automation.Object.Mapper.Annotations;
//using ir.amkdp.gear.core.Trace;

//namespace Gear.UnitTests.MapperTests
//{
//    public class MapperTest
//    {
//        public static void DoTest()
//        {
//            Test.RegisterPrivateLogger(new PrivateLogger());

//            Test.Create<T1, T2>("MapperCast.Test")
//                .Do("Init Args", x =>
//                {
//                    x.Arg1 = new T1
//                    {
//                        Age3 = 32,
//                        Name = "John"
//                    };
//                    x.Arg2 = new T2
//                    {
//                        AgeStr = "4",
//                        Name = "AMK",
//                        Info = "is-null"
//                    };
//                })
//                .Do("Mapping...", x => new SFMapper(MapperContext.Default()).Map(x.Arg2, x.Arg1))
//                .Do("print", x => Logger.Write($"Info: {x.Arg2.Info}"))
//                .TestArg2("Arg2.Name=John", x => x.Name, "John")
//                .TestArg2("Arg2.AgeStr=32", x => x.AgeStr, "32")
//                .TestArg2("Arg2.Age=32", x => x.Age1, 32)
//                .TestArg2("Arg2.Info='John - 32'", x => x.Info, "John - 32")
//                .Done();

//            //Test.Create<T1, T2>("MapperCast.Test")
//            //    .Do("Init Args", x =>
//            //    {
//            //        x.Arg1 = new T1
//            //        {
//            //            Age3 = 32,
//            //            Name = "John"
//            //        };
//            //        x.Arg2 = new T2
//            //        {
//            //            AgeStr = "4",
//            //            Name = "AMK",
//            //            Info = "is-null"
//            //        };
//            //    })
//            //    .Do("Mapping...", x => Mapper.Map(x.Arg2, x.Arg1))
//            //    .Do("print", x => Logger.Write($"Info: {x.Arg2.Info}"))
//            //    .TestArg2("Arg2.Name=John", x => x.Name, "John")
//            //    .TestArg2("Arg2.AgeStr=32", x => x.AgeStr, "32")
//            //    .TestArg2("Arg2.Age=32", x => x.Age1, 32)
//            //    .TestArg2("Arg2.Info='John - 32'", x => x.Info, "John - 32")
//            //    .Done();
//            //
//            //Test.Create<IEnumerable<T2>, List<T1>>("CollectionMapping")
//            //    .Do("Init Args", x =>
//            //    {
//            //        x.Arg2 = new List<T1>
//            //        {
//            //            new T1
//            //            {
//            //                Age3 = 1,
//            //                Name = "AMK1"
//            //            },
//            //            new T1
//            //            {
//            //                Age3= 2,
//            //                Name = "AMK2"
//            //            }
//            //        };
//            //    })
//            //    .Do("Mapping...", x =>
//            //    {
//            //        IEnumerable<T2> val = null;
//            //        Mapper.Map(ref val, x.Arg2);
//            //        x.Arg1 = val;
//            //    })
//            //    .Do("print", x => Logger.Write(string.Format($"Arg1.Count: {{0}}, Arg2.Count: {x.Arg2.Count}", x.Arg1.Count())))
//            //    .TestArg1("Arg1.Count=2", x => x.Count(), 2)
//            //    .TestArg1("Arg1[0].Age1", x => x.First().Age1, 1)
//            //    .TestArg1("Arg1[1].Age1", x => x.Skip(1).First().Age1, 2)
//            //    .Done();

//            //Logger.Write(t2.Name);
//            //Logger.Write(t2.Age2.Value);
//        }
//    }

//    class T1
//    {
//        public string Name { get; set; }
//        [Name("Age")] public int? Age3;
//    }

//    class T2
//    {
//        public string Name { get; set; }

//        [Name("Age")] [MapperCastToString(typeof (int))] public string AgeStr;

//        [Name("Age")]
//        public int Age1 { get; set; }

//        [MCast]
//        public string Info { get; set; }
//    }

//    public class MCast : MapperEvaluateAttribute
//    {
//        public MCast()
//            : base(typeof (T1), _convert)
//        {
//            //PassObject = true;
//        }

//        private static object _convert(object arg)
//        {
//            var t1 = arg as T1;
//            if (t1 == null) return null;

//            return $"{t1.Name} - {t1.Age3}";
//        }
//    }
//}