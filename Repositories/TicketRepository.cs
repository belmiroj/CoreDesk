using CoreDesk.API.Data;
using CoreDesk.API.Enums;
using CoreDesk.API.Models.Entities;
using CoreDesk.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoreDesk.API.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket> AddAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<Ticket?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Tickets
                .Include(t => t.Category)
                .Include(t => t.Interactions)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Ticket?> GetByIdAsync(int id) =>
            await _context.Tickets.FindAsync(id);

        public async Task<IEnumerable<Ticket>> GetFilteredAsync(TicketStatus? status, TicketPriority? priority, int? categoryId)
        {
            var query = _context.Tickets
                .Include(t => t.Category)
                .AsNoTracking()
                .AsQueryable();

            if (status.HasValue)
                query = query.Where(t => t.Status == status.Value);

            if (priority.HasValue)
                query = query.Where(t => t.Priority == priority.Value);

            if (categoryId.HasValue)
                query = query.Where(t => t.CategoryId == categoryId.Value);

            return await query.ToListAsync();
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task AddInteractionAsync(TicketInteraction interaction)
        {
            await _context.TicketInteractions.AddAsync(interaction);
            await _context.SaveChangesAsync();
        }
    }
}