using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using BSRBankWCF.Services;
using BSRBankWCF.Services.Implementations;

namespace BSRBankWCF.DefaultHost
{
    class Program
    {
        /// <summary>
        /// Hosting REST service and SOAP services
        /// </summary>
        /// <param name="args"></param>
        public static void Main( string[] args )
        {
            var baseAddress = "http://192.168.1.100:8080/accounts";
            var uri = new Uri(baseAddress);
            var host = new ServiceHost(typeof(RestService), uri);
            host.AddServiceEndpoint(typeof(IRestService), GetBinding(), "").Behaviors.Add(new WebHttpBehavior());
            host.Open();

            var nextHost = new ServiceHost(typeof(Service1), new Uri("http://localhost:8733/Design_Time_Addresses/BSRBankWCF/Service1/") );
            nextHost.AddServiceEndpoint(typeof(IService1), new BasicHttpBinding(), "");
            nextHost.Open();
            Console.WriteLine("Host open");
            Console.ReadKey();
        }

        /// <summary>
        /// Mapping every request Content type to raw.
        /// </summary>
        public class MyMapper : WebContentTypeMapper
        {
            public override WebContentFormat GetMessageFormatForContentType( string contentType )
            {
                return WebContentFormat.Raw;
            }
        }

        static Binding GetBinding()
        {
            var result = new CustomBinding(new WebHttpBinding());
            var webMEBE = result.Elements.Find<WebMessageEncodingBindingElement>();
            webMEBE.ContentTypeMapper = new MyMapper();
            return result;
        }
    }
}