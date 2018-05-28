using System;
using System.Collections.Generic;
using System.Diagnostics;
using Gear.UnitTests.TypeResolverTests.Models;
using ir.amkdp.gear.arch.Automation.IoC.Annotations;
using ir.amkdp.gear.arch.Data;
using ir.amkdp.gear.arch.LifetimeManagers;
using ir.amkdp.gear.arch.Modeling.Annotations;
using ir.amkdp.gear.arch.Trace.Annotations;
using ir.amkdp.gear.core.Automation.IoC;
using ir.amkdp.gear.core.Trace;
using ir.amkdp.gear.data.RawWrapper;
using ir.amkdp.gear.desktop.win.Trace;

namespace Gear.UnitTests.TypeResolverTests
{
    public class Entity : IIdEntity<int>
    {
        public bool IsEvaluated => Id != 0;
        public int Id { get; set; }
    }
    [TestClass]
    public class TypeResolverTest
    {
        [TestMethod]
        public void Test()
        {
            Logger.RegisterLogger(new TraceLogger());
            var c = new TypeResolverContainer();
            TypeResolver.SetWideResolver(c);

            //c.RegisterSingleton()
            //    .For<ISingleType>()
            //    .With(new SingleType())
            //    .Register();

            //c.RegisterType(typeof(ISig1), new Singleton());
            //c.RegisterType(typeof(ISig2), new Singleton2());
            //c.RegisterType(typeof(IRawWrapper), new RawWrapper());
            //c.RegisterType(typeof(IQueryable<Entity>), (new List<Entity>(new[] { new Entity { Id = 1 }, })).AsQueryable());
            //c.RegisterType(typeof(ICrudService<Entity, CrudOptions<Entity>>), typeof(QueryableCrudService<Entity, CrudOptions<Entity>>));

            //var crud = TypeResolver.CreateInstance<ICrudService<Entity, CrudOptions<Entity>>>();
            //var result = crud.GetAll();
            
            //c.RegisterType<TT>();
            //new ConstantNamedValue("@#% my text of world:)", "jason stt")

            //Logger.RegisterLogger(IoCNamespaceOptions.LoggerCategory, new MethodLogger(Logger.Write, Logger.Feed));
            //
            //c.RegisterType<IFromType, ToType>(
            //    new ConstantNamedValue("value1", 22),
            //    new ConstantNamedValue("jackson"),
            //    new ValueProvider((dt, t, name) =>
            //    {
            //        if (name == "testii") return "Hello world";
            //        return null;
            //    },
            //    (dt, t, name) => name == "testii"));
            //c.RegisterType(typeof(ISig1), new Singleton
            //{
            //    Action = "SEXY?"
            //});

            c.RegisterType(typeof (ViewModel));
            c.RegisterType(typeof (View));
            //c.BindProperty<View, ViewModel>(x => x.VM);

            var st = Stopwatch.StartNew();
            var instance = c.Resolve<View>();
            //var result = TypeResolver.CreateInstance<IFromType>();
            //new ConstantTypedValue(typeof(ISig1), new Singleton()
            //{
            //    Action = "SEXY?"
            //}), new ConstantTypedValue(typeof(ISig2), null)
            st.Stop();
            instance.Check();
            //result.XX();
            //var sig = TypeResolver.CreateInstance<ISig1>();
            //Logger.Write($"{sig.Action}");

            //Logger.Write($"Ellapsed time was: {st.ElapsedMilliseconds}");
            //Assert.AreEqual(methodName, nameof(IActionResultLocalization.Failure));
            Trace.Write($"Ellapsed time was: {st.ElapsedMilliseconds}");
        }

        [TestMethod]
        public void SingletonRegistrar()
        {
            var container = new TypeResolverContainer();

            container.RegisterType(typeof(ICoffeeMaker), typeof(CoffeeMaker));
            container.RegisterType(typeof(ICoffee), typeof(FrenchCoffee));
            container.RegisterType(typeof(IEnumerable<ISundry>), () => new ISundry[]
            {
                new CoffeeMilk(), new CoffeeCream(),
            }, true);

            var st = Stopwatch.StartNew();

            for (var i = 0; i < 500000; i++)
            {
                var instance = container.Resolve<ICoffeeMaker>();
                GC.KeepAlive(instance);
            }
            //var result = TypeResolver.CreateInstance<IFromType>();
            //new ConstantTypedValue(typeof(ISig1), new Singleton()
            //{
            //    Action = "SEXY?"
            //}), new ConstantTypedValue(typeof(ISig2), null)
            st.Stop();

            Trace.WriteLine($"Ellaped: {st.ElapsedMilliseconds}");
        }
    }

    public class ViewModel
    {
        public string Name { get; set; }

        public ViewModel()
        {
            Name = "ali";
        }
    }
    public class View : IView
    {
        public object VM { get; set; }

        public void Check()
        {
            if (VM == null)
            {
                Logger.Write("VM is null", styles: Logger.ErrorStyle);
            }
            else
            {
                Logger.Write("VM isnot null name:" + (VM as ViewModel).Name, styles: Logger.SuccessStyle);
            }
        }
    }

    public interface IView
    {
        
    }

    public interface IFromType
    {
        void XX();
    }

    public class RawWrapper : IRawWrapper
    {
        public object GetUnderlyingContext()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public IRawWrapperMetaData Meta { get; }
        public ITransaction BeginTransaction()
        {
            throw new System.NotImplementedException();
        }
    }

    public class JJ
    {
        
    }
    public class TT
    {
        public TT( JJ j
            //[Name("@#% my text of world:)")]string name,
            //[Name("@#% my text of world:)")]string text
            )
        {
            //Name = name;
            //Text = text;
        }

        public string Name { get; }
        public string Text { get; }
    }

    public interface ISig1 { string Action { get; } }
    public interface ISig2 { string Action { get; } }
    public class Singleton : ISig1
    {
        public string Action { get; set; } = "TestingNow:";
    }
    public class Singleton2 : ISig2
    {
        public string Action => "SIG22:";
    }
    public class ToType : IFromType
    {
        public int Value { get; }

        [Name("value1")]
        [ResolveValue]
        public int Age { get; }

        [Name("testii")]
        [ResolveValue]
        public string HSDF { get; }

        public TT TT { get; }

        public ISig1 Singleton { get; }
        public ISig2 Singleton2 { get; }

        public ToType(
            [Name("value1")]                int test,
            [ResolveDefaultValue]           int papa,
                                            int jackson,
            [Name("testii")]                string hsdf,
                                            TT tt,
                                            ISig1 singleton,
            [ResolveDefaultValue]           ISig2 singleton2)
        {
            Value = test;
            HSDF = hsdf;
            TT = tt;
            Singleton = singleton;
            Singleton2 = singleton2;
        }

        public void XX()
        {
            Logger.Write($"XX called: Value:{Value}, Age:{Age}, HSDF:{HSDF}, TT.Name={TT.Name}, TT.Text={TT.Text}, Singleton.Action={Singleton.Action}", styles: Logger.SuccessStyle);
        }
    }

    public interface ISingleType
    {
        string VProv { get; }
    }
    public class SingleType : ISingleType
    {
        public string VProv => "A.M.K.";
    }
}