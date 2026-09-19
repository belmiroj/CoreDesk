using System.Text.Json.Serialization;

namespace CoreDesk.API.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}