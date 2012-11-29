using System;
using System.IO;
using FootlooseExamples.WeatherInfo.Shared;

namespace FootlooseExamples.WeatherInfo.Client
{
    internal class Program
    {
        private static readonly FileInfo licenseFile = new FileInfo("Footloose.lic");

        private static void Main(string[] args)
        {
            using (var footlooseConnection = Footloose.Fluently.Configure()
                                                      .SerializerOfType<Footloose.Serialization.TextSerializer>()
                                                      .TransportChannel(
                                                          Footloose.Configuration.Fluent
                                                                   .IpcTransportChannelConfiguration.Standard)
                                                      .CreateFootlooseConnection(licenseFile))
            {

                footlooseConnection.ExceptionOccurred +=
                    (sender, eventArgs) => Console.WriteLine("Exception occurred: {0}", eventArgs.Exception);

                footlooseConnection.Open();

                Console.WriteLine("Footloose started...");
                Console.WriteLine("Uri of this endpoint is: " +
                                  footlooseConnection.EndpointIdentityManager.SelfEndpointIdentity.Uri);
                Console.WriteLine("Press Enter to start...");
                Console.ReadLine();

                // generate uri of the WeatherInfo Service
                var userName = Environment.UserName;
                var mashineName = Environment.MachineName;
                var serviceEndpointIdentifier = "footloose-weatherinfoservice";
                var serviceUri = footlooseConnection.UriBuilder.BuildEndpointUri(userName, mashineName,
                                                                                 serviceEndpointIdentifier);

                var weatherInfoRequest = new WeatherInfoRequest() {City = "Berlin"};

                footlooseConnection
                    .Invoke<IWeatherInfoService, WeatherInfoResponse>(
                        service => service.HandleRequest(weatherInfoRequest),

                        result =>
                        Console.WriteLine("Received result: It is {0} in {1}!",
                                          result.ReturnValue.Temperature, result.ReturnValue.City),

                        serviceUri);

                Console.WriteLine("Waiting for incoming result... Press ENTER to exit...");
                Console.ReadLine();

                footlooseConnection.Close();
            }
        }
    }
}