using System;
using System.IO;
using Microsoft.Practices.ServiceLocation;

namespace FootlooseExamples.Config.Fluent
{
    class Program
    {
        private static readonly FileInfo licenseFile = new FileInfo("Footloose.lic");

        private static readonly IServiceLocator serviceLocator = new ServiceLocatorDummy();

        static void Main(string[] args)
        {
            // create FootlooseConnection instance from fluent config API
            var footlooseConnection = Footloose.Fluently.Configure()
                .UseServiceLocator(serviceLocator)
                .UseSerializerOfType<Footloose.Serialization.TextSerializer>()
                .WithServiceContracts(
                    contracts => contracts.WithAutoServiceContract.RegisterFromAssemblyOf<FootlooseExamples.Config.Fluent.Program>()
                        .Where(types => types.Namespace.EndsWith("Contracts"))
                )
                .UseTransportChannel(
                    Footloose.Configuration.Fluent.IpcTransportChannelConfiguration.Standard
                        .WithTimeOut(5000)
                )
                .WithEndpointIdentifier("MyServiceIdentifier")
                .CreateConnection(licenseFile);

            footlooseConnection.ConnectionStateChanged +=
                (sender, eventArgs) =>
                Console.WriteLine("Footloose connection state changed to: " + eventArgs.ConnectionState);

            footlooseConnection.Open();
            Console.ReadLine();
        }
    }
}
