using System;
using System.Linq.Expressions;

namespace AMKsGear.Core.Automation.Mapper
{
    public class MappingCompiledInfo
    {
        public Expression MapExpression { get; internal set; }
        public Expression NewMapExpression { get; internal set; }
        
        public Action<object, object> MapFunction { get; internal set; }

    }
}