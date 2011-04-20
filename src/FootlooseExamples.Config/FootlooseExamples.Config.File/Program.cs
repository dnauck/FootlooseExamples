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
            // create FootlooseService instance from App.config settings
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
            var footloose = Footloose.FootlooseServiceFactory.CreateFootlooseService();
            Console.WriteLine("Footloose is connected: " + footloose.IsConnected);
            Console.ReadLine();
        }
    }
}
