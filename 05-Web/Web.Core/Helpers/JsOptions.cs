using System;
using AMKsGear.Architecture.Data.Serialization;
using AMKsGear.Core.Automation.IoC;
using AMKsGear.Core.Data.Serialization;

namespace AMKsGear.Web.Core.Helpers
{
    public abstract class JsOptions
    {
        public override string ToString() => Serialize(this);

        public static string Serialize(object target)
        {
//            var factory = TypeResolver.EnsureCreateInstance<ISerializerFactory>();
//            using (var serializer = factory.JsonSerializer())
//                return serializer.Serialize(target, SerializationOptions.JsOptions());
            throw new NotImplementedException();
        }
    }
}