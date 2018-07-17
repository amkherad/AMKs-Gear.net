using System.Linq.Expressions;

namespace AMKsGear.Architecture.Modeling
{
    /// <summary>
    /// Abstraction layer to get information about fields and properties.
    /// </summary>
    public interface IModelValueMemberInfo : IModelMemberInfo
    {
        /// <summary>
        /// Returns member's value.
        /// </summary>
        /// <param name="instance">The instance of the object.</param>
        /// <returns></returns>
        object GetValue(object instance);
        
        /// <summary>
        /// Returns member's value.
        /// </summary>
        /// <param name="instance">The instance of the object.</param>
        /// <param name="defaultValue">Specify a custom default value for member.</param>
        /// <returns></returns>
        object GetValue(object instance, object defaultValue);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="value"></param>
        void SetValue(object instance, object value);
        
        
        /// <summary>
        /// Creates an expression to get value.
        /// </summary>
        /// <param name="source">Object instance expression.</param>
        /// <returns></returns>
        Expression CreateGetExpression(Expression source);
        
        /// <summary>
        /// Creates an expression to set the value.
        /// </summary>
        /// <param name="source">Object instance expression.</param>
        /// <param name="value">Value expression.</param>
        /// <returns></returns>
        Expression CreateSetExpression(Expression source, Expression value);
    }
}