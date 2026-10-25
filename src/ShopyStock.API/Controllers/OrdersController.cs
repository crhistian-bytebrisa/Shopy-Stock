using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using ShopyStock.Application.DTOs;
using ShopyStock.Application.DTOs.Common;
using ShopyStock.Application.DTOs.Orders;
using ShopyStock.Application.Interfaces;

namespace ShopyStock.API.Controllers;

[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<OrderDto>>> GetAll([FromQuery] RequestOrder requestOrder,
        CancellationToken cancellationToken)
    {
        var orders = await _orderService.GetAll(requestOrder, cancellationToken);
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponseDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetById(id, cancellationToken);
        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody]CreateOrderDto createOrderDto, CancellationToken cancellationToken)
    {
        await _orderService.AddEntity(createOrderDto, cancellationToken);
        return Created(string.Empty, new { message = "Orden creada exitosamente" });
    }
}