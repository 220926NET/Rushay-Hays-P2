using System.Net.Sockets;

namespace ReimburementP2api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public bool IsAdmin { get; set; }
        public List<Ticket>? Tickets { get; set; }

    }
}
