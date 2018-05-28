using System;
using System.Collections.Generic;
using System.Reflection;
using AMKsGear.Architecture.Data.Serialization;
using AMKsGear.Architecture.Modeling;
using AMKsGear.Core.Modeling;

namespace AMKsGear.Core.Data.Serialization
{
    public class SerializationContext : ISerializationContext
    {
        private readonly object _instance;
        private readonly IEnumerable<IModelMemberInfo> _members;

        public static SerializationContext FromModel<TModel>(TModel model,
            Func<PropertyInfo, bool> propertySelector = null, Func<FieldInfo, bool> fieldSelector = null)
            where TModel : IModel
        {
            return new SerializationContext(model, propertySelector, fieldSelector);
        }
        public static SerializationContext FromObject(object @obj,
            Func<PropertyInfo, bool> propertySelector = null, Func<FieldInfo, bool> fieldSelector = null)
        {
            return new SerializationContext(@obj, propertySelector, fieldSelector);
        }

        protected SerializationContext(object instance,
            Func<PropertyInfo, bool> propertySelector = null, Func<FieldInfo, bool> fieldSelector = null)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            _instance = instance;
            _members = ModelingHelper.GetMembers(instance.GetType(), propertySelector, fieldSelector);
        }

        public object Instance => _instance;
        public IEnumerable<IModelMemberInfo> GetMembers() => _members;

        public IDictionary<string, object> GetValues()
        {
            var result = new Dictionary<string, object>();

            foreach (var member in _members)
            {
                result.Add(member.Name, member.GetValue(_instance));
            }

            return result;
        }
    }
}