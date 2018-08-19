namespace AMKsGear.Core.Automation.Mapper
{
    public partial class Mapper
    {
        public static partial class Compiler
        {
            public partial class Context
            {
                public Mapper Mapper { get; }
                public MapperContext MapperContext { get; }
                public Mapping Mapping { get; }
                public MappingCompiledInfo MappingCompiledInfo { get; }

                public bool AllowNullableTypes { get; set; }

                public Context(
                    Mapper mapper,
                    MapperContext mapperContext,
                    Mapping mapping,
                    MappingCompiledInfo mappingCompiledInfo
                )
                {
                    Mapper = mapper;
                    MapperContext = mapperContext;
                    MappingCompiledInfo = mappingCompiledInfo;
                    
                    Mapping = mapping;
                }

                public Context(
                    Context context,
                    Mapping mapping
                )
                {
                    Mapper = context.Mapper;
                    MapperContext = context.MapperContext;
                    MappingCompiledInfo = context.MappingCompiledInfo;
                    
                    Mapping = mapping;
                }

                public Context Clone()
                {
                    return new Context(
                        Mapper,
                        MapperContext,
                        Mapping,
                        MappingCompiledInfo
                        );
                }
            }
        }
    }
}