using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.DTO;
using OrderService.Domain.Interfaces.Services;

namespace OrderService.Presentation.Controllers;

[ApiController]
[Route("addresses")]
public class AddressController(ILogger<AddressController> logger, IAddress addressService) : ControllerBase
{
    private readonly ILogger<AddressController> _logger = logger;
    private readonly IAddress _addressService = addressService;

    [HttpGet()]
    public async Task<List<Address>> Get()
    {
        return await _addressService.GetAll();
    }

    [HttpGet("{id}", Name = "GetById")]
    public async Task<Address> Get(int id)
    {
        return await _addressService.Get(id);
    }

    [HttpPost()]
    public async Task<ActionResult> Post(Address address)
    {
        // need the Fluent Validation
        // need the Exception Handling Middleware

        await _addressService.Create(address);

        return Created("", address);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> Patch(int id, [FromBody] Address address)
    {
        await _addressService.Update(id, address);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _addressService.Delete(id);
        return NoContent();
    }

}
