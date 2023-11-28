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
        private readonly IOrders _ordersGroupDatabaseAccess;  // Updated interface name
        private readonly IMapper _mapper;

        public OrdersDataControl(IOrders ordersGroupDatabaseAccess, IMapper mapper)  // Updated interface name
        {
            _ordersGroupDatabaseAccess = ordersGroupDatabaseAccess;
            _mapper = mapper;
        }

        public async Task<OrdersDto> GetOrderById(int id)
        {
            var orders = await _ordersGroupDatabaseAccess.GetOrderById(id);  // Updated method name
            return _mapper.Map<OrdersDto>(orders);
        }

        public async Task<List<OrdersDto>> GetAllOrders()
        {
            var ordersList = await _ordersGroupDatabaseAccess.GetAllOrders();  // Updated method name
            return _mapper.Map<List<OrdersDto>>(ordersList);
        }

        public async Task<int> CreateOrder(OrdersDto orderDto)
        {
            var order = _mapper.Map<Orders>(orderDto);  // Updated class name
            return await _ordersGroupDatabaseAccess.CreateOrder(order);  // Updated method name
        }

        public async Task<bool> UpdateOrder(OrdersDto orderDto)
        {
            var order = _mapper.Map<Orders>(orderDto);  // Updated class name
            return await _ordersGroupDatabaseAccess.UpdateOrderById(order);  // Updated method name
        }

        public async Task<bool> DeleteOrder(int id)
        {
            return await _ordersGroupDatabaseAccess.DeleteOrderById(id);// Updated method name
        }
    }
}
