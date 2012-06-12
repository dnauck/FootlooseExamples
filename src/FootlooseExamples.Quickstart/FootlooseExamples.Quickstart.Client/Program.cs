using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Footloose;
using FootlooseExamples.Quickstart.Contracts;

namespace FootlooseExamples.Quickstart.Client
{
    class Program
    {
        private static readonly FileInfo licenseFile = new FileInfo("Footloose.lic");

        static void Main(string[] args)
        {
            var serviceLocator = new ServiceLocatorDummy();
            var endpointIdentifier = "footloose-quickstart-client";
            var footlooseConnection = ConfigureFootlooseConnection(serviceLocator, endpointIdentifier);

            //register events
            footlooseConnection.ExceptionOccurred += new EventHandler<ExceptionEventArgs>(Footloose_ExceptionOccurred);
            footlooseConnection.MethodResponseReceived += new EventHandler<MethodResponseEventArgs>(Footloose_MethodResponseReceived);

            // wait for incoming method calls
            footlooseConnection.Open();
            Console.WriteLine("Footloose started...");
            Console.WriteLine("Uri of this endpoint is: " + footlooseConnection.EndpointIdentityManager.SelfEndpointIdentity.Uri);

            // let try to call a method of ISimpleService on the service with event
            Console.WriteLine("Press Enter to start...");
            Console.ReadLine();

            var userName = Environment.UserName;
            var mashineName = Environment.MachineName;
            var serviceEndpointIdentifier = "footloose-quickstart-service";
            var serviceUri = footlooseConnection.UriBuilder.BuildCommunicationEndpointUri(userName, mashineName,
                                                                                          serviceEndpointIdentifier);

            var methodCallId = footlooseConnection.CallMethod<ISimpleService>(service => service.DoIt(), serviceUri);
            Console.WriteLine("Called method 'DoIt' of 'ISimpleService' on '" + serviceUri + "'. CorrelationId is '" +
                              methodCallId + "'.");

            Console.ReadLine();



            // let try to call a method of ISimpleService on the service with callback
            Console.WriteLine("Press Enter to start second run...");
            Console.ReadLine();

            methodCallId = footlooseConnection.CallMethod<string, ISimpleService>(
                            serice => serice.DoIt("Argument 1", "Argument 2"),
                            response => Console.WriteLine("=======" +
                                                        Environment.NewLine +
                                                        "Incoming method respose: " + response.ReturnValue +
                                                        Environment.NewLine +
                                                        "======="),
                            serviceUri
                            );

            Console.WriteLine("Called method 'DoIt' of 'ISimpleService' on '" + serviceUri + "'. CorrelationId is '" +
                              methodCallId + "'.");

            Console.ReadLine();

            footlooseConnection.Close();
            footlooseConnection.Dispose();
        }

        static void Footloose_MethodResponseReceived(object sender, MethodResponseEventArgs e)
        {
            Console.WriteLine("======================");
            Console.WriteLine("Incoming method respose from: " + e.MethodResponse.From);
            Console.WriteLine("Correlation Id: " + e.MethodResponse.CorrelationIdentifier);
            Console.WriteLine("Is error: " + (e.MethodResponse.Exception != null));
            Console.WriteLine("Return value: " + e.MethodResponse.ReturnValue);
            Console.WriteLine("Exception: " + e.MethodResponse.Exception);
            Console.WriteLine("======================");
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
                .TransportChannel(Footloose.Configuration.Fluent.RemotingTransportChannelConfiguration.Standard
                                      .EndpointIdentifier(endpointIdentifier) // Uri will be "ipc://user@host/<endpointIdentifier>"
                                      .TimeOut(5000)
                )
                .CreateFootlooseConnection(licenseFile);

            return footloose;
        }
    }
}
