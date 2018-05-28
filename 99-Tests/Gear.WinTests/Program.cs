using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Gear.WinTests.ServiceTests;
using ir.amkdp.gear.core.Trace;
using ir.amkdp.gear.desktop.win.Trace;

namespace Gear.WinTests
{
    public class Program
    {
        static void Main(string[] args)
        {
            Logger.RegisterLogger(new ConsoleLogger());
            Logger.RegisterLogger(new FileDumpLogger(@"L:\log.txt", Encoding.UTF8));
            
            //AssemblyActivator.ExecuteAll(AppDomain.CurrentDomain.GetAssemblies(), null);
            //
            //var dt = PersianDate.Parse("1394/9/17").ToGregorianDateTime().ToString();
            //Logger.Write(dt);
            //
            //var t = typeof(NewtonsoftJsonSerializer);
            //GC.KeepAlive(t);

            //CityFileGenerator.Start();

            //MapperTest.DoTest();
            ServiceTest.Test();
            //TypeResolverTest.Test();
            //NewtonsoftJsonTest.DoTest();
            //MapperTest.DoTest();
            //QueryableCrudServiceTest.Test();
            //byte[] bytePass = Encoding.Unicode.GetBytes("A.M.K. Website Creator haha you hacked me!");
            //byte[] byteSalt = Convert.FromBase64String("andDhiSISmySUALT");
            //byte[] byteResult = new byte[byteSalt.Length + bytePass.Length];
            //
            //Buffer.BlockCopy(byteSalt, 0, byteResult, 0, byteSalt.Length);
            //Buffer.BlockCopy(bytePass, 0, byteResult, byteSalt.Length, bytePass.Length);
            //
            //HashAlgorithm ha = HashAlgorithm.Create("SHA1");
            //Logger.Write(Convert.ToBase64String(ha.ComputeHash(byteResult)));

            //var ret = new RetryHelper(3);
            //while (!ret.IsDone())
            //{
            //    Logger.Write("Failed");
            //    ret.Fail();
            //}

            Console.ReadKey();
        }
    }
}