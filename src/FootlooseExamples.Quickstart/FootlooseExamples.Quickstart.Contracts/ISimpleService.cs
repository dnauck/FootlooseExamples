using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootlooseExamples.Quickstart.Contracts
{
    public interface ISimpleService
    {
        string DoIt();
        string DoIt(string param1);
        string DoIt(string param1, string param2);
        T DoIt<T>(T param1);
    }
}
