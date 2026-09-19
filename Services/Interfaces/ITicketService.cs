using CoreDesk.API.DTOs;
using CoreDesk.API.Enums;

namespace CoreDesk.API.Services.Interfaces
{
    public interface ITicketService
    {
        Task<TicketDetailsResponse> OpenTicketAsync(CreateTicketRequest request);
        Task StartServiceAsync(int id);
        Task CloseTicketAsync(int id, CloseTicketRequest request);
        Task AddInteractionAsync(int id, AddInteractionRequest request);
        Task<TicketDetailsResponse> GetByIdAsync(int id);
        Task<IEnumerable<TicketDetailsResponse>> GetFilteredAsync(TicketStatus? status, TicketPriority? priority, int? categoryId);
    }
}