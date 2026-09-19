using CoreDesk.API.Enums;

namespace CoreDesk.API.DTOs
{
    public record CreateTicketRequest(
        string Title, 
        string Description, 
        TicketPriority Priority, 
        string RequesterName, 
        int CategoryId
    );

    public record CloseTicketRequest(string Solution);

    public record AddInteractionRequest(string Author, string Message);

    public record InteractionResponse(
        int Id, 
        string Author, 
        string Message, 
        DateTime RegisteredAt
    );

    public record TicketDetailsResponse(
        int Id,
        string Title,
        string Description,
        TicketPriority Priority,
        TicketStatus Status,
        string RequesterName,
        DateTime CreatedAt,
        DateTime? ClosedAt,
        string? Solution,
        CategoryResponse? Category,
        List<InteractionResponse> Interactions
    );
}