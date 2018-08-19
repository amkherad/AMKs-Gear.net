using AMKsGear.Architecture.Patterns;
using AMKsGear.Core.Automation.Dependency;
using AMKsGear.Core.Automation.Dependency.Configurator;
using AMKsGear.Core.Automation.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMKsGear.MSTests.Core.DependencyContainerTesting
{
    [TestClass]
    public class DependencyContainerConfigTests
    {
        [TestMethod]
        public void ConfigTest()
        {
            var container = new DependencyContainer();

            using (var config = container.Config())
            {
                config.Add<IAdapter>().To<DependencyContainerConfigurator>();
            }
            
        }
    }
}