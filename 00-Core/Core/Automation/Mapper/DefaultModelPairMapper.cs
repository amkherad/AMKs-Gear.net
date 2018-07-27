//using AMKsGear.Architecture.Automation.IoC;
//using AMKsGear.Architecture.Automation.Mapper;
//using AMKsGear.Architecture.Modeling;
//
//namespace AMKsGear.Core.Automation.Mapper
//{
//    public class DefaultModelPairMapper<TLeft, TRight> : IModelPairMapper<TLeft, TRight>
//        where TLeft : IModel//, new()
//        where TRight : IModel//, new()
//    {
//        public ITypeResolver TypeResolver { get; }
//        
//        public virtual TRight LeftToRight(TLeft left, object context)
//        {
//            var result = TypeResolver.TryCreateInstance<TRight>();
//            if (result != null)
//            {
//                FillRightFromLeft(result, left, context);
//            }
//            return result;
//        }
//        public virtual TLeft RightToLeft(TRight right, object context)
//        {
//            var result = TypeResolver.TryCreateInstance<TLeft>();
//            if (result != null)
//            {
//                FillLeftFromRight(result, right, context);
//            }
//            return result;
//        }
//
//        public virtual void FillLeftFromRight(TLeft left, TRight right, object context)
//            => Mapper.MapConfig(left, right, Mapper.Configuration.CaseInsensetiveMapping);
//        public virtual void FillRightFromLeft(TRight right, TLeft left, object context)
//            => Mapper.MapConfig(right, left, Mapper.Configuration.CaseInsensetiveMapping);
//    }
//}