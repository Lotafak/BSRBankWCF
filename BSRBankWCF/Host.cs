using System;
using System.ServiceModel;

namespace BSRBankWCF
{
    class Host
    {
        static void Main(String[] args)
        {
            try
            {
                ServiceHost host = new ServiceHost(typeof(Service1));
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
