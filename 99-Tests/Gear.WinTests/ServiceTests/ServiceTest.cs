using System;
using ir.amkdp.gear.services.ServiceModel;
using ir.amkdp.gear.services.wcf.Manipulation;

namespace Gear.WinTests.ServiceTests
{
    public class ServiceTest
    {
        public static void Test()
        {
            var context = new ServiceContext("MainService", "Description", "http://www.localhost.com", "svc");

            context.AddMethod(new ServiceMethod
            {
                Name = "Method1",
                Description = "Description"
            });

            var svc = context.CreateWcfService("http://www.localhost.com", new Uri("http://localhost:8001"));
            svc.Start();
        }
    }
}