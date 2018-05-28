﻿using System;
using System.Linq.Expressions;

namespace AMKsGear.Architecture.Data
{
    public class SortingContext<T>
    {
        public Expression<Func<T, object>> FieldSelector { get; }
        public SortingOrder Order { get; }

        public SortingContext(Expression<Func<T, object>> fieldSelector, SortingOrder order = SortingOrder.Ascending)
        {
            if (fieldSelector == null) throw new ArgumentNullException(nameof(fieldSelector));
            FieldSelector = fieldSelector;
            Order = order;
        }
    }
}