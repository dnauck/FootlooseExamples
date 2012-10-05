using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            var footlooseConfig = Footloose.Configuration.FootlooseConfigurationSectionGroup.GetConfiguration();

            // configure serializer
            var serializerConfig = new SerializerConfigurationElement()
                                       {
                                           Name = "MyTextSerializer",
                                           Type = typeof (Footloose.Serialization.TextSerializer).AssemblyQualifiedName
                                       };

            footlooseConfig.Serializer.DefaultSerializer = "MyTextSerializer";
            footlooseConfig.Serializer.Types.Clear();
            footlooseConfig.Serializer.Types.Add(serializerConfig);

            
            // configure service contracts that should be exposed to other services
            var serviceContractConfig1 = new ServiceContractConfigurationElement()
                                             {
                                                 Type = typeof (FootlooseExamples.Config.ByCode.Contracts.IService1).AssemblyQualifiedName
                                             };

            var serviceContractConfig2 = new ServiceContractConfigurationElement()
                                             {
                                                 Type = typeof (FootlooseExamples.Config.ByCode.Contracts.IService1).AssemblyQualifiedName
                                             };

            footlooseConfig.ServiceContracts.Types.Add(serviceContractConfig1);
            footlooseConfig.ServiceContracts.Types.Add(serviceContractConfig2);


            // configure transport channel
            var transportChannelConfig = new IpcTransportChannelConfigurationElement()
                                             {
                                                 Name = "MyTransportChannel",
                                                 Type = typeof (Footloose.TransportChannels.Ipc.IpcTransportChannel).AssemblyQualifiedName
                                             };

            footlooseConfig.TransportChannel.DefaultTransportChannel = "MyTransportChannel";
            footlooseConfig.TransportChannel.TransportChannels.Clear();
            footlooseConfig.TransportChannel.TransportChannels.Add(transportChannelConfig);
            footlooseConfig.TransportChannel.EndpointIdentifier = "MyServiceIdentifier";


            // create FootlooseConnection instance from Configuration
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
            var footlooseConnection = Footloose.FootlooseConnectionFactory.CreateFootlooseConnection(footlooseConfig, licenseFile);
            footlooseConnection.ConnectionStateChanged +=
                (sender, eventArgs) =>
                Console.WriteLine("Footloose connection state changed to: " + eventArgs.ConnectionState);
            footlooseConnection.Open();
            Console.ReadLine();
        }
    }
}
