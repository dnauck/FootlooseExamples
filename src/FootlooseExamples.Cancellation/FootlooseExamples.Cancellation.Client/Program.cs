using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Footloose;
using FootlooseExamples.Cancellation.Contracts;

namespace FootlooseExamples.Cancellation.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var endpointIdentifier = "footloose-cancellation-client";
            var footlooseConnection = ConfigureFootlooseConnection(endpointIdentifier);

            //register events
            footlooseConnection.ExceptionOccurred += new EventHandler<ExceptionEventArgs>(Footloose_ExceptionOccurred);

            // wait for incoming method calls
            footlooseConnection.Open();
            Console.WriteLine("Footloose started...");
            Console.WriteLine("Uri of this endpoint is: " +
                              footlooseConnection.EndpointIdentityManager.SelfEndpointIdentity.Uri);

            // let try to call a method of ISimpleService on the service with event
            Console.WriteLine("Press Enter to start...");
            Console.ReadLine();

            var serviceUri =
                new Uri(
                    string.Concat("ipc://footloose-cancellation-service@" + Environment.MachineName +
                                  "/footloose-cancellation-service/FootlooseServiceProxy.rem"));

            var cts = new CancellationTokenSource();

            var methodCallTask = footlooseConnection.CallMethod<string>(typeof (ISimpleService), "DoIt", serviceUri,
                                                                        cts.Token);
            Console.WriteLine("Called method 'DoIt' of 'ISimpleService' on '" + serviceUri + "'. CorrelationId is '" +
                              methodCallTask.AsyncState + "'.");

            try
            {
                Console.WriteLine(System.Environment.NewLine);
                Console.WriteLine("Press 'c' to cancel method call or 'ENTER' to continue and wait for method call response!");
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.C)
                    cts.Cancel();

                Console.WriteLine(System.Environment.NewLine);
                Console.WriteLine("Method Call Response: {0}", methodCallTask.Result.ReturnValue);
            }
            catch (AggregateException aggregateException)
            {
                Console.WriteLine(System.Environment.NewLine);
                Console.WriteLine("Exception during method call: {0}", aggregateException.Flatten());
            }
            finally
            {
                footlooseConnection.Close();
                footlooseConnection.Dispose();
                Console.ReadLine();
            }
        }

        static void Footloose_ExceptionOccurred(object sender, ExceptionEventArgs e)
        {
            Console.WriteLine("Exception occured: " + e.Exception);
        }

        private static IFootlooseConnection ConfigureFootlooseConnection(string endpointIdentifier)
        {
            var footloose = Fluently.Configure()
                .SerializerOfType<Footloose.Serialization.TextSerializer>()
                .TransportChannel(Footloose.Configuration.Fluent.RemotingTransportChannelConfiguration.Standard
                                      .EndpointIdentifier(endpointIdentifier) // Uri will be "ipc://<endpointIdentifier>/FootlooseServiceProxy.rem"
                                      .TimeOut(5000)
                )
                .CreateFootlooseConnection();

            return footloose;
        }
    }
}
