using Microsoft.Web.Administration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testIIS
{
    class Program
    {
        static void Main(string[] args)
        {
/*            DirectoryEntry getEntity = new DirectoryEntry("IIS://localhost/W3SVC/INFO");
            string Version = getEntity.Properties["MajorIISVersionNumber"].Value.ToString();
            Console.Out.WriteLine("IIS版本为:" + Version);
            Console.Out.WriteLine("");

            Console.Out.WriteLine("Is App Pool 'test' exist?");
            Console.Out.WriteLine(IISUtil.DoesExistAppPool("test"));
            Console.Out.WriteLine("");

            List<string> poolnames=IISUtil.GetAllAppPoolNames();

            foreach (string name in poolnames)
            {
                Console.Out.WriteLine("All pool name");
                Console.Out.WriteLine(name + " status:"+IISUtil.getAppPoolSatus(name));
            }

            Console.Out.WriteLine("");

            Console.Out.WriteLine("get app pool test status");
            Console.Out.WriteLine(IISUtil.getAppPoolSatus("test"));

            Console.Out.WriteLine("start app pool test");
            IISUtil.StartAppPool("test");

            for (int i = 0; i < 10; i++)
            {
                Console.Out.WriteLine(IISUtil.getAppPoolSatus("test"));
                System.Threading.Thread.Sleep(1000);
            }

            Console.Out.WriteLine("get app pool testttt status");
            Console.Out.WriteLine(IISUtil.getAppPoolSatus("testttt"));

            Console.Out.WriteLine("stop app pool testttt");
            IISUtil.StopAppPool("testttt");
            for (int i = 0; i < 10; i++)
            {
                Console.Out.WriteLine(IISUtil.getAppPoolSatus("testttt"));
                System.Threading.Thread.Sleep(1000);
            }*/

           /* Console.Out.WriteLine("get app pool testForRecyle status");
            Console.Out.WriteLine(IISUtil.getAppPoolSatus("testForRecyle"));

            Console.Out.WriteLine("Recycle app pool testForRecyle");
            Console.Out.WriteLine(IISUtil.RecycleAppPool("testForRecyle"));
            Console.Out.WriteLine("");
            Console.Out.WriteLine("state");
            for (int i = 0; i < 10; i++)
            {
                Console.Out.WriteLine(IISUtil.getAppPoolSatus("testForRecyle"));
                System.Threading.Thread.Sleep(1000);
            }*/

            ApplicationPool appPool = IISUtil.getAppPool("test");
            Console.ReadLine();
        }
    }
}
