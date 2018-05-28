using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AMKsGear.Architecture.Trace;
using AMKsGear.Core.Trace.PerObjectTracing.Settings;

namespace AMKsGear.Core.Trace.PerObjectTracing
{
    internal class PerObjectTracingHandler
    {
        public static readonly Settings LogSettings;
        static PerObjectTracingHandler()
        {
            LogSettings = new Settings();
        }

        public static int GetObjectHashCode(object obj)
        {
            return RuntimeHelpers.GetHashCode(obj);
        }

        public static void WritePrivateLog(object source, string log, string category, ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath)
        {
            var perObjectSettings =
                LogSettings.GetPerObjectSettings(source) ??
                TracePerObjectSettings.Default;

            _writePrivateLog(log,
                context, callerMemberName, callerLineNumber, callerFilePath,
                perObjectSettings, category);

            var groups = LogSettings.GetGroups(source);
            if (groups != null)
            {
                foreach (var g in groups)
                {
                    _writePrivateLog(log,
                        context, callerMemberName, callerLineNumber, callerFilePath,
                        g, category);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void _writePrivateLog(
            string log, ILoggingContext context, string callerMemberName, int callerLineNumber, string callerFilePath,
            TraceSettings perObjectSettings, string category)
        {
            var targetCategories = new List<string>();

            var logEngine = perObjectSettings.LogEngine;
            var categoryOverride = perObjectSettings.CategoryOverrideStrategy;

            switch (categoryOverride)
            {
                case LogCategoryOverrideStrategy.NoOverride:
                    targetCategories.AddRange(perObjectSettings.Categories);
                    break;
                case LogCategoryOverrideStrategy.OnlyOverride:
                    targetCategories.Add(category);
                    break;
                case LogCategoryOverrideStrategy.UseBoth:
                    targetCategories.Add(category);
                    targetCategories.AddRange(perObjectSettings.Categories);
                    break;
                case LogCategoryOverrideStrategy.UseBothExclusive:
                    targetCategories.AddRange(
                        perObjectSettings.Categories
                            .Where(x => x != category));
                    break;
            }

            foreach (var cat in targetCategories)
            {
                logEngine.Write(log, cat, context, callerMemberName, callerLineNumber, callerFilePath);
            }
        }


        public class Settings
        {
            public readonly IDictionary<int, TracePerObjectSettings> PerObjectSettings;
            public readonly TraceObjectGroupSettings[] Groups;
            public readonly TraceTypedGroupSettings TypedGroup;
            public readonly TraceNamedGroupSettings NamedGroup;

            public Settings()
            {
                PerObjectSettings = new Dictionary<int, TracePerObjectSettings>();

                TypedGroup = new TraceTypedGroupSettings();
                NamedGroup = new TraceNamedGroupSettings();
                Groups = new TraceObjectGroupSettings[]
                {
                    TypedGroup,
                    NamedGroup
                };
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public TracePerObjectSettings GetOrCreatePerObjectSettings(object obj)
                => GetOrCreatePerObjectSettings(GetObjectHashCode(obj));
            public TracePerObjectSettings GetOrCreatePerObjectSettings(int objHashCode)
            {
                TracePerObjectSettings perObjectSettings;
                if (!PerObjectSettings.TryGetValue(objHashCode, out perObjectSettings))
                {
                    perObjectSettings = new TracePerObjectSettings();
                    PerObjectSettings.Add(new KeyValuePair<int, TracePerObjectSettings>(objHashCode, perObjectSettings));
                }

                return perObjectSettings;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public TracePerObjectSettings GetPerObjectSettings(object obj)
                => GetPerObjectSettings(GetObjectHashCode(obj));
            public TracePerObjectSettings GetPerObjectSettings(int objHashCode)
            {
                TracePerObjectSettings perObjectSettings;
                if (!PerObjectSettings.TryGetValue(objHashCode, out perObjectSettings))
                {
                    return null;
                }

                return perObjectSettings;
            }

            public IEnumerable<TraceObjectGroupSettings> GetGroups(object obj)
                => Groups.Where(x => x.IsMemberOfGroup(obj));
        }
    }
}