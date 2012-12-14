using System;
using System.IO;
using System.Net;
using Footloose;
using Footloose.PublishSubscribe;
using FootlooseExamples.PubSub.Events;

namespace FootlooseExamples.PubSub.Subscriber
{
    class Program
    {
        private static readonly FileInfo licenseFile = new FileInfo("Footloose.lic");
        private static readonly NetworkCredential xmppLoginData = new NetworkCredential("test2", "test2", "localhost");

        static void Main(string[] args)
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
                    "Please wait until you're connected and received events and press ENTER to exist." +
                    Environment.NewLine);
                Console.ReadLine();

                Console.WriteLine("Subscribing to events of type 'TextMessage'...");
                footlooseConnection.SubscribeToEventsOfType<TextMessage>();


                Console.ReadLine();
                footlooseConnection.Close();
            }
        }

        private static IConnection ConfigureConnection()
        {
            return Fluently.Configure()
                           .ServiceLocator(new ServiceLocatorDummy())
                           .SerializerOfType<Footloose.Serialization.BinarySerializer>()
                           .TransportChannel(Footloose.Configuration.Fluent.XmppTransportChannelConfiguration.Standard
                                                      .EndpointIdentifier("Subscriber-" + Guid.NewGuid().ToString().Substring(0, 5))
                                                      .Credentials(xmppLoginData))
                           .CreateConnection(licenseFile);
        }
    }
}