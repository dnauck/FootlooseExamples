using System;
using System.IO;
using Footloose.Configuration;
using Microsoft.Practices.ServiceLocation;

namespace FootlooseExamples.Config.ByCode
{
    class Program
    {
        private static readonly FileInfo licenseFile = new FileInfo("Footloose.lic");

        private static readonly IServiceLocator serviceLocator = new ServiceLocatorDummy();

        static void Main(string[] args)
        {
            // create configuration instance
            var footlooseConfig = new Footloose.Configuration.FootlooseConfiguration();

            footlooseConfig.EndpointIdentifier = "MyServiceIdentifier";

            // configure serializer
            footlooseConfig.Serializer = typeof (Footloose.Serialization.TextSerializer);

            
            // configure service contracts that should be exposed to other services
            footlooseConfig.ServiceContracts.Add(typeof (FootlooseExamples.Config.ByCode.Contracts.IService1));
            footlooseConfig.ServiceContracts.Add(typeof (FootlooseExamples.Config.ByCode.Contracts.IService2));

            // configure transport channel
            footlooseConfig.TransportChannelConfiguration = new IpcTransportChannelConfiguration();


            // create FootlooseConnection instance from Configuration
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
            var footlooseConnection = Footloose.ConnectionFactory.CreateConnection(footlooseConfig, licenseFile);
            footlooseConnection.ConnectionStateChanged +=
                (sender, eventArgs) =>
                Console.WriteLine("Footloose connection state changed to: " + eventArgs.ConnectionState);
            footlooseConnection.Open();
            Console.ReadLine();
        }
    }
}