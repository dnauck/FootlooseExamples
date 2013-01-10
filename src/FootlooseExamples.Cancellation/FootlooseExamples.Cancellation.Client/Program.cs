using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Footloose;
using FootlooseExamples.Cancellation.Contracts;

namespace FootlooseExamples.Cancellation.Client
{
    class Program
    {
        private static readonly FileInfo licenseFile = new FileInfo("Footloose.lic");

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
                              footlooseConnection.EndpointIdentityManager.SelfEndpointIdentity.LocalEndpoint.Uri);

            // let try to call a method of ISimpleService on the service with event
            Console.WriteLine("Press Enter to start...");
            Console.ReadLine();

            var userName = Environment.UserName;
            var mashineName = Environment.MachineName;
            var serviceEndpointIdentifier = "footloose-cancellation-service";
            var serviceUri = footlooseConnection.UriBuilder.BuildEndpointUri(userName, mashineName,
                                                                             serviceEndpointIdentifier);
            var cts = new CancellationTokenSource();

            var methodCallTask = footlooseConnection.Invoke<ISimpleService, string>(s => s.DoIt(), cts.Token,
                                                                                        serviceUri);

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
            Console.WriteLine("Exception occurred: " + e.Exception);
        }

        private static IConnection ConfigureFootlooseConnection(string endpointIdentifier)
        {
            var footloose = Fluently.Configure()
                .UseSerializerOfType<Footloose.Serialization.TextSerializer>()
                .UseTransportChannel(Footloose.Configuration.Fluent.IpcTransportChannelConfiguration.Standard
                                      .WithTimeOut(5000)
                )
                .WithEndpointIdentifier(endpointIdentifier) // Uri will be "ipc://user@mashineName/<EndpointIdentifier>"
                .CreateConnection(licenseFile);

            return footloose;
        }
    }
}
