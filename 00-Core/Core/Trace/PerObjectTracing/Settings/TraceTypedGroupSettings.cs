//using System;
//using System.Collections.Generic;
//using AMKsGear.Core.Automation.Reflection;
//
//namespace AMKsGear.Core.Trace.PerObjectTracing.Settings
//{
//    internal class TraceTypedGroupSettings : TraceObjectGroupSettings
//    {
//        private readonly HashSet<Type> IncludedTypes = new HashSet<Type>(); 
//
//        public override bool IsMemberOfGroup(object obj)
//        {
//            return IncludedTypes.Contains(obj.GetType());
//        }
//
//        public void AddType(Type type, bool includeTypeHierarchy)
//        {
//            if (includeTypeHierarchy)
//            {
//                foreach (var t in type.GetTypeHierarchy(true))
//                {
//                    IncludedTypes.Add(t);
//                }
//            }
//            else
//            {
//                IncludedTypes.Add(type);
//            }
//        }
//        public bool RemoveType(Type type)
//        {
//            return IncludedTypes.Remove(type);
//        }
//    }
//}