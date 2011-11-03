﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Footloose;
using FootlooseExamples.Cancellation.Contracts;

namespace FootlooseExamples.Cancellation.Service
{
    class Program
    {
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
            Console.WriteLine("Uri of this endpoint is: " + footlooseConnection.EndpointIdentityManager.SelfEndpointIdentity.Uri);
            Console.ReadLine();
            footlooseConnection.Close();
            footlooseConnection.Dispose();
        }

        static void Footloose_ExceptionOccurred(object sender, ExceptionEventArgs e)
        {
            Console.WriteLine("Exception occured: " + e.Exception);
        }

        private static IFootlooseConnection ConfigureFootlooseConnection(ServiceLocatorDummy serviceLocator, string endpointIdentifier)
        {
            var footloose = Fluently.Configure()
                .SerializerOfType<Footloose.Serialization.TextSerializer>()
                .ServiceLocator(serviceLocator)
                .ServiceContracts(contracts => contracts.ServiceContract.RegisterOfType<ISimpleService>())
                .TransportChannel(Footloose.Configuration.Fluent.RemotingTransportChannelConfiguration.Standard
                                      .EndpointIdentifier(endpointIdentifier) // Uri will be "ipc://<endpointIdentifier>/FootlooseServiceProxy.rem"
                                      .TimeOut(5000)
                )
                .CreateFootlooseConnection();

            return footloose;
        }
    }
}