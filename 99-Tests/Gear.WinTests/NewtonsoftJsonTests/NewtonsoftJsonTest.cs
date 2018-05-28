using ir.amkdp.gear.core.Data.Serialization;
using ir.amkdp.gear.core.Trace;
using ir.amkdp.gear.web.FancyPack.JqueryDataTables;

namespace Gear.WinTests.NewtonsoftJsonTests
{
    public class NewtonsoftJsonTest
    {
        public static void DoTest()
        {
            var options = new JqueryDataTablesOptions
            {
                AutoWidth = true
            };
           
            Logger.Write(Serializer.Serialize(options, SerializationOptions.JsOptions()));
        }
    }
}