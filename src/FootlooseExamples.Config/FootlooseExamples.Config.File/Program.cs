using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;

namespace FootlooseExamples.Config.File
{
    class Program
    {
        private static readonly IServiceLocator serviceLocator = new ServiceLocatorDummy();

        static void Main(string[] args)
        {
            // create FootlooseConnection instance from App.config settings
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
            var footlooseConnection = Footloose.FootlooseConnectionFactory.CreateFootlooseConnection();
            footlooseConnection.ConnectionStateChanged +=
                (sender, eventArgs) =>
                Console.WriteLine("Footloose connection state changed to: " + eventArgs.ConnectionState);
            footlooseConnection.Open();
            Console.ReadLine();
        }
    }
}
