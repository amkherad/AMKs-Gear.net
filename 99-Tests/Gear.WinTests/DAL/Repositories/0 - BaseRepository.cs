//using System;
//using ir.amkdp.gear.arch.Data;
//using ir.amkdp.gear.data.orm.entityframework.Repository.CodeFirst;
//using ir.amkdp.gear.data.orm.Entity;
//using ir.amkdp.gear.data.orm.RawWrapper;
//using ir.amkdp.gear.data.orm.Repository;

//namespace Gear.WinTests.DAL.Repositories
//{
//    public class BaseRepository<TEntity> : EFIdRepository<int, TEntity>, IInt32IdRepository<TEntity>
//        where TEntity : class, IEntity, IIdEntity<int>
//    {
//        public BaseRepository(IRawWrapper rawWrapper) : base(rawWrapper)
//        {
//        }

//        public override object Clone()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}