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
            footlooseConnection.Open();
            Console.WriteLine("Footloose is connected: " + footlooseConnection.IsConnected);
            Console.ReadLine();
        }
    }
}
