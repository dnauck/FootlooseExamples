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
            // create FootlooseConnection instance from fluent config API
            var footlooseConnection = Footloose.Fluently.Configure()
                .ServiceLocator(serviceLocator)
                .SerializerOfType<Footloose.Serialization.TextSerializer>()
                .ServiceContracts(
                    contracts => contracts.AutoServiceContract.RegisterFromAssemblyOf<FootlooseExamples.Config.Fluent.Program>()
                        .Where(types => types.Namespace.EndsWith("Contracts"))
                )
                .TransportChannel(
                    Footloose.Configuration.Fluent.RemotingTransportChannelConfiguration.Standard
                        .EndpointIdentifier("MyServiceIdentifier")
                        .UseConfigFile("")
                        .TimeOut(5000)
                )
                .CreateFootlooseConnection();


            footlooseConnection.Open();
            Console.WriteLine("Footloose is connected: " + footlooseConnection.IsConnected);
            Console.ReadLine();
        }
    }
}
