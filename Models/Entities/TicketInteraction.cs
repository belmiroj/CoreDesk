using System.Text.Json.Serialization;

namespace CoreDesk.API.Models.Entities
{
    public class TicketInteraction
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime RegisteredAt { get; set; }

        [JsonIgnore]
        public Ticket? Ticket { get; set; }
    }
}