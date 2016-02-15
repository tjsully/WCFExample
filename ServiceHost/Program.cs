using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using UniversityService;
using System.ServiceModel.Description;
using System.ServiceModel.Activation;

namespace ServiceHost
{
    public class Program
    {
        static void Main(string[] args)
        {
            System.ServiceModel.ServiceHost universityDataServiceHost = new System.ServiceModel.ServiceHost(typeof(UniversityDataService));

            var baseAddress = new Uri("net.tcp://localhost:8002/");
            var mexTcpAddress = new Uri("net.tcp://localhost:8000/mex/");
            var mexHttpAddress = new Uri("http://localhost:8733/Design_Time_Addresses/UniversityService/UniversityDataService/mex");

            // add metadata service behaviour
            universityDataServiceHost.Description.Behaviors.Add(
                new ServiceMetadataBehavior { HttpGetEnabled = true, HttpGetUrl = mexHttpAddress });
            
            // when using mexTcpAddress
            //universityDataServiceHost.Description.Behaviors.Add(
            //    new ServiceMetadataBehavior { HttpGetEnabled = false });

            // configure endpoints
            //universityDataServiceHost.AddServiceEndpoint(
            //    typeof(Contracts.IUniversityDataService),
            //    new BasicHttpBinding(),
            //    "http://localhost/");

            universityDataServiceHost.AddServiceEndpoint(
                typeof(IMetadataExchange),
                MetadataExchangeBindings.CreateMexHttpBinding(),
                mexHttpAddress);
            
            universityDataServiceHost.AddServiceEndpoint(
                typeof(Contracts.IUniversityDataService),
                new NetTcpBinding(),
                baseAddress);

            //universityDataServiceHost.AddServiceEndpoint(
            //    typeof(Contracts.IUniversityDataService),
            //    new NetNamedPipeBinding(),
            //    new Uri("net.pipe://localhost/"));


            try
            {
                universityDataServiceHost.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Host Open Error:  " + e.Message);
            }

            Console.WriteLine("University Data Service has been hosted.  Press Enter to stop");
            Console.ReadLine();

            try
            {
                universityDataServiceHost.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Host Close Error:  " + e.Message);
            }


        }
    }
}
