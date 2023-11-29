using AutoMapper;
using Service_Api.BusinessLogicLayer.Interfaces;
using Service_Api.DTOs;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service_Api.BusinessLogicLayer
{
    public class OrderLineDataControl : IOrderLineData
    {
        private readonly IOrderLine _orderLineDatabaseAccess;  // Updated interface name
        private readonly IMapper _mapper;

        public OrderLineDataControl(IOrderLine orderLineDatabaseAccess, IMapper mapper)  // Updated interface name
        {
            _orderLineDatabaseAccess = orderLineDatabaseAccess;
            _mapper = mapper;
        }

        public async Task<OrderLineDto> GetOrderLineById(int id)
        {
            var orderLine = await _orderLineDatabaseAccess.GetOrderLineById(id);
            return _mapper.Map<OrderLineDto>(orderLine);
        }

        public async Task<List<OrderLineDto>> GetAllOrderLines()
        {
            var orderLines = await _orderLineDatabaseAccess.GetAllOrderLines();
            return _mapper.Map<List<OrderLineDto>>(orderLines);
        }

        public async Task<int> CreateOrderLine(OrderLineDto orderLineDto)
        {
            var orderLine = _mapper.Map<OrderLine>(orderLineDto);
            return await _orderLineDatabaseAccess.CreateOrderLine(orderLine);
        }

        public async Task<bool> UpdateOrderLine(OrderLineDto orderLineDto)
        {
            var orderLine = _mapper.Map<OrderLine>(orderLineDto);
            return await _orderLineDatabaseAccess.UpdateOrderLineById(orderLine);
        }

        public async Task<bool> DeleteOrderLine(int id)
        {
            return await _orderLineDatabaseAccess.DeleteOrderLineById(id);
        }

        public async Task<OrderlineGroupDto> GetOrderlineGroupByOrderlineId(int orderlineId)
        {
            var orderlineGroup = await _orderLineDatabaseAccess.GetOrderlineGroupByOrderlineId(orderlineId);
            return _mapper.Map<OrderlineGroupDto>(orderlineGroup);
        }

    }
}
