using System;
using System.ComponentModel;
using System.Linq.Expressions;
using AMKsGear.Core.Data.Binding.EndPoints;
using AMKsGear.Core.Linq.Expressions;

namespace AMKsGear.Core.Data.Binding
{
    public static class DataBinding
    {
        public static void BindProperty<T>(this INotifyPropertyChanged source, Expression<Func<T>> property, IBindingEndpoint endpoint)
            => BindProperty(source, property.GetMethodName(), endpoint);
        public static void BindProperty(this INotifyPropertyChanged source, string propertyName, IBindingEndpoint endpoint)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));

            source.PropertyChanged += (sender, args) =>
            {
                //endpoint
            };
        }
    }
}