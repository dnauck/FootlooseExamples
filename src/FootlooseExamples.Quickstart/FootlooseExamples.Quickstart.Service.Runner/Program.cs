using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Footloose;
using FootlooseExamples.Quickstart.Contracts;

namespace FootlooseExamples.Quickstart.Service.Runner
{
    class Program
    {
        private static readonly FileInfo licenseFile = new FileInfo("Footloose.lic");

        static void Main(string[] args)
        {
            var serviceLocator = new ServiceLocatorDummy();
            var endpointIdentifier = "footloose-quickstart-service";
            var footlooseConnection = ConfigureFootlooseConnection(serviceLocator, endpointIdentifier);

            //register events
            footlooseConnection.ExceptionOccurred += new EventHandler<ExceptionEventArgs>(Footloose_ExceptionOccurred);
            
            // wait for incoming method calls
            footlooseConnection.Open();
            Console.WriteLine("Footloose started... [Press Enter to exit]");
            Console.WriteLine("Uri of this endpoint is: " + footlooseConnection.EndpointIdentityManager.SelfEndpointIdentity.LocalEndpoint.Uri);
            Console.ReadLine();
            footlooseConnection.Close();
            footlooseConnection.Dispose();
        }

        static void Footloose_ExceptionOccurred(object sender, ExceptionEventArgs e)
        {
            Console.WriteLine("Exception occured: " + e.Exception);
        }

        private static IConnection ConfigureFootlooseConnection(ServiceLocatorDummy serviceLocator, string endpointIdentifier)
        {
            var footloose = Fluently.Configure()
                .UseSerializerOfType<Footloose.Serialization.TextSerializer>()
                .UseServiceLocator(serviceLocator)
                .WithServiceContracts(contracts =>
                {
                    //single registration
                    contracts.WithServiceContract.RegisterOfType<ISimpleService>();

                    //other example; automatically register all public interfaces that are in the "*.Contracts" namespace
                    contracts.WithAutoServiceContract.RegisterFromAssemblyOf<ISimpleService>().
                        Where(
                            type =>
                            type.IsInterface &&
                            type.IsPublic &&
                            type.Namespace.EndsWith("Contracts"));
                })
                .UseTransportChannel(Footloose.Configuration.Fluent.IpcTransportChannelConfiguration.Standard
                                      .WithTimeOut(5000)
                )
                .WithEndpointIdentifier(endpointIdentifier) // Uri will be "ipc://user@host/<endpointIdentifier>"
                .CreateConnection(licenseFile);

            return footloose;
        }
    }
}
