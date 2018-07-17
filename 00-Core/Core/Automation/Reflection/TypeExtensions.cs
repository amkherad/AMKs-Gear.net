using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AMKsGear.Core.Automation.Reflection
{
    public static class TypeExtensions
    {
        public static TypeInfo GetPureType(this TypeInfo type)
        {
            ThrowHelper.ThrowIfNull_Arg(type, nameof(type));
            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                ? Nullable.GetUnderlyingType(type.BaseType).GetTypeInfo()
                : type;
        }
        public static Type GetPureType(this Type type)
        {
            ThrowHelper.ThrowIfNull_Arg(type, nameof(type));
            return (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                ? Nullable.GetUnderlyingType(type)
                : type;
        }

        public static IEnumerable<PropertyInfo> GetReadWriteProperties(this TypeInfo type)
        {
            ThrowHelper.ThrowIfNull_Arg(type, nameof(type));
            return type.AsType().GetRuntimeProperties().Where(x => x.CanRead && x.CanWrite);
        }
        public static IEnumerable<PropertyInfo> GetReadWriteProperties(this Type type)
        {
            ThrowHelper.ThrowIfNull_Arg(type, nameof(type));
            return type.GetRuntimeProperties().Where(x => x.CanRead && x.CanWrite);
        }
        public static IEnumerable<FieldInfo> GetReadWriteFields(this TypeInfo type)
        {
            ThrowHelper.ThrowIfNull_Arg(type, nameof(type));
            return type.AsType().GetRuntimeFields();
        }
        public static IEnumerable<FieldInfo> GetReadWriteFields(this Type type)
        {
            ThrowHelper.ThrowIfNull_Arg(type, nameof(type));
            return type.GetRuntimeFields();
        }

        public static IEnumerable<Type> GetTypeHierarchy(this Type type) => GetTypeHierarchy(type, false);
        public static IEnumerable<Type> GetTypeHierarchy(this Type type, bool includeInterfaces)
        {
            ThrowHelper.ThrowIfNull_Arg(type, nameof(type));

            var typeInfo = type.GetTypeInfo();

            //typeInfo.ImplementedInterfaces

            var current = typeInfo;
            var baseType = current.BaseType;
            if (includeInterfaces)
            {
                while (baseType != null)
                {
                    yield return baseType;
                    foreach (var iface in current.ImplementedInterfaces) yield return iface;
                    current = baseType.GetTypeInfo();
                    baseType = current.BaseType;
                }
            }
            else
            {
                while (baseType != null)
                {
                    yield return baseType;
                    current = baseType.GetTypeInfo();
                    baseType = current.BaseType;
                }
            }
        }

        /// <summary>
        /// [ <c>public static object GetDefault(this Type type)</c> ]
        /// <para></para>
        /// Retrieves the default value for a given Type
        /// </summary>
        /// <param name="type">The Type for which to get the default value</param>
        /// <returns>The default value for <paramref name="type"/></returns>
        /// <remarks>
        /// If a null Type, a reference Type, or a System.Void Type is supplied, this method always returns null.  If a value type 
        /// is supplied which is not publicly visible or which contains generic parameters, this method will fail with an 
        /// exception.
        /// </remarks>
        /// <example>
        /// To use this method in its native, non-extension form, make a call like:
        /// <code>
        ///     object Default = DefaultValue.GetDefault(someType);
        /// </code>
        /// To use this method in its Type-extension form, make a call like:
        /// <code>
        ///     object Default = someType.GetDefault();
        /// </code>
        /// </example>
        /// <see cref="http://stackoverflow.com/questions/2490244/default-value-of-a-type-at-runtime"/>
        /// <seealso cref="GetDefault&lt;T&gt;"/>
        public static object GetDefault(this Type type)
        {
            var typeInfo = type.GetTypeInfo();
            // If no Type was supplied, if the Type was a reference type, or if the Type was a System.Void, return null
            if (type == null || !typeInfo.IsValueType || type == typeof(void))
                return null;

            // If the supplied Type has generic parameters, its default value cannot be determined
            if (typeInfo.ContainsGenericParameters)
                throw new ArgumentException($"The supplied value type <{type}> contains generic parameters, so the default value cannot be retrieved");

            // If the Type is a primitive type, or if it is another publicly-visible value type (i.e. struct/enum), return a 
            //  default instance of the value type
            if (typeInfo.IsPrimitive || !typeInfo.IsNotPublic)
            {
                try
                {
                    return Activator.CreateInstance(type);
                }
                catch (Exception e)
                {
                    throw new ArgumentException($"The Activator.CreateInstance method could not create a default instance of the supplied value type <{type}>", e);
                }
            }

            // Fail with exception
            throw new ArgumentException($"The supplied value type <{type}> is not a publicly-visible type, so the default value cannot be retrieved");
        }
    }
}