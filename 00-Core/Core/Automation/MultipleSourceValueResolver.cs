//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using AMKsGear.Architecture.Extensibility;

//namespace AMKsGear.Core.Extensibility
//{
//    public class MultipleSourceValueResolver : IValueResolver
//    {
//        private class ObjectType
//        {
//            public object Object;
//            public Type Type;
//            //public PropertyInfo[]
//        }

//        //protected ObjectType[] Sources;

//        public MultipleSourceValueResolver(IEnumerable<object> sortedSources, ValueResolverValueNotFoundBehavior valueNotFoundBehavior = ValueResolverValueNotFoundBehavior.UseDefault)
//        {
//            if (sortedSources == null) throw new ArgumentNullException("sortedSources");
//            var sources =
//                sortedSources.Where(x => x != null)
//                    .Select(x => new ObjectType() {Object = x, Type = x.GetType()})
//                    .ToArray();
//            Sources = sources;
//            ValueNotFoundBehavior = valueNotFoundBehavior;
//        }

//        public ValueResolverValueNotFoundBehavior ValueNotFoundBehavior { get; protected set; }

//        public object GetValue(string propName, Type valueType)
//        {
//            foreach (var src in Sources)
//            {
//                src.Type.
//            }
//        }
//        public object GetValue(string propName)
//        {

//        }
//    }
//}