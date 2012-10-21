using System;
using System.IO;
using FootlooseExamples.AsyncAwait.Shared;

namespace FootlooseExamples.AsyncAwait.Service
{
    internal class Program
    {
        private static readonly FileInfo licenseFile = new FileInfo("Footloose.lic");


        private static void Main(string[] args)
        {

            var serviceLocator = new ServiceLocatorDummy();

            using (var footlooseConnection = Footloose.Fluently.Configure()
                .SerializerOfType<Footloose.Serialization.TextSerializer>()
                .ServiceLocator(serviceLocator)
                .ServiceContracts(contracts => contracts.ServiceContract.RegisterOfType<IServiceContract>())
                .TransportChannel(
                    Footloose.Configuration.Fluent.IpcTransportChannelConfiguration.Standard
                        .EndpointIdentifier("footloose-asyncawaitservice")
                )
                .CreateFootlooseConnection(licenseFile))
            {

                footlooseConnection.ExceptionOccurred +=
                    (sender, eventArgs) => Console.WriteLine("Exception occurred: {0}", eventArgs.Exception);

                footlooseConnection.Open();

                Console.WriteLine("Footloose Connection is now listening on: {0}",
                                  footlooseConnection.EndpointIdentityManager.SelfEndpointIdentity.Uri);

                Console.WriteLine("Press ENTER to exit...");
                Console.ReadLine();

                footlooseConnection.Close();
            }
        }
    }
}