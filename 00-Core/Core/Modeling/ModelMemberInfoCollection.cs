using System.Collections.Generic;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Modeling
{
    public class ModelMemberInfoCollection<T> : List<T>
        where T : IModelMemberInfo
    {
        public ModelMemberInfoCollection() { }
        public ModelMemberInfoCollection(IEnumerable<T> collection)
            : base(collection) { }
    }
}