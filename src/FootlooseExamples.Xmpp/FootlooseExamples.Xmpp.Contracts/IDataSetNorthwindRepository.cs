using System.Collections.Generic;
using System.Linq;
using System.Text;
using FootlooseExamples.Xmpp.DataModel;

namespace FootlooseExamples.Xmpp.Contracts
{
    public interface IDataSetNorthwindRepository
    {
        Northwind.CustomersDataTable GetCustomers();
    }
}
