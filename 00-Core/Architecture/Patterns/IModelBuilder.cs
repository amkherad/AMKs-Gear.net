using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Patterns
{
    public interface IModelBuilder { }
    public interface IModelBuilder<TLeft, TRight> : IModelBuilder
        where TLeft : IModel
        where TRight : IModel
    {
        TRight LeftToRight(TLeft left, object context);
        TLeft RightToLeft(TRight right, object context);

        void FillLeftFromRight(TLeft left, TRight right, object context);
        void FillRightFromLeft(TRight right, TLeft left, object context);
    }
}