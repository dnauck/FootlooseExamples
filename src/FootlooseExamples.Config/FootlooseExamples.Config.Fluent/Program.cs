using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;

namespace FootlooseExamples.Config.Fluent
{
    class Program
    {
        private static readonly IServiceLocator serviceLocator = new ServiceLocatorDummy();

        static void Main(string[] args)
        {
            // create FootlooseService instance from fluent config API
            var footloose = Footloose.Fluently.Configure()
                .ServiceLocator(serviceLocator)
                .SerializerOfType<Footloose.Serialization.BinarySerializer>()
                .ServiceContracts(
                    contracts => contracts.AutoServiceContract.RegisterFromAssemblyOf<FootlooseExamples.Config.Fluent.Program>()
                        .Where(types => types.Namespace.EndsWith("Contracts"))
                )
                .TransportChannel(
                    Footloose.Configuration.Fluent.RemotingTransportChannelConfiguration.Standard
                        .EndpointIdentifier("MyServiceIdentifier")
                        .UseConfigFile("")
                        .TimeOut(10000)
                )
                .CreateFootlooseService();
                


            Console.WriteLine("Footloose is connected: " + footloose.IsConnected);
            Console.ReadLine();
        }
    }
}
