using AMKsGear.Architecture.Data;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Automation.Mapper
{
    public interface IModelEntityMapper<TEntity, TModel> : IModelPairMapper<TEntity, TModel>
        where TEntity : IEntity
        where TModel : IModel
    {
        TEntity ModelToEntity(TModel model, object context);
        TModel EntityToModel(TEntity entity, object context);

        void FillModelFromEntity(TModel model, TEntity entity, object context);
        void FillEntityFromModel(TEntity entity, TModel model, object context);
    }
}