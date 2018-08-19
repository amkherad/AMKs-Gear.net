using System;
using System.Linq.Expressions;

namespace AMKsGear.Core.Automation.Mapper
{
    public partial class Mapper
    {
        public static partial class Compiler
        {
            public interface ITypeCompiler
            {
                bool CanMap(
                    Context context,
                    Type sourceType,
                    Type destinationType);

                Expression CreateCopyExpression(
                    Context context,
                    Expression source,
                    Expression destination,
                    MappingType mappingType);
                
                Expression CreateConvertExpression(
                    Context context,
                    Expression source,
                    Type destinationType,
                    MappingType mappingType);
            }
        }
    }
}