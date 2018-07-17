using System;
using System.Collections.Generic;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Architecture.Modeling
{
    /// <summary>
    /// Abstraction layer to get information about members.
    /// </summary>
    public interface IModelMemberInfo : IAdapter
    {
        //Type DeclaringType { get; }
        //Module Module { get; }
        
        /// <summary>
        /// Gets member name.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Gets member type.
        /// </summary>
        Type Type { get; }
        //Type ReflectedType { get; }

        /// <summary>
        /// Gets custom attributes of member.
        /// </summary>
        /// <param name="inherit">Determines the method should return all base types attributes.</param>
        /// <returns>List of member attributes.</returns>
        IEnumerable<object> GetCustomAttributes(bool inherit);
        /// <summary>
        /// Gets custom attributes of member.
        /// </summary>
        /// <param name="attributeType">Forces method to get attributes derived from attributeType only.</param>
        /// <param name="inherit">Determines the method should return all base types attributes.</param>
        /// <returns>List of member attributes.</returns>
        IEnumerable<object> GetCustomAttributes(Type attributeType, bool inherit);
        
        /// <summary>
        /// Checks whether attribute is in the member's attribute list.
        /// </summary>
        /// <param name="attributeType">The attribute to check against attribute list.</param>
        /// <param name="inherit">Determines the method should check in all base types attributes.</param>
        /// <returns></returns>
        bool IsDefined(Type attributeType, bool inherit);
    }
}