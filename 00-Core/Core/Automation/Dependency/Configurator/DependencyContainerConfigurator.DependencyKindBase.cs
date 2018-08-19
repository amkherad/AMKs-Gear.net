using System;
using System.Linq.Expressions;

namespace AMKsGear.Core.Automation.Dependency.Configurator
{
    public partial class DependencyContainerConfigurator
    {
        public abstract class DependencyKindBase : IDependencyKind
        {
            public MemberBinding BindProperty(Type baseType, string name)
            {
                
                return new MemberBinding();
            }
            public MemberBinding BindProperty(Type baseType, Type resolveType, string name)
            {
                
                return new MemberBinding();
            }
            
            public MemberBinding<TBase, TPropertyType> BindProperty<TBase, TPropertyType>(Expression<Func<TBase, TPropertyType>> propertyExpression)
            {
                
                return new MemberBinding<TBase, TPropertyType>();
            }
            public MemberBinding<TBase, TResolve> BindProperty<TBase, TResolve, TPropertyType>(Expression<Func<TBase, TPropertyType>> propertyExpression)
                where TResolve : TBase
            {
                
                return new MemberBinding<TBase, TResolve>();
            }
            
            public MemberBinding BindValueMember(Type baseType, string name)
            {
                
                return new MemberBinding();
            }
            public MemberBinding BindValueMember(Type baseType, Type resolveType, string name)
            {
                
                return new MemberBinding();
            }
            
            public MemberBinding<TBase, TPropertyType> BindValueMember<TBase, TPropertyType>(Expression<Func<TBase, TPropertyType>> propertyExpression)
            {
                
                return new MemberBinding<TBase, TPropertyType>();
            }
            public MemberBinding<TBase, TResolve> BindValueMember<TBase, TResolve, TPropertyType>(Expression<Func<TBase, TPropertyType>> propertyExpression)
                where TResolve : TBase
            {
                
                return new MemberBinding<TBase, TResolve>();
            }
        }
    }
}