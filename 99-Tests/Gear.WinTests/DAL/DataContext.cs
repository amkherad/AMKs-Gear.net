//using System.Data.Entity;
//using System.Data.Entity.Migrations;
//using Gear.WinTests.DAL.Entities;

//namespace Gear.WinTests.DAL
//{
//    public class DataContext : DbContext
//    {
//        public DataContext() : base("DefaultConnection") { }

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, ContextDbMigrationsConfiguration>());

//            base.OnModelCreating(modelBuilder);
//        }

//        public DbSet<Foo> Foos { get; set; }
//    }

//    public class ContextDbMigrationsConfiguration : DbMigrationsConfiguration<DataContext>
//    {
//        public ContextDbMigrationsConfiguration()
//        {
//            AutomaticMigrationsEnabled = true;
//            AutomaticMigrationDataLossAllowed = true;
//        }
//    }
//}
