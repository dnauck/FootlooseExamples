using System;
using System.Collections.Generic;
using FootlooseExamples.PubSub.Events;
using Microsoft.Practices.ServiceLocation;

namespace FootlooseExamples.PubSub.Subscriber
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

        /// <summary>
        /// Get an instance of the given <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">Type of object requested.</param><exception cref="T:Microsoft.Practices.ServiceLocation.ActivationException">if there is an error resolving
        ///             the service instance.</exception>
        /// <returns>
        /// The requested service instance.
        /// </returns>
        public object GetInstance(Type serviceType)
        {
            if (serviceType == typeof(Footloose.PublishSubscribe.IHandleEventsOfType<TextMessage>))
                return new TextMessageHandler();

            throw new Exception("ServiceLocator is unable to find implementation of " + serviceType.FullName);
        }

        /// <summary>
        /// Get an instance of the given named <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">Type of object requested.</param><param name="key">Name the object was registered with.</param><exception cref="T:Microsoft.Practices.ServiceLocation.ActivationException">if there is an error resolving
        ///             the service instance.</exception>
        /// <returns>
        /// The requested service instance.
        /// </returns>
        public object GetInstance(Type serviceType, string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all instances of the given <paramref name="serviceType"/> currently
        ///             registered in the container.
        /// </summary>
        /// <param name="serviceType">Type of object requested.</param><exception cref="T:Microsoft.Practices.ServiceLocation.ActivationException">if there is are errors resolving
        ///             the service instance.</exception>
        /// <returns>
        /// A sequence of instances of the requested <paramref name="serviceType"/>.
        /// </returns>
        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get an instance of the given <typeparamref name="TService"/>.
        /// </summary>
        /// <typeparam name="TService">Type of object requested.</typeparam><exception cref="T:Microsoft.Practices.ServiceLocation.ActivationException">if there is are errors resolving
        ///             the service instance.</exception>
        /// <returns>
        /// The requested service instance.
        /// </returns>
        public TService GetInstance<TService>()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get an instance of the given named <typeparamref name="TService"/>.
        /// </summary>
        /// <typeparam name="TService">Type of object requested.</typeparam><param name="key">Name the object was registered with.</param><exception cref="T:Microsoft.Practices.ServiceLocation.ActivationException">if there is are errors resolving
        ///             the service instance.</exception>
        /// <returns>
        /// The requested service instance.
        /// </returns>
        public TService GetInstance<TService>(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all instances of the given <typeparamref name="TService"/> currently
        ///             registered in the container.
        /// </summary>
        /// <typeparam name="TService">Type of object requested.</typeparam><exception cref="T:Microsoft.Practices.ServiceLocation.ActivationException">if there is are errors resolving
        ///             the service instance.</exception>
        /// <returns>
        /// A sequence of instances of the requested <typeparamref name="TService"/>.
        /// </returns>
        public IEnumerable<TService> GetAllInstances<TService>()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
