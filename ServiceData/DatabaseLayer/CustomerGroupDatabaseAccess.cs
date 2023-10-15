using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer
{
    public class CustomerGroupDatabaseAccess : ICustomerGroup
    {
        public int CreateCustomerGroup(CustomerGroup anCustomerGroup)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCustomerGroupById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Discount> GetAllCustomerGroup()
        {
            throw new NotImplementedException();
        }

        public Discount GetCustomerGroupById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCustomerGroupById(CustomerGroup customerGroupToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
