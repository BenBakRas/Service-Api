using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer.Interfaces
{
    public interface IOrderLine
    {
        OrderLine GetOrderLineById(int id);
        List<OrderLine> GetAllOrderLines();
        int CreateOrderLine(OrderLine aOrderLine);
        bool DeleteOrderLineById(int id);
        bool UpdateOrderLineById(OrderLine OrderLineToUpdate);

    }
}
