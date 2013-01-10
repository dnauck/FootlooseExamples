using System;
using System.IO;
using FootlooseExamples.WeatherInfo.Shared;

namespace FootlooseExamples.WeatherInfo.Service
{
    internal class Program
    {
        private static readonly FileInfo licenseFile = new FileInfo("Footloose.lic");

        private static void Main(string[] args)
        {
            var serviceLocator = new ServiceLocatorDummy();

            using (var footlooseConnection = Footloose.Fluently.Configure()
                .UseSerializerOfType<Footloose.Serialization.TextSerializer>()
                .UseServiceLocator(serviceLocator)
                .WithServiceContracts(contracts => contracts.WithServiceContract.RegisterOfType<IWeatherInfoService>())
                .UseTransportChannel(
                    Footloose.Configuration.Fluent.IpcTransportChannelConfiguration.Standard
                )
                .WithEndpointIdentifier("footloose-weatherinfoservice")
                .CreateConnection(licenseFile))
            {

                footlooseConnection.ExceptionOccurred +=
                    (sender, eventArgs) => Console.WriteLine("Exception occurred: {0}", eventArgs.Exception);

                footlooseConnection.Open();

                Console.WriteLine("Footloose Connection is now listening on: {0}",
                                    footlooseConnection.EndpointIdentityManager.SelfEndpointIdentity.LocalEndpoint.Uri);

                Console.WriteLine("Press ENTER to exit...");
                Console.ReadLine();

                footlooseConnection.Close();
            }
        }
    }
}