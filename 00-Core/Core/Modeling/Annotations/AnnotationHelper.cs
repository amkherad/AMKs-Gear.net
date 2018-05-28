using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AMKsGear.Architecture.Modeling;
using AMKsGear.Architecture.Modeling.Annotations;

namespace AMKsGear.Core.Modeling.Annotations
{
    public static class AnnotationHelper
    {
        public static IDictionary<string, string> GetTypeHintBagAsDictionary(this MemberInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return GetTypeHintBagAsDictionary(info.GetCustomAttributes(typeof(HintBagAttribute), true));
        }
        public static IEnumerable<HintBagAttribute> GetTypeHintBag(this MemberInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return GetTypeHintBag(info.GetCustomAttributes(typeof(HintBagAttribute), true));
        }
        public static IDictionary<string, string> GetTypeHintBagAsDictionary(this IModelMemberInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return GetTypeHintBagAsDictionary(info.GetCustomAttributes(typeof(HintBagAttribute), true));
        }
        public static IEnumerable<HintBagAttribute> GetTypeHintBag(this IModelMemberInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return GetTypeHintBag(info.GetCustomAttributes(typeof(HintBagAttribute), true));
        }

        public static IEnumerable<HintBagAttribute> GetTypeHintBag(IEnumerable<Attribute> objects)
        {
            if (objects == null) throw new ArgumentNullException(nameof(objects));
            return objects.OfType<HintBagAttribute>();
        }
        public static IEnumerable<HintBagAttribute> GetTypeHintBag(IEnumerable<object> objects)
        {
            if (objects == null) throw new ArgumentNullException(nameof(objects));
            return objects.OfType<HintBagAttribute>();
        }
        public static IDictionary<string, string> GetTypeHintBagAsDictionary(IEnumerable<Attribute> objects)
        {
            if (objects == null) throw new ArgumentNullException(nameof(objects));
            return objects.OfType<HintBagAttribute>().ToDictionary(obj => obj.Name, obj => obj.Value);
        }
        public static IDictionary<string, string> GetTypeHintBagAsDictionary(IEnumerable<object> objects)
        {
            if (objects == null) throw new ArgumentNullException(nameof(objects));
            return objects.OfType<HintBagAttribute>().ToDictionary(obj => obj.Name, obj => obj.Value);
        }
    }
}