//using System;
//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
//using AMKsGear.Architecture.Trace;
//using AMKsGear.Core.Trace.PerObjectTracing.Settings;
//
//namespace AMKsGear.Core.Trace.PerObjectTracing
//{
//    public class PerObjectTrace
//    {
//        public static void WritePrivateLog(object source, string log, string category, ILoggingContext context,
//            [CallerMemberName] string callerMemberName = null,
//            [CallerLineNumber] int callerLineNumber = 0,
//            [CallerFilePath] string callerFilePath = null)
//        {
//            PerObjectTracingHandler.WritePrivateLog(source, log, category, context,
//                callerMemberName, callerLineNumber, callerFilePath);
//        }
//
//        #region Configuration
//        public static void AddObjectToLogGroup(object obj, string namedGroup)
//        {
//            //PerObjectTracingHandler.LogSettings.NamedGroup.IsMemberOfGroup()
//        }
//        public static void RemoveObjectFromLogGroup(object obj, string namedGroup)
//        {
//
//        }
//
//        public static void TrackObjectsOfType(Type type, bool includeTypeHierarchy = true)
//        {
//            PerObjectTracingHandler.LogSettings.TypedGroup.AddType(type, includeTypeHierarchy);
//        }
//        public static void UnTrackObjectsOfType(Type type)
//        {
//            PerObjectTracingHandler.LogSettings.TypedGroup.RemoveType(type);
//        }
//
//        public static bool IsTrackingPerObject(object obj)
//        {
//            ThrowHelper.ThrowIfNull_Arg(obj, nameof(obj));
//
//            return PerObjectTracingHandler.LogSettings.GetPerObjectSettings(obj) != null;
//        }
//        public static void UnTrackPerObject(object obj)
//        {
//            ThrowHelper.ThrowIfNull_Arg(obj, nameof(obj));
//
//            var objHashCode = PerObjectTracingHandler.GetObjectHashCode(obj);
//            TracePerObjectSettings perObjectSettings;
//            if (PerObjectTracingHandler.LogSettings.PerObjectSettings.TryGetValue(objHashCode, out perObjectSettings))
//            {
//                PerObjectTracingHandler.LogSettings.PerObjectSettings.Remove(objHashCode);
//            }
//        }
//        public static void SetPerObjectLogEngine(object obj, ILoggerEngine logEngine)
//        {
//            ThrowHelper.ThrowIfNull_Args(
//                obj, nameof(obj),
//                logEngine, nameof(logEngine));
//
//            PerObjectTracingHandler.LogSettings.GetOrCreatePerObjectSettings(obj)
//                .LogEngine = logEngine;
//        }
//        public static ILoggerEngine GetPerObjectLogEngine(object obj)
//        {
//            ThrowHelper.ThrowIfNull_Arg(obj, nameof(obj));
//
//            return PerObjectTracingHandler.LogSettings.GetPerObjectSettings(obj)
//                .LogEngine;
//        }
//        public static void SetPerObjectLogCategories(object obj, IEnumerable<string> categories, LogCategoryOverrideStrategy overrideStrategy = LogCategoryOverrideStrategy.OnlyOverride)
//        {
//            ThrowHelper.ThrowIfNull_Args(
//                obj, nameof(obj),
//                categories, nameof(categories));
//
//            var settings = PerObjectTracingHandler.LogSettings.GetOrCreatePerObjectSettings(obj);
//
//            settings.Categories = categories;
//            settings.CategoryOverrideStrategy = overrideStrategy;
//        }
//        public static IEnumerable<string> GetPerObjectLogCategory(object obj)
//        {
//            ThrowHelper.ThrowIfNull_Arg(obj, nameof(obj));
//
//            return PerObjectTracingHandler.LogSettings.GetPerObjectSettings(obj)
//                .Categories;
//        }
//        public static void SetPerObjectLogCategoryOverrideStrategy(object obj, LogCategoryOverrideStrategy overrideStrategy = LogCategoryOverrideStrategy.OnlyOverride)
//        {
//            ThrowHelper.ThrowIfNull_Arg(obj, nameof(obj));
//
//            PerObjectTracingHandler.LogSettings.GetOrCreatePerObjectSettings(obj)
//                .CategoryOverrideStrategy = overrideStrategy;
//        }
//        public static LogCategoryOverrideStrategy GetPerObjectLogCategoryOverrideStrategy(object obj)
//        {
//            ThrowHelper.ThrowIfNull_Arg(obj, nameof(obj));
//
//            return PerObjectTracingHandler.LogSettings.GetPerObjectSettings(obj)
//                .CategoryOverrideStrategy;
//        }
//        #endregion
//    }
//}