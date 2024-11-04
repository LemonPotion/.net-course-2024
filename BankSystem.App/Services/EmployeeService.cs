using System.Linq.Expressions;
using AutoMapper;
using BankSystem.App.Dto.Employee.Requests;
using BankSystem.App.Dto.Employee.Responses;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Services;

public class EmployeeService
{
    private readonly IStorage<Employee> _employeeStorage;

    private readonly IMapper _mapper;

    public EmployeeService(IStorage<Employee> employeeStorage, IMapper mapper)
    {
        _employeeStorage = employeeStorage;
        _mapper = mapper;
    }

    public async Task AddAsync(CreateEmployeeRequest request, CancellationToken cancellationToken)
    {
        var employee = _mapper.Map<Employee>(request);
        
        await _employeeStorage.AddAsync(employee, cancellationToken);
    }

    public async Task<GetEmployeeByIdResponse> GetByIdASync(Guid id, CancellationToken cancellationToken)
    {
        var employee = await _employeeStorage.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<GetEmployeeByIdResponse>(employee);
    }

    public async Task<IEnumerable<GetEmployeeByIdResponse>> GetPagedAsync(GetAllEmployeesPagedRequest request, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<Expression<Func<Employee, bool>>>(request.Filter);
        var employees = await _employeeStorage.GetAsync(request.Pagination.PageNumber, request.Pagination.PageSize, filter, cancellationToken);
        return _mapper.Map<List<GetEmployeeByIdResponse>>(employees);
    }

    public async Task UpdateAsync(UpdateEmployeeRequest  request, CancellationToken cancellationToken)
    {
        var employee = _mapper.Map<Employee>(request);
        
        await _employeeStorage.UpdateAsync(employee.Id, employee, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _employeeStorage.DeleteAsync(id, cancellationToken);
    }
}