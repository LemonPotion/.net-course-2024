using BankSystem.App.Dto.Client.Requests;
using BankSystem.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    
    private readonly ClientService _clientService;

    public ClientController(ClientService clientService)
    {
        _clientService = clientService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddClient([FromBody] CreateClientRequest request, CancellationToken cancellationToken)
    {
        
        await _clientService.AddAsync(request,cancellationToken);
        
        return Ok();
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetClientById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _clientService.GetByIdAsync(id,cancellationToken);
        
        return Ok(result);
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetAllClientsPaged([FromQuery] GetAllClientsPagedRequest request, CancellationToken cancellationToken)
    {
        var result = await _clientService.GetPagedAsync(request, cancellationToken);
        
        return Ok(result);
    }
    
    
    [HttpPut]
    public async Task<IActionResult> UpdateClient([FromBody] UpdateClientRequest request, CancellationToken cancellationToken)
    {
        await _clientService.UpdateAsync(request, cancellationToken);
        
        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteClient([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        await _clientService.DeleteAsync(id, cancellationToken);
        
        return Ok();
    }
}