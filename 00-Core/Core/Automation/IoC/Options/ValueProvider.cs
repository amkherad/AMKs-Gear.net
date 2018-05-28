using System;

namespace AMKsGear.Core.Automation.IoC.Options
{
    public class ValueProvider : TypeResolverOption
    {
        public Func<
            TypeResolverDestinationType, //destination type
            Type,                        //required type
            string,                      //property name.
            object> ValueProviderFactory { get; }

        public Func<
            TypeResolverDestinationType, //destination type
            Type,                        //required type
            string,                      //property name.
            bool> CanProvide { get; }

        public ValueProvider(Func<TypeResolverDestinationType, Type, string, object> valueProviderFactory,
            Func<TypeResolverDestinationType, Type, string, bool> canProvide)
        {
            if (valueProviderFactory == null) throw new ArgumentNullException(nameof(valueProviderFactory));
            if (canProvide == null) throw new ArgumentNullException(nameof(canProvide));
            ValueProviderFactory = valueProviderFactory;
            CanProvide = canProvide;
        }
    }
}