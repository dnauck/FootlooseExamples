using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Footloose;
using FootlooseExamples.Quickstart.Contracts;

namespace FootlooseExamples.Quickstart.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceLocator = new ServiceLocatorDummy();
            var endpointIdentifier = "footloose-quickstart-client";
            var footloose = ConfigureFootloose(serviceLocator, endpointIdentifier);

            //register events
            footloose.ExceptionOccurred += new EventHandler<ExceptionEventArgs>(Footloose_ExceptionOccurred);
            footloose.MethodResponseReceived += new EventHandler<MethodResponseEventArgs>(Footloose_MethodResponseReceived);

            // wait for incoming method calls
            Console.WriteLine("Footloose started...");
            Console.WriteLine("Uri of this endpoint is: " + footloose.EndpointIdentityManager.SelfEndpointIdentity.Uri);

            // let try to call a method of ISimpleService on the service with event
            Console.WriteLine("Press Enter to start...");
            Console.ReadLine();

            var serviceUri = new Uri(string.Concat("ipc://footloose-quickstart-service@" + Environment.MachineName + "/footloose-quickstart-service/FootlooseServiceProxy.rem"));
            var methodCallId = footloose.CallMethod(typeof(ISimpleService), "DoIt", serviceUri);
            Console.WriteLine("Called method 'DoIt' of 'ISimpleService' on '" + serviceUri + "'. CorrelationId is '" +
                              methodCallId + "'.");

            Console.ReadLine();



            // let try to call a method of ISimpleService on the service with callback
            Console.WriteLine("Press Enter to start second run...");
            Console.ReadLine();

            methodCallId = footloose.CallMethod<string, string, string>(typeof(ISimpleService), "DoIt", serviceUri,
                                                         result =>
                                                             {
                                                                 Console.WriteLine("=======" +
                                                                                   Environment.NewLine +
                                                                                   "Incoming method respose: " + result +
                                                                                   Environment.NewLine +
                                                                                   "=======");
                                                             },
                                                         "Argument 1", "Argument 2");
            Console.WriteLine("Called method 'DoIt' of 'ISimpleService' on '" + serviceUri + "'. CorrelationId is '" +
                              methodCallId + "'.");

            Console.ReadLine();
        }

        static void Footloose_MethodResponseReceived(object sender, MethodResponseEventArgs e)
        {
            Console.WriteLine("======================");
            Console.WriteLine("Incoming method respose from: " + e.From);
            Console.WriteLine("Correlation Id: " + e.CorrelationIdentifier);
            Console.WriteLine("Is error: " + (e.Exception != null));
            Console.WriteLine("Return value: " + e.ReturnValue);
            Console.WriteLine("Exception: " + e.Exception);
            Console.WriteLine("======================");
        }

        static void Footloose_ExceptionOccurred(object sender, ExceptionEventArgs e)
        {
            Console.WriteLine("Exception occured: " + e.Exception);
        }

        private static IFootlooseService ConfigureFootloose(ServiceLocatorDummy serviceLocator, string endpointIdentifier)
        {
            var footloose = Fluently.Configure()
                .SerializerOfType<Footloose.Serialization.TextSerializer>()
                .ServiceLocator(serviceLocator)
                .TransportChannel(Footloose.Configuration.Fluent.RemotingTransportChannelConfiguration.Standard
                                      .EndpointIdentifier(endpointIdentifier) // Uri will be "ipc://<endpointIdentifier>/FootlooseServiceProxy.rem"
                                      .TimeOut(5000)
                )
                .CreateFootlooseService();

            return footloose;
        }
    }
}
