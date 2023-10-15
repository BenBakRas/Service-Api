using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer.Interfaces
{
    public interface ICustomerGroup
    {
        Discount GetCustomerGroupById(int id);
        List<Discount> GetAllCustomerGroup();
        int CreateCustomerGroup(CustomerGroup anCustomerGroup);
        bool DeleteCustomerGroupById(int id);
        bool UpdateCustomerGroupById(CustomerGroup customerGroupToUpdate);


    }
}
