using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Core.Automation.Mapper
{
    public partial class Mapper
    {
        /// <summary>
        /// <see cref="Mapper"/> internal default compiler.
        /// </summary>
        public static class Compiler
        {
            [NotNull]
            public static MappingCompiledInfo CompileMapping(Mapper mapper, MapperContext context, Mapping mapping)
            {
                throw new MapperCompileException();
            }
        }
    }
}