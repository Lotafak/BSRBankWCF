using System;
using System.ServiceModel;

namespace BSRBankWCF
{
    internal class Host
    {
        private static void Main(string[] args)
        {
            try
            {
                var host = new ServiceHost(typeof(Service1));
                host.Open();
                Console.WriteLine("Host running, press any key to exit. . .");
                Console.ReadKey();
                host.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}