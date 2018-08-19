using System;
using System.Diagnostics.Contracts;
using AMKsGear.Architecture.Data;
using AMKsGear.Core.Automation.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMKsGear.MSTests.Core.MapperTesting
{
    [TestClass]
    public class MapperTests
    {
        [TestMethod]
        public void ConfigTest()
        {
            XX(null);
        }

        public void XX(Mapping row)
        {
            Contract.Requires<ArgumentNullException>(row != null, "row");
            
        }
    }
}