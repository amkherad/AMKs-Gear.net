using System;
using System.Linq;
using System.Security.Principal;
using AMKsGear.Core.Patterns.AppModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMKsGear.MSTests.Core.AppModelTesting
{

    public interface IXX
    {
        void XX();
    }

    public class TTX : IXX
    {
        public void XX()
        {
            Console.Write("XX called");
        }
    }
    
    [TestClass]
    public class AppModelContextTest
    {
        [TestMethod]
        public void TestStorageContext()
        {
            var app = new AppModelContext();
            
            app.AddValues<IXX>(new [] { new TTX() });

            var val = app.GetValues<IXX>();
            Assert.AreEqual(val.Count(), 1);
        }
    }
}