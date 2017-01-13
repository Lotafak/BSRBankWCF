using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using BSRBankWCF.Services;
using BSRBankWCF.Services.Implementations;

namespace Host
{
    class Program
    {
        public static void Main( string[] args )
        {
            try
            {
                var baseAdress = "http://localhost:8000/Service";
                var host = new ServiceHost(typeof(Service1), new Uri(baseAdress));
                host.AddServiceEndpoint(typeof(IRestService), GetBinding(), "").Behaviors.Add(new WebHttpBehavior());
                host.AddServiceEndpoint(typeof(IService1), new BasicHttpBinding(), "");
                host.Open();
                Console.WriteLine("Host running, press any key to exit. . .");
                Console.ReadKey();
                host.Close();
            }
            catch ( Exception ex )
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }

        public class MyMapper : WebContentTypeMapper
        {
            public override WebContentFormat GetMessageFormatForContentType( string contentType )
            {
                return WebContentFormat.Raw; // always
            }
        }
        static Binding GetBinding()
        {
            CustomBinding result = new CustomBinding(new WebHttpBinding());
            WebMessageEncodingBindingElement webMEBE = result.Elements.Find<WebMessageEncodingBindingElement>();
            webMEBE.ContentTypeMapper = new MyMapper();
            return result;
        }
    }
}
