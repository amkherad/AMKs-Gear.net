using System;
using AMKsGear.Architecture.Automation.IoC;

namespace AMKsGear.Core.TraceTools.Mocking.Faking
{
    public static class FakingManager
    {
        public static Type CreateFakeResultTypeBuilder<TFake>() => CreateFakeResultTypeBuilder(typeof (TFake));
        public static Type CreateFakeResultTypeBuilder(Type realType)
        {
            return null;
        }

        public static object CreateFakeResultBuilder(ITypeResolver typeResolver, Type realType)
            => typeResolver.Resolve(CreateFakeResultTypeBuilder(realType), null, null);

        public static Func<object> CreateFakeResultDelegateBuilder(ITypeResolver typeResolver, Type realType)
            => () => typeResolver.Resolve(CreateFakeResultTypeBuilder(realType), null, null);
        public static Func<Type, object> CreateFakeResultDelegateBuilder(ITypeResolver typeResolver)
            => x => typeResolver.Resolve(CreateFakeResultTypeBuilder(x), null, null);

        public static void RegisterFakeResult<TFrom, TTo>(this ITypeResolverContainer container, params object[] options)
            => RegisterFakeResult(container, typeof(TFrom), typeof (TTo), options);
        public static void RegisterFakeResult(this ITypeResolverContainer container, Type fromType, Type toType, params object[] options)
        {
            container.RegisterType(fromType, CreateFakeResultDelegateBuilder(container, toType), options);
        }
    }
}