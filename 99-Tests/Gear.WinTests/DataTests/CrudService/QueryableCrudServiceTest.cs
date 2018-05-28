using System;
using System.Collections.Generic;
using System.Linq;
using ir.amkdp.gear.core.Automation.Object.Mapper;
using ir.amkdp.gear.core.Trace;
using ir.amkdp.gear.core.TraceTools.Manipulation;
using ir.amkdp.gear.data;
using ir.amkdp.gear.data.AbstractInterface;
using ir.amkdp.gear.data.AbstractInterface.Templates;

namespace Gear.WinTests.DataTests.CrudService
{
    public class QueryableCrudServiceTest
    {
        static readonly IEnumerable<Person> Persons = TestDataContext.Generate(100, new Random(Environment.TickCount), new TestDataGeneratorSettings
        {
            MaxAgeValue = 40
        })
            .Select(Mapper.Map<Person, TestDataContext>);

        public static void Test()
        {
            var queryable = Persons.AsQueryable();
            
            var crud = new QueryableCrudService<Person, DefaultCrudServiceOptions>(queryable);

            foreach (var entry in crud.FindAll(x => x.Age > 37, x=>x.FullName))
            {
                Logger.Write($"{entry}");
            }
        }
    }
}