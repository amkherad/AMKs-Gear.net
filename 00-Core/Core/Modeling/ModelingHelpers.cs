using System;
using System.Collections.Generic;
using System.Linq;
using AMKsGear.Architecture.Modeling;
using AMKsGear.Architecture.Modeling.Annotations;
using AMKsGear.Core.Collections;

namespace AMKsGear.Core.Modeling
{
    /// <summary>
    /// Provides helper methods to provides information about models(types).
    /// </summary>
    public static partial class ModelingHelpers
    {
        
        public static IEnumerable<T> OrderMemberInfos<T>(IEnumerable<T> collection) where T : IModelMemberInfo
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            var collectionToList = collection.ToList();
            
            var mustOrder = new List<Tuple<T, ModelMemberOrderAttribute>>(collectionToList.Count);
            var result = new ModelMemberInfoCollection<T>();

            foreach (var element in collectionToList)
            {
                var attr = element.GetCustomAttributes(typeof(ModelMemberOrderAttribute), true);
                if (attr != null && attr.Any())
                    mustOrder.Add(new Tuple<T, ModelMemberOrderAttribute>
                        (element, attr.First() as ModelMemberOrderAttribute));
                else
                    result.Add(element);
            }

            var insertAfters = new List<Tuple<T, ModelMemberOrderAttribute>>(mustOrder.Count);
            foreach (var order in mustOrder)
            {
                var member = order.Item1;
                var attr = order.Item2;
                if (attr is ModelMemberOrderInsertBeforeAttribute)
                {
                    insertAfters.Add(order);
                    continue;
                }

                var index = attr.Order;
                if (index == -1)
                    result.Add(member);
                else
                    result.Insert(index, member);
            }

            bool oneInserted = true;
            while (oneInserted && insertAfters.Any())
            {
                oneInserted = false;
                var inserts = insertAfters.ToArray();
                foreach (var order in inserts)
                {
                    var member = order.Item1;
                    var attr = order.Item2 as ModelMemberOrderInsertBeforeAttribute;
                    if (attr == null) continue;
                    var index = result.SequentialIndexOf(x => attr.NameComparer.Equals(x.Name, attr.MemberName));
                    if (index >= 0)
                    {
                        insertAfters.Remove(order);
                        result.Insert(index, member);
                        oneInserted = true;
                    }
                }
            }
            if (insertAfters.Any())
                result.AddRange(insertAfters.Select(x => x.Item1));

            return result;
        }
    }
}