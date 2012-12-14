using System;
using System.IO;
using System.Net;
using Footloose;
using Footloose.PublishSubscribe;
using FootlooseExamples.PubSub.Events;

namespace FootlooseExamples.PubSub.Publisher
{
    internal class Program
    {
        private static readonly FileInfo licenseFile = new FileInfo("Footloose.lic");
        private static readonly NetworkCredential xmppLoginData = new NetworkCredential("test1", "test1", "localhost");

        private static void Main(string[] args)
        {
            using (var footlooseConnection = ConfigureConnection())
            {
                footlooseConnection.ExceptionOccurred +=
                    (sender, eventArgs) => Console.WriteLine("Exception occurred: {0}", eventArgs.Exception);

                footlooseConnection.ConnectionStateChanged +=
                    (sender, eventArgs) =>
                    Console.WriteLine("Connection state changed to '{0}'.", eventArgs.ConnectionState);

                footlooseConnection.EndpointIdentityManager.PresenceNotificationReceived +=
                    (sender, eventArgs) =>
                    Console.WriteLine("Endpoint '{0}' is now '{1}'.", eventArgs.From.Uri, eventArgs.Presence.Status);


                footlooseConnection.Open();

                Console.WriteLine(
                    "Please wait until you're connected and enter a text to publish or press ENTER to exist." +
                    Environment.NewLine);

                var input = Console.ReadLine();

                while (!string.IsNullOrWhiteSpace(input))
                {
                    var messageToPublish = new TextMessage()
                                               {
                                                   Timestamp = DateTime.Now,
                                                   Message = input
                                               };

                    Console.WriteLine(Environment.NewLine + "Publishing message '{0}: {1}'..." + Environment.NewLine,
                                      messageToPublish.Timestamp.ToShortTimeString(),
                                      messageToPublish.Message);

                    footlooseConnection.PublishEvent<TextMessage>(messageToPublish);

                    Console.WriteLine("Please enter a text to publish or press ENTER to exist." + Environment.NewLine);
                    input = Console.ReadLine();
                }

                footlooseConnection.Close();
            }
        }

        private static IConnection ConfigureConnection()
        {
            return Fluently.Configure()
                           .SerializerOfType<Footloose.Serialization.BinarySerializer>()
                           .ServiceContracts(
                               s => s.ServiceContract.RegisterOfType<Footloose.PublishSubscribe.IPublishEventsOfType<FootlooseExamples.PubSub.Events.TextMessage>>()
                            )
                           .TransportChannel(Footloose.Configuration.Fluent.XmppTransportChannelConfiguration.Standard
                                                      .EndpointIdentifier("Publisher-" + Guid.NewGuid().ToString().Substring(0, 5))
                                                      .Credentials(xmppLoginData))
                           .CreateConnection(licenseFile);
        }
    }
}