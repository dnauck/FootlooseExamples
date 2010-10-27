using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Footloose.Configuration;
using Microsoft.Practices.ServiceLocation;

namespace FootlooseExamples.Config.ByCode
{
    class Program
    {
        private static readonly IServiceLocator serviceLocator = new ServiceLocatorDummy();

        static void Main(string[] args)
        {
            // create configuration instance
            var footlooseConfig = Footloose.Configuration.FootlooseConfigurationSectionGroup.GetConfiguration();

            // configure serializer
            var serializerConfig = new SerializerConfigurationElement()
                                       {
                                           Name = "MyBinarySerializer",
                                           Type = typeof (Footloose.Serialization.BinarySerializer).AssemblyQualifiedName
                                       };

            footlooseConfig.Serializer.DefaultSerializer = "MyBinarySerializer";
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
            var transportChannelConfig = new RemotingTransportChannelConfigurationElement()
                                             {
                                                 Name = "MyTransportChannel",
                                                 Type = typeof (Footloose.TransportChannels.Remoting.RemotingTransportChannel).AssemblyQualifiedName,
                                                 RemotingConfigFile = ""
                                             };

            footlooseConfig.TransportChannel.DefaultTransportChannel = "MyTransportChannel";
            footlooseConfig.TransportChannel.TransportChannels.Clear();
            footlooseConfig.TransportChannel.TransportChannels.Add(transportChannelConfig);
            footlooseConfig.TransportChannel.EndpointIdentifier = "MyServiceIdentifier";


            // create FootlooseService instance from Configuration
            var footloose = Footloose.FootlooseServiceFactory.CreateFootlooseService(serviceLocator, footlooseConfig);
            Console.WriteLine("Footloose is connected: " + footloose.IsConnected);
            Console.ReadLine();
        }
    }
}
