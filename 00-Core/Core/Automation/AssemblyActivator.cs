using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AMKsGear.Architecture;
using AMKsGear.Architecture.Automation;
using AMKsGear.Architecture.Automation.Annotations;
using AMKsGear.Architecture.Patterns;
using AMKsGear.Core.Automation.IoC;
using AMKsGear.Core.Framework;

namespace AMKsGear.Core.Automation
{
    public static class AssemblyActivator
    {
        public static int ExecuteAll(IEnumerable<Assembly> assemblies, ICrossCuttingContext context)
        {
            if (assemblies == null) throw new ArgumentNullException(nameof(assemblies));
            var allActivatorAttributess = new List<AssemblyActivatorAttribute>();
            foreach (var assembly in assemblies)
            {
                var activatorAttributes = assembly.GetCustomAttributes(typeof(AssemblyActivatorAttribute));
                allActivatorAttributess.AddRange(activatorAttributes.Cast<AssemblyActivatorAttribute>());
            }
            var orderedActivatorAttributess = allActivatorAttributess
                .OrderHelper(x => x.Order, SortingOrder.Descending);

            var activators = orderedActivatorAttributess.Select(x =>
                TypeResolver.CreateInstance(x.AssemblyActivatorType) as IAssemblyActivator).ToList();

            var orderedActivators = activators.PriorityHelper();
            //allActivators.AddRange(activators.Select(x =>
            //        TypeResolver.CreateInstance(x.AssemblyActivatorType) as IAssemblyActivator));
            foreach (var assemblyActivator in orderedActivators)
            {
                assemblyActivator?.Activate(PlatformContext.Current, context);
            }
            return activators.Count;
        }
    }
}