using AutoMapper;
using Service_Api.BusinessLogicLayer.Interfaces;
using Service_Api.DTOs;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service_Api.BusinessLogicLayer
{
    public class OrdersDataControl : IOrdersData
    {
        private readonly IOrderlineGroup _orderlineGroupDatabaseAccess;
        private readonly IMapper _mapper;

        public OrdersDataControl(IOrderlineGroup orderlineGroupDatabaseAccess, IMapper mapper)
        {
            _orderlineGroupDatabaseAccess = orderlineGroupDatabaseAccess;
            _mapper = mapper;
        }

        public async Task<OrdersDto> GetOrderById(int id)
        {
            var orders = await _orderlineGroupDatabaseAccess.GetOrderlineGroupById(id);
            return _mapper.Map<OrdersDto>(orders);
        }

        public async Task<List<OrdersDto>> GetAllOrders()
        {
            var ordersList = await _orderlineGroupDatabaseAccess.GetAllOrderlineGroups();
            return _mapper.Map<List<OrdersDto>>(ordersList);
        }

        public async Task<int> CreateOrder(OrdersDto orderDto)
        {
            var order = _mapper.Map<OrderlineGroup>(orderDto);
            return await _orderlineGroupDatabaseAccess.CreateOrderlineGroup(order);
        }

        public async Task<bool> UpdateOrder(OrdersDto orderDto)
        {
            var order = _mapper.Map<OrderlineGroup>(orderDto);
            return await _orderlineGroupDatabaseAccess.UpdateOrderlineGroupById(order);
        }

        public async Task<bool> DeleteOrder(int id)
        {
            return await _orderlineGroupDatabaseAccess.DeleteOrderlineGroupById(id);
        }
    }
}
