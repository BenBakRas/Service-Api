using Service_Api.DTOs;

namespace Service_Api.BusinessLogicLayer.Interfaces
{
    public interface IOrderLineData
    {
        Task<OrderLineDto> GetOrderLineById(int id);
        Task<List<OrderLineDto>> GetAllOrderLines();
        Task<int> CreateOrderLine(OrderLineDto orderLineDto);
        Task<bool> UpdateOrderLine(OrderLineDto orderLineDto);
        Task<bool> DeleteOrderLine(int id);
    }
}
