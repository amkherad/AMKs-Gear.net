using System.Collections.Generic;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Modeling
{
    public class ModelMemberInfoCollection : List<IModelMemberInfo>
    {
        public ModelMemberInfoCollection() { }
        public ModelMemberInfoCollection(IEnumerable<IModelMemberInfo> collection)
            : base(collection) { }
    }
}