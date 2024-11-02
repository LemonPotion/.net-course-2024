namespace BankSystem.App.Dto;

public record PaginationRequest(
    int PageNumber,
    int PageSize
);