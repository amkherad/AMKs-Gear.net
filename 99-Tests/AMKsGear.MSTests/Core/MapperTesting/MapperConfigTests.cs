using AMKsGear.Architecture.Data;
using AMKsGear.Core.Automation.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMKsGear.MSTests.Core.MapperTesting
{
    [TestClass]
    public class MapperConfigTests
    {
        public class Customer
        {
            public string Name { get; set; }
        }

        public class Order
        {
            public string Title { get; set; }
            public Customer Customer { get; set; }
        }

        public class OrderDto
        {
            public string CustomerName { get; set; }
            public string Title { get; set; }
        }

        [TestMethod]
        public void ConfigTest()
        {
            var mapper = new Mapper();

            using (var config = mapper.Config())
            {
                config.CreateMap<Order, OrderDto>()
                    .TwoWay()
                    .UseFlattering()
                    //OR: .AddBindingPath(x => x.Customer.Name, x => x.CustomerName)
                    ;
            }
            
            mapper.Compile();
        }
    }
}