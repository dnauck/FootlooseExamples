using System;
using System.Data;
using FootlooseExamples.Xmpp.Contracts;
using FootlooseExamples.Xmpp.DataModel;

namespace FootlooseExamples.Xmpp.DataAccess
{
    public class DataSetNorthwindRepository : IDataSetNorthwindRepository
    {
        private static readonly Northwind dataSet = new Northwind();

        static DataSetNorthwindRepository()
        {
            dataSet.ReadXml("Northwind.Data.xml", XmlReadMode.Auto);
        }

        public DataSetNorthwindRepository()
        {}

        #region Implementation of IDataSetNorthwindRepository

        public Northwind.CustomersDataTable GetCustomers()
        {
            return dataSet.Customers;
        }

        #endregion
    }
}