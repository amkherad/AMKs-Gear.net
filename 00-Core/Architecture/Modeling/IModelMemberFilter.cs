using System;
using System.Collections.Generic;

namespace AMKsGear.Architecture.Modeling
{
    /// <summary>
    /// Provides a filter on members of a model.
    /// </summary>
    public interface IModelMemberFilter
    {
        /// <summary>
        /// A fixed filter predicate.
        /// </summary>
        Func<IModelMemberInfo, bool> FilterPredicate { get; }

        /// <summary>
        /// Applies a filter on an enumerable of <see cref="IModelMemberInfo"/>.
        /// </summary>
        /// <param name="memberInfos"></param>
        /// <returns></returns>
        IEnumerable<IModelMemberInfo> Filter(IEnumerable<IModelMemberInfo> memberInfos);
    }
}