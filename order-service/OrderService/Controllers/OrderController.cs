using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.DTO;
using OrderService.Domain.Interfaces.Services;

namespace OrderService.Presentation.Controllers;

[ApiController]
[Route("orders")]
public class OrderController(ILogger<OrderController> logger, IOrder orderService) : ControllerBase
{
    private readonly ILogger<OrderController> _logger = logger;
    private readonly IOrder _orderService = orderService;

    [HttpGet()]
    public async Task<List<Order>> Get()
    {
        return await _orderService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<Order> Get(int id)
    {
        return await _orderService.Get(id);
    }

    [HttpPost()]
    public async Task<ActionResult> Post(Order order)
    {
        // need the Fluent Validation
        // need the Exception Handling Middleware

        await _orderService.Create(order);

        return Created("", order);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> Patch(int id, [FromBody] Order order)
    {
        await _orderService.Update(id, order);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _orderService.Delete(id);
        return NoContent();
    }

}
