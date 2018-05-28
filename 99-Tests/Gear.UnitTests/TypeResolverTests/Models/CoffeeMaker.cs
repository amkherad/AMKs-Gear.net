using System.Collections.Generic;
using System.Linq;
using ir.amkdp.gear.arch.Trace;
using ir.amkdp.gear.core.Trace;

namespace Gear.UnitTests.TypeResolverTests.Models
{
    public interface ICoffeeMaker
    {
        void Make(ILoggerEngine logger);
    }
    public class CoffeeMaker : ICoffeeMaker
    {
        public ICoffee Coffee { get; }
        public IEnumerable<ISundry> Sundries { get; }

        public CoffeeMaker(ICoffee coffee, IEnumerable<ISundry> sundries)
        {
            Coffee = coffee;
            Sundries = sundries;
        }

        public void Make(ILoggerEngine logger)
        {
            logger.Write(
                $"Making '{Coffee.Name}' in '{Coffee.ReadyTime}' Minutes with '{string.Join(", ", Sundries.Select(x => x.Name))}'");
        }
    }
}