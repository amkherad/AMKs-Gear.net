using AMKsGear.Architecture.Data;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Patterns
{
    public interface IModelEntityBuilder : IModelBuilder { }
    public interface IModelEntityBuilder<TEntity, TModel> : IModelBuilder<TEntity, TModel>, IModelEntityBuilder
        where TEntity : IEntity
        where TModel : IModel
    {
        TEntity ModelToEntity(TModel model, object context);
        TModel EntityToModel(TEntity entity, object context);

        void FillModelFromEntity(TModel model, TEntity entity, object context);
        void FillEntityFromModel(TEntity entity, TModel model, object context);
    }
}