using System.Collections.Generic;
using System.IO;
using System.Linq;
using ir.amkdp.gear.applayer.Globalization.Geo.Iran;
using ir.amkdp.gear.core.Trace;
using MySql.Data.MySqlClient;

namespace Gear.WinTests.CityGenerator
{
    public class CityFileGenerator
    {
        public static void CreateFileFromIranGeoInfo1(string path)
        {
            var connection = new MySqlConnection("Server=localhost; database=irangeoinfo1; UID=root;");
            connection.Open();

            var command = new MySqlCommand("SELECT idLocate, name, lvl, parent FROM locate GROUP BY(name)", connection);
            var dr = command.ExecuteReader();

            var records = new List<IranGeoInfo1>();
            while (dr.Read())
            {
                var record = new IranGeoInfo1();

                try
                {
                    record.Id = dr.GetInt32("idLocate");
                }
                catch { }
                try
                {
                    record.Name = dr.GetString("name");
                }
                catch { }
                try
                {
                    record.ParentId = dr.GetInt32("parent");
                }
                catch { }

                try
                {
                    record.Level = dr.GetInt32("lvl");
                }
                catch { }
                //if (!dr.GetInt32())
                //    record.ParentId = dr.GetInt32("parent");

                records.Add(record);
            }

            Logger.Write(records.Count);

            var allDbStates = records.Where(x => x.Level == 1).ToList();
            var allDbCities = records.Where(x => x.Level == 2).ToList();
            
            var states = IranState.GetProvinces(ProvinceType.Province)
                .Select(x => new
                {
                    Fx = x,
                    Db = allDbStates.FirstOrDefault(d => d.Name == x.ParsiName)
                });
            
            
            using (
                var output = File.OpenWrite(path))
            using (var sw = new StreamWriter(output))
            {
                var names = new List<string>();
                foreach (var r in allDbCities)
                {
                    if (names.Exists(x => x == r.CodeName)) continue;
                    sw.WriteLine($"public const string {r.CodeName}Name = \"{r.Name}\";");
                    names.Add(r.CodeName);
                }

                sw.WriteLine();
                sw.WriteLine();

                names = new List<string>();
                foreach (var r in allDbCities)
                {
                    if (names.Exists(x => x == r.CodeName)) continue;
                    var state = states.FirstOrDefault(s => r.ParentId == s.Db.Id);
                    sw.WriteLine($"public static IranCity {r.CodeName} => new IranCity({r.CodeName}Name, \"{r.Name}\", \"\", IranState.{state.Fx.Name.Replace("-", "")}Name);");
                    names.Add(r.CodeName);
                }

                sw.WriteLine();
                sw.WriteLine();
                
                sw.WriteLine("public static IEnumerable<IranCity> GetCities()");
                sw.WriteLine("{");
                names = new List<string>();
                foreach (var r in allDbCities)
                {
                    if (names.Exists(x => x == r.CodeName)) continue;
                    sw.WriteLine($"yield return {r.CodeName};");
                    names.Add(r.CodeName);
                }
                sw.WriteLine("}");
            }
        }

        public static void Start()
        {
            CreateFileFromIranGeoInfo1(@"D:\cities.cs");
        }
    }

    public class IranGeoInfo1
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public int? Level { get; set; }

        private string _codeName = null;
        public string CodeName
        {
            get
            {
                if (_codeName != null) return _codeName;
                _codeName = Name.Replace(' ', '_');
                var pIndex = _codeName.IndexOf('(');
                if (pIndex >= 0)
                {
                    _codeName = _codeName.Substring(0, pIndex).Trim('_');
                }
                return _codeName;
            }
        }

        public int? ParentId { get; set; }
    }
}