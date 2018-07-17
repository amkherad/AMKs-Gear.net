using System;
using AMKsGear.Architecture;
using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Core.Automation.Object.Mapper.Annotations
{
    [AttributeUsage(ConstantTable.AllValueMembers)]
    public class MapperEvaluateAttribute : OrderedAttribute
    {
        internal readonly Type InternalSource;
        internal readonly Func<object, object> InternalEvalExpression;

        public Type Source => InternalSource;
        public Func<object, object> EvaluateExpression => InternalEvalExpression;

        public MapperEvaluateAttribute(Func<object, object> evaluateExpression)
        {
            //Source = null;
            InternalEvalExpression = evaluateExpression;
        }
        public MapperEvaluateAttribute(Type source, Func<object, object> evaluateExpression)
        {
            InternalSource = source;
            InternalEvalExpression = evaluateExpression;
        }
    }
}