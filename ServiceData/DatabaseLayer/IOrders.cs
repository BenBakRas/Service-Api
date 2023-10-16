using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer
{
    public interface IOrders
    {
        Orders GetOrderById(int id);
        List<Orders> GetAllOrders();
        int CreateOrder(Orders aOrder);
        bool DeleteOrderById(int id);
        bool UpdateOrderById(Product orderToUpdate);
    }
}
