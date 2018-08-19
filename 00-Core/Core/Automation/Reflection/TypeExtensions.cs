using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AMKsGear.Core.Automation.Reflection
{
    /// <summary>
    /// Contains some helper methods to <see cref="Type"/> and <see cref="TypeInfo"/>.
    /// </summary>
    /// <remarks>
    /// This class lacks of any check against null for <c>this</c> parameters.
    /// </remarks>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets base type of a nullable, if input is not a nullable type returns the input.
        /// </summary>
        /// <param name="type">The input type.</param>
        /// <returns>The input or nullable base type.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TypeInfo GetTypeOrNullableBaseType(this TypeInfo type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)
                ? Nullable.GetUnderlyingType(type.BaseType).GetTypeInfo()
                : type;
        }

        /// <summary>
        /// Gets base type of a nullable, if input is not a nullable type returns the input.
        /// </summary>
        /// <param name="type">The input type.</param>
        /// <returns>The input or nullable base type.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type GetTypeOrNullableBaseType(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)
                ? Nullable.GetUnderlyingType(type)
                : type;
        }


        /// <summary>
        /// Checks whether type is nullable or not.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>A boolean indicating the input is nullable or not.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Checks whether type is nullable or not.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="nullableBaseType">The nullable base type.</param>
        /// <returns>A boolean indicating the input is nullable or not.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullable(this Type type, out Type nullableBaseType)
        {
            if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Nullable<>))
            {
                nullableBaseType = null;
                return false;
            }

            nullableBaseType = type.GetGenericArguments()[0];
            return true;
        }

        /// <summary>
        /// Checks whether type is nullable or not.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>A boolean indicating the input is nullable or not.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullable(this TypeInfo type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Checks whether type is nullable or not.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="nullableBaseType">The nullable base type.</param>
        /// <returns>A boolean indicating the input is nullable or not.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullable(this TypeInfo type, out Type nullableBaseType)
        {
            if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Nullable<>))
            {
                nullableBaseType = null;
                return false;
            }

            nullableBaseType = type.GetGenericArguments()[0];
            return true;
        }


        /// <summary>
        /// Checks whether type is inherited from <see cref="IConvertible"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsConvertible(this Type type) => typeof(IConvertible).IsAssignableFrom(type);

        /// <summary>
        /// Checks whether type is inherited from <see cref="IConvertible"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsConvertible(this TypeInfo type) => typeof(IConvertible).IsAssignableFrom(type);


        /// <summary>
        /// Checks whether type is primitive or decimal.
        /// </summary>
        /// <param name="type">The input type.</param>
        /// <returns>A boolean determining the type is primitive or decimal.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPrimitiveOrDecimal(this Type type)
            => type.IsPrimitive || type == typeof(decimal);

        /// <summary>
        /// Checks whether type is primitive or decimal.
        /// </summary>
        /// <param name="type">The input type.</param>
        /// <returns>A boolean determining the type is primitive or decimal.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPrimitiveOrDecimal(this TypeInfo type)
            => type.IsPrimitive || type == typeof(decimal);


        /// <summary>
        /// Determines whether a type is assignable to a generic type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="genericType"></param>
        /// <returns></returns>
        public static bool IsAssignableToGeneric(this Type type, Type genericType)
        {
            var interfaces = type.GetInterfaces();
            foreach (var iface in interfaces)
            {
                if (iface.IsGenericType && iface.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            return false;
        }


        /// <summary>
        /// Returns the type of <see cref="IEnumerable{T}"/> generic argument type or typeof(object) if <see cref="IEnumerable"/> presented.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetEnumerableBaseType(this Type type)
        {
            if (type.IsArray)
            {
                return type.GetElementType();
            }

            foreach (var iface in type.GetInterfaces())
            {
                if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return iface.GetGenericArguments()[0];
                }
            }

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                return typeof(object);
            }

#warning Localization Required.
            //@LocalizationRequired
            throw new Exception();
        }

        /// <summary>
        /// Returns the type of <see cref="IEnumerable{T}"/> generic argument type or typeof(object) if <see cref="IEnumerable"/> presented.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetEnumerableBaseType(this TypeInfo type)
        {
            if (type.IsArray)
            {
                return type.GetElementType();
            }

            foreach (var iface in type.GetInterfaces())
            {
                if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return iface.GetGenericArguments()[0];
                }
            }

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                return typeof(object);
            }

#warning Localization Required.
            //@LocalizationRequired
            throw new Exception();
        }
        
        
        /// <summary>
        /// Determines whether the type has default constructor.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool HasDefaultConstructor(this Type type)
        {
            if (type == typeof(void) ||
                type.IsAbstract ||
                type.IsInterface)
            {
                return false;
            }

            if (type.IsValueType)
            {
                return true;
            }

            return type.GetConstructor(Type.EmptyTypes) != null;
        }
        
        /// <summary>
        /// Determines whether the type has default constructor.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool HasDefaultConstructor(this TypeInfo type)
        {
            if (type == typeof(void) ||
                type.IsAbstract ||
                type.IsInterface)
            {
                return false;
            }

            if (type.IsValueType)
            {
                return true;
            }

            return type.GetConstructor(Type.EmptyTypes) != null;
        }


        /// <summary>
        /// Tries to get default constructor for the type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="constructorInfo"></param>
        /// <returns></returns>
        public static bool TryGetDefaultConstructor(this Type type, out ConstructorInfo constructorInfo)
        {
            if (type == typeof(void) ||
                type.IsAbstract ||
                type.IsInterface)
            {
                constructorInfo = default;
                return false;
            }

            constructorInfo = type.GetConstructor(Type.EmptyTypes);
            return constructorInfo != null;
        }
        
        /// <summary>
        /// Tries to get default constructor for the type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="constructorInfo"></param>
        /// <returns></returns>
        public static bool TryGetDefaultConstructor(this TypeInfo type, out ConstructorInfo constructorInfo)
        {
            if (type == typeof(void) ||
                type.IsAbstract ||
                type.IsInterface)
            {
                constructorInfo = default;
                return false;
            }

            constructorInfo = type.GetConstructor(Type.EmptyTypes);
            return constructorInfo != null;
        }
    }
}