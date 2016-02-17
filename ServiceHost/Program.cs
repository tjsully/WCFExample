using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using UniversityData;
using UniversityService;
using System.ServiceModel.Description;
using System.ServiceModel.Activation;
using Castle.Windsor;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Facilities.WcfIntegration;

namespace ServiceHost
{
    public class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = new Uri("net.tcp://localhost:8002/");
            var mexTcpAddress = new Uri("net.tcp://localhost:8001/mex/");
            var mexHttpAddress = new Uri("http://localhost:8733/Design_Time_Addresses/UniversityService/UniversityDataService/mex");

            // set up DI container
            IWindsorContainer container = new WindsorContainer();
            container.Kernel.AddFacility<WcfFacility>();
            container.Kernel.Register(
                Component.For<IUniversityDataRepository>().ImplementedBy<UniversityDataRepository>(),
                Component.For<IUniversityDataService>().ImplementedBy<UniversityDataService>()
                );
          
            

            // create service host
            var serviceHost = new Castle.Facilities.WcfIntegration.DefaultServiceHostFactory().CreateServiceHost(typeof(IUniversityDataService).AssemblyQualifiedName, new Uri[] {baseAddress});
            //System.ServiceModel.ServiceHost universityDataServiceHost = new System.ServiceModel.ServiceHost(typeof(UniversityDataService));


            // add metadata service behaviour
            //universityDataServiceHost.Description.Behaviors.Add(
            //    new ServiceMetadataBehavior { HttpGetEnabled = true, HttpGetUrl = mexHttpAddress });

            // when using mexTcpAddress
            serviceHost.Description.Behaviors.Add(
                new ServiceMetadataBehavior { HttpGetEnabled = false });

            // configure endpoints
            //universityDataServiceHost.AddServiceEndpoint(
            //    typeof(Contracts.IUniversityDataService),
            //    new BasicHttpBinding(),
            //    "http://localhost/");

            serviceHost.AddServiceEndpoint(
                "IMetadataExchange",
                MetadataExchangeBindings.CreateMexTcpBinding(),
                mexTcpAddress);

            serviceHost.AddServiceEndpoint(
                "Contracts.IUniversityDataService",
                new NetTcpBinding(),
                baseAddress);

            //serviceHost.AddServiceEndpoint(
            //    typeof(IMetadataExchange),
            //    MetadataExchangeBindings.CreateMexTcpBinding(),
            //    mexTcpAddress);

            //serviceHost.AddServiceEndpoint(
            //    typeof(Contracts.IUniversityDataService),
            //    new NetTcpBinding(),
            //    baseAddress);

            //universityDataServiceHost.AddServiceEndpoint(
            //    typeof(Contracts.IUniversityDataService),
            //    new NetNamedPipeBinding(),
            //    new Uri("net.pipe://localhost/"));

           
                

            //IComponentRegistration registration;
            //if (!container.ComponentRegistry.TryGetRegistration(new TypedService(typeof(IUniversityDataService)), out registration))
            //{
            //    Console.WriteLine("The service contract has not been registered in the container");
            //    Console.ReadLine();
            //    Environment.Exit(-1);
            //}

            //ServiceImplementationData sid = new ServiceImplementationData();
            //sid.ServiceTypeToHost = typeof(UniversityDataService);

            try
            {
                serviceHost.Open();
                Console.WriteLine("University Data Service has been hosted.  Press Enter to stop");
            }
            catch (Exception e)
            {
                Console.WriteLine("Host Open Error:  " + e.Message);
            }

            Console.ReadLine();

            try
            {
                serviceHost.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Host Close Error:  " + e.Message);
            }
            finally
            {
                if (container != null)
                    container.Dispose();
                Environment.Exit(0);
            }

        }
    }
}
