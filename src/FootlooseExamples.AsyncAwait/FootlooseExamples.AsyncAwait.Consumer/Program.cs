using System;
using System.IO;
using System.Threading;
using FootlooseExamples.AsyncAwait.Shared;

namespace FootlooseExamples.AsyncAwait.Consumer
{
    internal class Program
    {
        private static readonly FileInfo licenseFile = new FileInfo("Footloose.lic");

        private static void Main(string[] args)
        {
            using (var footlooseConnection = Footloose.Fluently.Configure()
                .UseSerializerOfType<Footloose.Serialization.TextSerializer>()
                .UseTransportChannel(Footloose.Configuration.Fluent.IpcTransportChannelConfiguration.Standard)
                .CreateConnection(licenseFile))
            {

                footlooseConnection.ExceptionOccurred +=
                    (sender, eventArgs) => Console.WriteLine("Exception occurred: {0}", eventArgs.Exception);

                footlooseConnection.Open();

                Console.WriteLine("Footloose started...");
                Console.WriteLine("Uri of this endpoint is: " +
                                  footlooseConnection.EndpointIdentityManager.SelfEndpointIdentity.Uri);
                Console.WriteLine("Press Enter to start...");
                Console.ReadLine();

                DoAsync(footlooseConnection);

                Console.WriteLine("Waiting for incoming result... Press ENTER to exit...");
                Console.ReadLine();


                footlooseConnection.Close();
            }
        }

        private static async void DoAsync(Footloose.IConnection footlooseConnection)
        {
            // generate uri of the WeatherInfo Service
            var userName = Environment.UserName;
            var mashineName = Environment.MachineName;
            var serviceEndpointIdentifier = "footloose-asyncawaitservice";
            var serviceUri = footlooseConnection.UriBuilder.BuildEndpointUri(userName, mashineName,
                                                                             serviceEndpointIdentifier);

            var cs = new CancellationTokenSource();

            var requestDto1 = new RequestDto() { InputValue = 1 };
            var result =
                await footlooseConnection
                          .Invoke<IServiceContract, ResponseDto>(service => service.HandleRequest(requestDto1),
                                                                 cs.Token,
                                                                 serviceUri);

            Console.WriteLine("Received result 1: It is {0}!", result.ReturnValue.OutputValue);
            
            var requestDto2 = new RequestDto() { InputValue = 2 };
            var result2 =
                await footlooseConnection
                          .Invoke<IServiceContract, ResponseDto>(service => service.HandleRequest(requestDto2),
                                                                 cs.Token,
                                                                 serviceUri);

            Console.WriteLine("Received result 2: It is {0}!", result2.ReturnValue.OutputValue);
        }
    }
}