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
public class OrderLineController : ControllerBase
{
    private readonly IOrderLineData _orderLineData;
    private readonly IMapper _mapper;

    public OrderLineController(IOrderLineData orderLineData, IMapper mapper)
    {
        _orderLineData = orderLineData;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrderLines()
    {
        var orderLines = await _orderLineData.GetAllOrderLines();
        var orderLineDtos = _mapper.Map<List<OrderLineDto>>(orderLines);
        return Ok(orderLineDtos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderLine(OrderLineDto orderLineDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _orderLineData.CreateOrderLine(orderLineDto);
                return Ok("OrderLine created successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error creating an OrderLine: " + ex.Message);
                return BadRequest("Error creating an OrderLine");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderLineById(int id)
    {
        try
        {
            var orderLine = await _orderLineData.GetOrderLineById(id);
            if (orderLine == null)
            {
                return NotFound();
            }
            var orderLineDto = _mapper.Map<OrderLineDto>(orderLine);
            return Ok(orderLineDto);
        }
        catch (Exception ex)
        {
            LogError("Error retrieving OrderLine with Id: " + id + " - " + ex.Message);
            return BadRequest("Error finding OrderLine");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderLine(int id, OrderLineDto orderLineDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _orderLineData.UpdateOrderLine(orderLineDto);
                return Ok("OrderLine updated successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error updating an OrderLine: " + ex.Message);
                return BadRequest("Error updating an OrderLine");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderLine(int id)
    {
        try
        {
            await _orderLineData.DeleteOrderLine(id);
            return Ok("OrderLine deleted successfully");
        }
        catch (Exception ex)
        {
            // Handle the exception or log it
            LogError("Error deleting an OrderLine: " + ex.Message);
            return BadRequest("Error deleting an OrderLine");
        }
    }

    [HttpGet("GetOrderlineGroup/{orderlineId}")]
    public async Task<IActionResult> GetOrderLineGroupByOrderlineId(int orderlineId)
    {
        try
        {
            var orderlineGroup = await _orderLineData.GetOrderlineGroupByOrderlineId(orderlineId);
            if (orderlineGroup == null)
            {
                return NotFound();
            }
            var orderlineGroupDto = _mapper.Map<OrderlineGroupDto>(orderlineGroup);
            return Ok(orderlineGroupDto);
        }
        catch (Exception ex)
        {
            LogError("Error retrieving OrderlineGroup with orderlineId: " + orderlineId + " - " + ex.Message);
            return BadRequest("Error finding OrderLine");
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
