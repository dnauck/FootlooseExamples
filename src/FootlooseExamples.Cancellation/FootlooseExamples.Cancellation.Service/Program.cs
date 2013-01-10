using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Footloose;
using FootlooseExamples.Cancellation.Contracts;

namespace FootlooseExamples.Cancellation.Service
{
    class Program
    {
        private static readonly FileInfo licenseFile = new FileInfo("Footloose.lic");

        static void Main(string[] args)
        {
            var serviceLocator = new ServiceLocatorDummy();
            var endpointIdentifier = "footloose-cancellation-service";
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
            Console.WriteLine("Exception occurred: " + e.Exception);
        }

        private static IConnection ConfigureFootlooseConnection(ServiceLocatorDummy serviceLocator, string endpointIdentifier)
        {
            var footloose = Fluently.Configure()
                .UseSerializerOfType<Footloose.Serialization.TextSerializer>()
                .UseServiceLocator(serviceLocator)
                .WithServiceContracts(contracts => contracts.WithServiceContract.RegisterOfType<ISimpleService>())
                .UseTransportChannel(Footloose.Configuration.Fluent.IpcTransportChannelConfiguration.Standard
                                      .WithTimeOut(5000)
                )
                .WithEndpointIdentifier(endpointIdentifier) // Uri will be "ipc://user@mashineName/<EndpointIdentifier>"
                .CreateConnection(licenseFile);

            return footloose;
        }
    }
}
