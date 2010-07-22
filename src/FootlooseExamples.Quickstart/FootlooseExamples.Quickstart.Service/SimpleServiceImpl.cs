using System;
using System.Reflection;
using FootlooseExamples.Quickstart.Contracts;

namespace FootlooseExamples.Quickstart.Service
{
    public class SimpleServiceImpl : ISimpleService
    {
        #region Implementation of ISimpleService

        public string DoIt()
        {
            var s = string.Format("Method '{0}' was called.", MethodBase.GetCurrentMethod());
            Console.WriteLine(s);
            return s;
        }

        public string DoIt(string param1)
        {
            var s = string.Format("Method '{0}' with parameter '{1}' was called.", MethodBase.GetCurrentMethod(),
                                  param1);
            Console.WriteLine(s);
            return s;
        }

        public string DoIt(string param1, string param2)
        {
            var s = string.Format("Method '{0}' with parameter '{1}'/'{2}' was called.", MethodBase.GetCurrentMethod(),
                                  param1, param2);
            Console.WriteLine(s);
            return s;
        }

        public T DoIt<T>(T param1)
        {
            Console.WriteLine(string.Format("Method '{0}' with parameter '{1}' of type '{2}' was called.", MethodBase.GetCurrentMethod(),
                                            param1, typeof(T).FullName));

            return param1;
        }

        #endregion
    }
}