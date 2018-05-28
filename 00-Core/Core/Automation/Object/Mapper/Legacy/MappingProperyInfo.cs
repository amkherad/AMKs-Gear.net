using System;
using System.Linq;
using System.Reflection;
using AMKsGear.Architecture.Modeling;
using AMKsGear.Architecture.Modeling.Annotations;
using AMKsGear.Core.Automation.Object.Mapper.Annotations;

namespace AMKsGear.Core.Automation.Object.Mapper
{
    public class MappingProperyInfo
    {
        public readonly IModelMemberInfo MemberInfo;
        public readonly string Name;
        public readonly Type PropertyType;
        public readonly TypeInfo PropertyTypeInfo;
        public readonly Type ObjectType;

        public readonly Attribute[] CustomAttributes;

        public Func<object, object> CastExpression;
        public bool PassNullsToCast;

        public MapperEvaluateAttribute Evaluation;
        public Func<object, object> EvaluateExpression;

        public MappingProperyInfo(IModelMemberInfo pInfo, Type objType)
        {
            MemberInfo = pInfo;
            ObjectType = objType;
            PropertyType = pInfo.Type.GetPureType();
            PropertyTypeInfo = PropertyType.GetTypeInfo();

            CustomAttributes = pInfo.GetCustomAttributes(true).ToArray();

            Name =
                (CustomAttributes.FirstOrDefault(x => x is NameAttribute) as NameAttribute)
                    ?.Name ?? pInfo.Name;

            //var evals = CustomAttributes.Where(x => x is MapperEvaluateAttribute);
        }

        public bool IsEvaluationRequired(Type srcType, object source)
        {
            var evals = CustomAttributes.OfType<MapperEvaluateAttribute>()
                .OrderByDescending(x => x.Order).ToList();

            var eval = evals.FirstOrDefault(x => x.InternalSource == srcType);

            if (eval == null)
            {
                if (evals.Count == 0) return false;

                eval = evals.FirstOrDefault();
            }
            
            Evaluation = eval;
            EvaluateExpression = eval.EvaluateExpression;

            return true;
        }
    }
}