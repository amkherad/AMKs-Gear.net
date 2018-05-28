using ir.amkdp.gear.arch.Data;

namespace Gear.WinTests.DataTests
{
    public class Person : IEntity
    {
        public string FullName { get; set; }
        public int Age { get; set; }

        public bool IsEvaluated => true;
    }
}