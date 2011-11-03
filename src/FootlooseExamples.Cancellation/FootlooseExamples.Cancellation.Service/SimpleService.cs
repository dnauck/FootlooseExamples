using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FootlooseExamples.Cancellation.Contracts;

namespace FootlooseExamples.Cancellation.Service
{
    public class SimpleServiceImpl : ISimpleService
    {
        #region Implementation of ISimpleService

        public string DoIt()
        {
            var s = string.Format("Method '{0}' was called.", MethodBase.GetCurrentMethod());
            Console.WriteLine(s);
            Console.WriteLine("Doing some calculations that takes 10 sec.");
            System.Threading.Thread.Sleep(10000);
            Console.WriteLine("Calculations done! Return response now...");
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
