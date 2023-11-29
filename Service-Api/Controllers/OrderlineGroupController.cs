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
public class OrderlineGroupController : ControllerBase
{
    private readonly IOrderlineGroupData _orderlineGroupData;
    private readonly IMapper _mapper;

    public OrderlineGroupController(IOrderlineGroupData orderlineGroupData, IMapper mapper)
    {
        _orderlineGroupData = orderlineGroupData;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrderlineGroups()
    {
        var orderlineGroups = await _orderlineGroupData.GetAllOrderlineGroups();
        var orderlineGroupDtos = _mapper.Map<List<OrderlineGroupDto>>(orderlineGroups);
        return Ok(orderlineGroupDtos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderlineGroup(OrderlineGroupDto orderlineGroupDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _orderlineGroupData.CreateOrderlineGroup(orderlineGroupDto);
                return Ok("OrderlineGroup created successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error creating an OrderlineGroup: " + ex.Message);
                return BadRequest("Error creating an OrderlineGroup");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpGet("{orderlineId}/{productId}/{comboId}")]
    public async Task<IActionResult> GetOrderlineGroupById(int orderlineId, int productId, int comboId)
    {
        try
        {
            var orderlineGroup = await _orderlineGroupData.GetOrderlineGroupById(orderlineId, productId, comboId);
            if (orderlineGroup == null)
            {
                return NotFound();
            }
            var orderlineGroupDto = _mapper.Map<OrderlineGroupDto>(orderlineGroup);
            return Ok(orderlineGroupDto);
        }
        catch (Exception ex)
        {
            LogError("Eror");
            return BadRequest("Error finding OrderlineGroup");
        }
    }

    [HttpPut("{orderlineId}/{productId}/{comboId}")]
    public async Task<IActionResult> UpdateOrderlineGroup(int orderlineID, int productId, int comboId, OrderlineGroupDto orderlineGroupDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _orderlineGroupData.UpdateOrderlineGroup(orderlineGroupDto);
                return Ok("OrderlineGroup updated successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error updating an OrderlineGroup: " + ex.Message);
                return BadRequest("Error updating an OrderlineGroup");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpDelete("{orderlineId}/{productId}/{comboId}")]
    public async Task<IActionResult> DeleteOrderlineGroup(int orderlineID, int productId, int comboId)
    {
        try
        {
            await _orderlineGroupData.DeleteOrderlineGroup(orderlineID, productId, comboId);
            return Ok("OrderlineGroup deleted successfully");
        }
        catch (Exception ex)
        {
            // Handle the exception or log it
            LogError("Error deleting an OrderlineGroup: " + ex.Message);
            return BadRequest("Error deleting an OrderlineGroup");
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
