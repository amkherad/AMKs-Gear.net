namespace Gear.UnitTests.TypeResolverTests.Models
{
    public interface ICoffee
    {
        string Name { get; }
        int ReadyTime { get; }
    }


    public class TurkCoffee : ICoffee
    {
        public string Name => "Turk Coffee";
        public int ReadyTime => 5;
    }
    public class FrenchCoffee : ICoffee
    {
        public string Name => "French Coffee";
        public int ReadyTime => 3;
    }
}
