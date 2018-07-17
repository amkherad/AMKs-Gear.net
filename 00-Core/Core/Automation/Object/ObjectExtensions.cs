using System;
using System.Reflection;

namespace AMKsGear.Core.Automation.Object
{
    public static class ObjectExtensions
    {
        #region Member Utils
        //public static TDelegate GetNonPublicMethod<TDelegate>(this object @object, string methodName)
        //{
        //    if (@object == null) throw new ArgumentNullException(nameof(@object));
        //    return (TDelegate)(object)@object.GetType()
        //        .GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic)
        //        .CreateDelegate(typeof(TDelegate), @object);
        //}
        #endregion
        
        public static Type GetPureType(this object @object)
        {
            if (@object == null) throw new ArgumentNullException(nameof(@object));
            var type = @object.GetType();
            if (!type.GetTypeInfo().IsGenericType) return type;
            return type.GetGenericTypeDefinition() == typeof (Nullable<>)
                ? Nullable.GetUnderlyingType(type)
                : type;
        }
        public static Type GetPureType(this Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (!type.GetTypeInfo().IsGenericType) return type;
            return type.GetGenericTypeDefinition() == typeof(Nullable<>)
                ? Nullable.GetUnderlyingType(type)
                : type;
        }
        public static Type GetPureType(this TypeInfo type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            var baseType = type.BaseType;
            if (!type.IsGenericType) return baseType;
            return type.GetGenericTypeDefinition() == typeof(Nullable<>)
                ? Nullable.GetUnderlyingType(baseType)
                : baseType;
        }


        public static TResult DoAs<T, TResult>(this T obj, Func<T, TResult> action)
        {
            return (action ?? (x => default(TResult))).Invoke(obj);
        }
            
        public static T ModifyAs<T>(this T obj, Action<T> action)
        {
            action?.Invoke(obj);
            return obj;
        }

        internal static bool _IsInstanceOfType(this Type type, object obj)
        {
            return obj.GetType() == type || obj.GetType().GetTypeInfo().IsSubclassOf(type);
        }
        //public static bool IsInstanceOfType(this Type type, object obj)
        //{
        //    if (type == null) throw new ArgumentNullException(nameof(type));
        //    if (obj == null) throw new ArgumentNullException(nameof(obj));
        //    return obj.GetType() == type || obj.GetType().GetTypeInfo().IsSubclassOf(type);
        //}
    }
}