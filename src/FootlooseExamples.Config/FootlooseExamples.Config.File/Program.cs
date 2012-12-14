using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;

namespace FootlooseExamples.Config.File
{
    class Program
    {
        private static readonly FileInfo licenseFile = new FileInfo("Footloose.lic");

        private static readonly IServiceLocator serviceLocator = new ServiceLocatorDummy();

        static void Main(string[] args)
        {
            // create FootlooseConnection instance from App.config settings
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
            var footlooseConnection = Footloose.ConnectionFactory.CreateConnection(licenseFile);
            footlooseConnection.ConnectionStateChanged +=
                (sender, eventArgs) =>
                Console.WriteLine("Footloose connection state changed to: " + eventArgs.ConnectionState);
            footlooseConnection.Open();
            Console.ReadLine();
        }
    }
}
