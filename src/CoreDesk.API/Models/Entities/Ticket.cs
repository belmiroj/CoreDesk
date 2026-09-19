using CoreDesk.API.Enums;

namespace CoreDesk.API.Models.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketPriority Priority { get; set; }
        public TicketStatus Status { get; set; }
        public string RequesterName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string? Solution { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<TicketInteraction> Interactions { get; set; } = new List<TicketInteraction>();
    }
}