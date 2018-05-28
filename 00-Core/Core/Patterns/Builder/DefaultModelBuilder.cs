using AMKsGear.Architecture.Modeling;
using AMKsGear.Architecture.Patterns;
using AMKsGear.Core.Automation.IoC;
using AMKsGear.Core.Automation.Object.Mapper;

namespace AMKsGear.Core.Patterns.Builder
{
    public class DefaultModelBuilder<TLeft, TRight> : IModelBuilder<TLeft, TRight>
        where TLeft : IModel//, new()
        where TRight : IModel//, new()
    {
        public virtual TRight LeftToRight(TLeft left, object context)
        {
            var result = TypeResolver.TryCreateInstance<TRight>();
            if (result != null)
            {
                FillRightFromLeft(result, left, context);
            }
            return result;
        }
        public virtual TLeft RightToLeft(TRight right, object context)
        {
            var result = TypeResolver.TryCreateInstance<TLeft>();
            if (result != null)
            {
                FillLeftFromRight(result, right, context);
            }
            return result;
        }

        public virtual void FillLeftFromRight(TLeft left, TRight right, object context)
            => Mapper.MapConfig(left, right, DefaultMapper.Configuration.CaseInsensetiveMapping);
        public virtual void FillRightFromLeft(TRight right, TLeft left, object context)
            => Mapper.MapConfig(right, left, DefaultMapper.Configuration.CaseInsensetiveMapping);
    }
}