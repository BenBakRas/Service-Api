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
        private readonly IOrderlineGroup _orderlineGroupDatabaseAccess;
        private readonly IMapper _mapper;

        public OrderLineDataControl(IOrderlineGroup orderlineGroupDatabaseAccess, IMapper mapper)
        {
            _orderlineGroupDatabaseAccess = orderlineGroupDatabaseAccess;
            _mapper = mapper;
        }

        public async Task<OrderLineDto> GetOrderLineById(int id)
        {
            var orderLine = await _orderlineGroupDatabaseAccess.GetOrderlineGroupById(id);
            return _mapper.Map<OrderLineDto>(orderLine);
        }

        public async Task<List<OrderLineDto>> GetAllOrderLines()
        {
            var orderLines = await _orderlineGroupDatabaseAccess.GetAllOrderlineGroups();
            return _mapper.Map<List<OrderLineDto>>(orderLines);
        }

        public async Task<int> CreateOrderLine(OrderLineDto orderLineDto)
        {
            var orderLine = _mapper.Map<OrderlineGroup>(orderLineDto);
            return await _orderlineGroupDatabaseAccess.CreateOrderlineGroup(orderLine);
        }

        public async Task<bool> UpdateOrderLine(OrderLineDto orderLineDto)
        {
            var orderLine = _mapper.Map<OrderlineGroup>(orderLineDto);
            return await _orderlineGroupDatabaseAccess.UpdateOrderlineGroupById(orderLine);
        }

        public async Task<bool> DeleteOrderLine(int id)
        {
            return await _orderlineGroupDatabaseAccess.DeleteOrderlineGroupById(id);
        }
    }
}
