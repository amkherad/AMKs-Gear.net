using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Core.Automation.Object.Mapper.Annotations;
using AMKsGear.Core.Automation.Reflection;

namespace AMKsGear.Core.Automation.Object.Mapper
{
    public delegate bool MatchFinder(MapperContext context, MappingProperyInfo destinationInfo, MappingProperyInfo sourceInfo);

    public class MapperContext
    {
        public ITypeResolver TypeResolver { get; set; }
        
        public MatchFinder MatchFinder { get; }
        public StringComparer NameComparer { get; }
        public bool SkipNulls { get; protected set; }

        public bool MapProperties { get; set; } = true;

        protected readonly IList<Type> SourceExcludeMember;
        public IReadOnlyCollection<Type> SourceMemberExcludeAnnotation => new ReadOnlyCollection<Type>(SourceExcludeMember);


        protected MapperContext(MatchFinder matchFinder, StringComparer nameComparer, bool skipNulls, bool mapProperties)
        {
            MatchFinder = matchFinder;
            NameComparer = nameComparer;
            SkipNulls = skipNulls;
            MapProperties = mapProperties;
        }

        public static MapperContext Default() =>
            new MapperContext(DefaultMatchFinder, StringComparer.CurrentCulture, true, false);



        public static readonly MatchFinder DefaultMatchFinder = (context, destInfo, srcInfo) =>
        {
            if (!context.NameComparer.Equals(srcInfo.Name, destInfo.Name)) return false;

            var srcAttrs = srcInfo.MemberInfo.GetCustomAttributes(true);
            var dstAttrs = destInfo.MemberInfo.GetCustomAttributes(true);

            var annotationExcludes = context.SourceExcludeMember;
            if (annotationExcludes != null && annotationExcludes.Count > 0)
            {
                foreach (var attr in srcAttrs)
                    if (annotationExcludes.Any(x => x._IsInstanceOfType(attr)))
                        return false;
                foreach (var attr in dstAttrs)
                    if (annotationExcludes.Any(x => x._IsInstanceOfType(attr)))
                        return false;
            }

            if (srcAttrs.Any(x => x is MapperExcludeAttribute)) return false;
            if (dstAttrs.Any(x => x is MapperExcludeAttribute)) return false;

            var propertyType = srcInfo.PropertyType;
            var propertyTypeInfo = srcInfo.PropertyTypeInfo;

            var assignable = destInfo.PropertyTypeInfo.IsAssignableFrom(propertyTypeInfo);

            if (assignable) return true;

            var castAttr = dstAttrs.FirstOrDefault(x => x is MapperCastAttribute) as MapperCastAttribute;
            if (castAttr == null) return false;

            var source = castAttr.InternalSource;
            if (source == null || propertyType == source)
            {
                destInfo.CastExpression = castAttr.InternalCastExpression;
                destInfo.PassNullsToCast = castAttr.InternalPassNulls;
                return true;
            }

            return false;
        };

        public class MapInfo : TypeInformationTypeDescriptorContext
        {
            public string SourceTypeInfo;

            public MapInfo(string bindingStrongName) : base(bindingStrongName)
            {
            }

            public MapInfo(Type bindingType) : base(bindingType)
            {
            }
        }
    }
}