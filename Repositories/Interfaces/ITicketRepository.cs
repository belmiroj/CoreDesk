using CoreDesk.API.Enums;
using CoreDesk.API.Models.Entities;

namespace CoreDesk.API.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket> AddAsync(Ticket ticket);
        Task<Ticket?> GetByIdWithDetailsAsync(int id);
        Task<Ticket?> GetByIdAsync(int id);
        Task<IEnumerable<Ticket>> GetFilteredAsync(TicketStatus? status, TicketPriority? priority, int? categoryId);
        Task UpdateAsync(Ticket ticket);
        Task AddInteractionAsync(TicketInteraction interaction);
    }
}