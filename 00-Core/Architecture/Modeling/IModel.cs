using AMKsGear.Architecture.Data;

namespace AMKsGear.Architecture.Modeling
{
    public interface IModel
    {

    }
    public interface IBoundedModel<TBoundedEntity> : IModel
        where TBoundedEntity : IEntity
    {
        void Fill(TBoundedEntity entity);
        TBoundedEntity MapToEntity();
    }
}