namespace Gear.UnitTests.TypeResolverTests.Models
{
    public interface ISundry
    {
        string Name { get; }

        decimal Price { get; }
    }

    public class CoffeeCream : ISundry
    {
        public string Name => "Cream";
        public decimal Price => 4000;
    }
    public class CoffeeMilk : ISundry
    {
        public string Name => "Milk";
        public decimal Price => 1000;
    }
}
