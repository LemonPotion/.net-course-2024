namespace BankSystem.App.Dto.Employee.Requests;

public record GetAllEmployeesPagedRequest(
    PaginationRequest Pagination,
    EmployeeFilterRequest? Filter
);