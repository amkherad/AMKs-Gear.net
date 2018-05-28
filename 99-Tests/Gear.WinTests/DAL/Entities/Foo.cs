using ir.amkdp.gear.arch.Data;

namespace Gear.WinTests.DAL.Entities
{
    public class Foo : IIdEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public long Size { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public bool IsEvaluated => Id != 0;
    }
}