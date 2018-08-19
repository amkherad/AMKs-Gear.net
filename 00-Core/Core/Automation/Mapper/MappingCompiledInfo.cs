using System;
using System.Linq.Expressions;

namespace AMKsGear.Core.Automation.Mapper
{
    public class MappingCompiledInfo
    {
        public LambdaExpression ObjectMapExpression { get; internal set; }
        public LambdaExpression ProjectionExpression { get; internal set; }
        
        public Action<object, object> MapFunction { get; internal set; }

    }
}