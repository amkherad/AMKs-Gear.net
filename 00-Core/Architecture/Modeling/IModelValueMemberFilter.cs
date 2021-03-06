using System;
using System.Collections.Generic;

namespace AMKsGear.Architecture.Modeling
{
    /// <summary>
    /// Provides a filter on members of a model.
    /// </summary>
    public interface IModelValueMemberFilter
    {
        /// <summary>
        /// A fixed filter predicate.
        /// </summary>
        Func<string, IModelValueMemberInfo, bool> FilterPredicate { get; }

        /// <summary>
        /// Applies a filter on an enumerable of <see cref="IModelValueMemberInfo"/>.
        /// </summary>
        /// <param name="memberInfos"></param>
        /// <returns></returns>
        IDictionary<string, IModelValueMemberInfo> Filter(IDictionary<string, IModelValueMemberInfo> memberInfos);
    }
}