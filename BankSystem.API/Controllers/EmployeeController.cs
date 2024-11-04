using BankSystem.App.Dto.Employee.Requests;
using BankSystem.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService EmployeeService)
    {
        _employeeService = EmployeeService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeRequest request, CancellationToken cancellationToken)
    {
        await _employeeService.AddAsync(request,cancellationToken);
        
        return Ok();
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEmployeeById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _employeeService.GetByIdASync(id,cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetEmployees([FromQuery] GetAllEmployeesPagedRequest request, CancellationToken cancellationToken)
    {
        
        var result = await _employeeService.GetPagedAsync(request, cancellationToken);
        
        return Ok(result);
    }
    
    
    [HttpPut]
    public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeRequest request, CancellationToken cancellationToken)
    {
        await _employeeService.UpdateAsync(request, cancellationToken);
        
        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteEmployee([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        await _employeeService.DeleteAsync(id, cancellationToken);
        
        return Ok();
    }
}