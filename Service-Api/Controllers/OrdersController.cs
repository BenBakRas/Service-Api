using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Service_Api.BusinessLogicLayer.Interfaces;
using Service_Api.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using log4net.Config;
using log4net.Core;
using log4net;
using System.Reflection;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersData _ordersData;
    private readonly IMapper _mapper;

    public OrdersController(IOrdersData ordersData, IMapper mapper)
    {
        _ordersData = ordersData;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _ordersData.GetAllOrders();
        var orderDtos = _mapper.Map<List<OrdersDto>>(orders);
        return Ok(orderDtos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrdersDto orderDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _ordersData.CreateOrder(orderDto);
                return Ok("Order created successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error creating an Order: " + ex.Message);
                return BadRequest("Error creating an Order");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        try
        {
            var order = await _ordersData.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            var orderDto = _mapper.Map<OrdersDto>(order);
            return Ok(orderDto);
        }
        catch (Exception ex)
        {
            LogError("Error retrieving Order with Id: " + id + " - " + ex.Message);
            return BadRequest("Error finding Order");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, OrdersDto orderDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _ordersData.UpdateOrder(orderDto);
                return Ok("Order updated successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error updating an Order: " + ex.Message);
                return BadRequest("Error updating an Order");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        try
        {
            await _ordersData.DeleteOrder(id);
            return Ok("Order deleted successfully");
        }
        catch (Exception ex)
        {
            // Handle the exception or log it
            LogError("Error deleting an Order: " + ex.Message);
            return BadRequest("Error deleting an Order");
        }
    }

    private void LogError(string message)
    {
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        ILog _logger = LogManager.GetLogger(typeof(LoggerManager));
        _logger.Info(message);
    }
}
