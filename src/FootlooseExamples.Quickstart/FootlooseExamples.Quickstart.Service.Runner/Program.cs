using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Footloose;
using Footloose.Contracts;
using FootlooseExamples.Quickstart.Contracts;

namespace FootlooseExamples.Quickstart.Service.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceLocator = new ServiceLocatorDummy();
            var endpointIdentifier = "Footloose-Quickstart-Service";
            var footloose = ConfigureFootloose(serviceLocator, endpointIdentifier);

            //register events
            footloose.ExceptionOccurred += new EventHandler<ExceptionEventArgs>(Footloose_ExceptionOccurred);
            
            // wait for incoming method calls
            Console.WriteLine("Footloose started... [Press Enter to exit]");
            Console.WriteLine("Uri of this endpoint is: ipc://" + endpointIdentifier + "/FootlooseServiceProxy.rem");
            Console.ReadLine();
        }

        static void Footloose_ExceptionOccurred(object sender, ExceptionEventArgs e)
        {
            Console.WriteLine("Exception occured: " + e.Exception);
        }

        private static IFootlooseService ConfigureFootloose(ServiceLocatorDummy serviceLocator, string endpointIdentifier)
        {
            var footloose = Fluently.Configure()
                .SerializerOfType<Footloose.Serialization.BinarySerializer>()
                .ServiceLocator(serviceLocator)
                .ServiceContracts(contracts =>
                {
                    //single registration
                    contracts.ServiceContract.RegisterOfType<ISimpleService>();

                    //other example; automaticly register all public interfaces that are in the "*.Contracts" namespace
                    contracts.AutoServiceContract.RegisterFromAssemblyOf<ISimpleService>().
                        Where(
                            type =>
                            type.IsInterface &&
                            type.IsPublic &&
                            type.Namespace.EndsWith("Contracts"));
                })
                .TransportChannel(Footloose.Configuration.RemotingTransportChannelConfiguration.Standard
                                      .EndpointIdentifier(endpointIdentifier) // Uri will be "ipc://<endpointIdentifier>/FootlooseServiceProxy.rem"
                                      .TimeOut(5000)
                )
                .CreateFootlooseService();

            return footloose;
        }
    }
}
