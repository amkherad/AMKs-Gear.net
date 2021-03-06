﻿//using AMKsGear.Architecture.Automation.Mapper;
//using AMKsGear.Architecture.Data;
//using AMKsGear.Architecture.Modeling;
//
//namespace AMKsGear.Core.Automation.Mapper
//{
//    public class DefaultModelEntityMapper<TEntity, TModel> : IModelEntityMapper<TEntity, TModel>
//        where TEntity : IEntity//, new()
//        where TModel : IModel//, new()
//    {
//        public virtual TEntity ModelToEntity(TModel model, object context)
//        {
//            var result = TypeResolver.TryCreateInstance<TEntity>();
//            if (result != null)
//            {
//                FillEntityFromModel(result, model, context);
//            }
//            return result;
//        }
//        public virtual TModel EntityToModel(TEntity entity, object context)
//        {
//            var result = TypeResolver.TryCreateInstance<TModel>();
//            if (result != null)
//            {
//                FillModelFromEntity(result, entity, context);
//            }
//            return result;
//        }
//        public virtual void FillModelFromEntity(TModel model, TEntity entity, object context)
//            => Mapper.MapConfig(model, entity, Mapper.Configuration.CaseInsensetiveMapping);
//        public virtual void FillEntityFromModel(TEntity entity, TModel model, object context)
//            => Mapper.MapConfig(entity, model, Mapper.Configuration.CaseInsensetiveMapping);
//
//
//
//        public TModel LeftToRight(TEntity left, object context) => EntityToModel(left, context);
//        public TEntity RightToLeft(TModel right, object context) => ModelToEntity(right, context);
//        public void FillLeftFromRight(TEntity left, TModel right, object context)
//            => FillEntityFromModel(left, right, context);
//        public void FillRightFromLeft(TModel right, TEntity left, object context)
//            => FillModelFromEntity(right, left, context);
//    }
//}