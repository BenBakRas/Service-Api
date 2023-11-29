using Service_Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service_Api.BusinessLogicLayer.Interfaces
{
    public interface IOrderlineGroupData
    {
        Task<OrderlineGroupDto> GetOrderlineGroupById(int orderlineID, int productId, int comboId);
        Task<List<OrderlineGroupDto>> GetAllOrderlineGroups();
        Task CreateOrderlineGroup(OrderlineGroupDto orderlineGroupDto);
        Task<bool> UpdateOrderlineGroup(OrderlineGroupDto orderlineGroupDto);
        Task<bool> DeleteOrderlineGroup(int orderlineId, int productId, int comboId);
    }
}
