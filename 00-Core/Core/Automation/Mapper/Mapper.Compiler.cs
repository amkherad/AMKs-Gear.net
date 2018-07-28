using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Core.Automation.Mapper
{
    public partial class Mapper
    {
        public static class Compiler
        {
            [NotNull]
            public static MappingCompiledInfo CompileMapping(Mapper mapper, MapperContext context, Mapping mapping)
            {
                throw new MapperCompileException();
                return null;
            }
        }
    }
}