using Service_Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service_Api.BusinessLogicLayer.Interfaces
{
    public interface IOrdersData
    {
        Task<OrdersDto> GetOrderById(int id);
        Task<List<OrdersDto>> GetAllOrders();
        Task<int> CreateOrder(OrdersDto orderDto);
        Task<bool> UpdateOrder(OrdersDto orderDto);
        Task<bool> DeleteOrder(int id);
    }
}
