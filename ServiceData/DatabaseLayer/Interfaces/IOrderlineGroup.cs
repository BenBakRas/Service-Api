using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer.Interfaces
{
    public interface IOrderlineGroup
    {
        Task<OrderlineGroup> GetOrderlineGroupById(int id);
        Task<List<OrderlineGroup>> GetAllOrderlineGroups();
        Task<int> CreateOrderlineGroup(OrderlineGroup orderlineGroup);
        Task<bool> DeleteOrderlineGroupById(int id);
        Task<bool> UpdateOrderlineGroupById(OrderlineGroup orderlineGroupToUpdate);
    }
}
