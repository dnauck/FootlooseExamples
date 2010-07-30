using System;
using System.Collections.Generic;
using FootlooseExamples.Xmpp.Contracts;
using FootlooseExamples.Xmpp.DataAccess;
using Microsoft.Practices.ServiceLocation;

namespace FootlooseExamples.Xmpp.Service
{
    public class ServiceLocatorDummy : IServiceLocator
    {
        #region Implementation of IServiceProvider

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <returns>
        /// A service object of type <paramref name="serviceType"/>.-or- null if there is no service object of type <paramref name="serviceType"/>.
        /// </returns>
        /// <param name="serviceType">An object that specifies the type of service object to get. </param><filterpriority>2</filterpriority>
        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IServiceLocator

        public object GetInstance(Type serviceType)
        {
            if (serviceType == typeof(IDataSetNorthwindRepository))
                return new DataSetNorthwindRepository();

            throw new Exception("ServiceLocator is unable to find implementation of " + serviceType.FullName);
        }

        public object GetInstance(Type serviceType, string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public TService GetInstance<TService>()
        {
            throw new NotImplementedException();
        }

        public TService GetInstance<TService>(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
