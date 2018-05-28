//using System.Data.Entity;
//using ir.amkdp.gear.arch.Data;
//using ir.amkdp.gear.data.orm.entityframework.RawWrapper;
//using ir.amkdp.gear.data.orm.RawWrapper;
//using ir.amkdp.gear.data.orm.Repository;

//namespace Gear.WinTests.DAL
//{
//    public class RawWrapper : EfRawWrapper, IUoW
//    {
//        private readonly IRepositoryFactory _repoFactory = new UnitOfWorkRepositoryFactory();

//        public RawWrapper()
//            : base(new DataContext())
//        { }
//        internal RawWrapper(DbContext context)
//            : base(context)
//        { }

//        public DbContext GetDbContext() { return this.Context; }
//        public override IRepositoryFactory RepositoryFactory => _repoFactory;

//        class UnitOfWorkRepositoryFactory : IRepositoryFactory
//        {
//            public void RegisterRepository<TRepository, TEntity>()
//                where TRepository : IRepository<TEntity>
//                where TEntity : class, IEntity
//            {
//                //Do nothing, Alternate pattern.
//            }

//            public IRepository<TEntity> CreateRepository<TRepository, TEntity>() where TRepository : IRepository<TEntity> where TEntity : class, IEntity
//            {
//                return null;
//            }
//            public IRepository CreateRepository<TRepository>() where TRepository : IRepository
//            {
//                return null;
//            }
//            public void Release(IRepository repository)
//            {
//                //IoCContainer.Container.Teardown(repository);
//            }
//        }
//    }
//}