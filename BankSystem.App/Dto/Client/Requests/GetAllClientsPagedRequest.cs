namespace BankSystem.App.Dto.Client.Requests;

public record GetAllClientsPagedRequest(
    PaginationRequest Pagination,
    ClientFilterRequest? Filter
);