using CoreDesk.API.DTOs;
using CoreDesk.API.Enums;
using CoreDesk.API.Models.Entities;
using CoreDesk.API.Repositories.Interfaces;
using CoreDesk.API.Services.Interfaces;

namespace CoreDesk.API.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ICategoryRepository _categoryRepository;

        public TicketService(ITicketRepository ticketRepository, ICategoryRepository categoryRepository)
        {
            _ticketRepository = ticketRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<TicketDetailsResponse> OpenTicketAsync(CreateTicketRequest request)
        {
            _ = await _categoryRepository.GetByIdAsync(request.CategoryId)
                ?? throw new KeyNotFoundException("Provided category does not exist.");

            var ticket = new Ticket
            {
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority,
                RequesterName = request.RequesterName,
                CategoryId = request.CategoryId,
                Status = TicketStatus.Open,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _ticketRepository.AddAsync(ticket);
            return await GetByIdAsync(created.Id);
        }

        public async Task StartServiceAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Ticket not found.");

            if (ticket.Status != TicketStatus.Open)
                throw new InvalidOperationException("Only tickets with status 'Open' can be started.");

            ticket.Status = TicketStatus.InProgress;
            await _ticketRepository.UpdateAsync(ticket);
        }

        public async Task CloseTicketAsync(int id, CloseTicketRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Solution))
                throw new ArgumentException("A solution text is required to close the ticket.");

            var ticket = await _ticketRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Ticket not found.");

            if (ticket.Status == TicketStatus.Closed)
                throw new InvalidOperationException("The ticket is already closed.");

            ticket.Status = TicketStatus.Closed;
            ticket.Solution = request.Solution;
            ticket.ClosedAt = DateTime.UtcNow;

            await _ticketRepository.UpdateAsync(ticket);
        }

        public async Task AddInteractionAsync(int id, AddInteractionRequest request)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Ticket not found.");

            if (ticket.Status == TicketStatus.Closed)
                throw new InvalidOperationException("Cannot add interactions to a closed ticket.");

            var interaction = new TicketInteraction
            {
                TicketId = id,
                Author = request.Author,
                Message = request.Message,
                RegisteredAt = DateTime.UtcNow
            };

            await _ticketRepository.AddInteractionAsync(interaction);
        }

        public async Task<TicketDetailsResponse> GetByIdAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdWithDetailsAsync(id)
                ?? throw new KeyNotFoundException("Ticket not found.");

            return MapToResponse(ticket);
        }

        public async Task<IEnumerable<TicketDetailsResponse>> GetFilteredAsync(TicketStatus? status, TicketPriority? priority, int? categoryId)
        {
            var list = await _ticketRepository.GetFilteredAsync(status, priority, categoryId);
            return list.Select(MapToResponse);
        }

        private static TicketDetailsResponse MapToResponse(Ticket t)
        {
            return new TicketDetailsResponse(
                t.Id,
                t.Title,
                t.Description,
                t.Priority,
                t.Status,
                t.RequesterName,
                t.CreatedAt,
                t.ClosedAt,
                t.Solution,
                t.Category != null ? new CategoryResponse(t.Category.Id, t.Category.Name) : null,
                t.Interactions.Select(i => new InteractionResponse(i.Id, i.Author, i.Message, i.RegisteredAt)).ToList()
            );
        }
    }
}