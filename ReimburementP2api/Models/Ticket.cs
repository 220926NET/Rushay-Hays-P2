namespace ReimburementP2api.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? Note { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal AmountRequest { get; set; }
        public string? Status { get; set; }
        public int UserId { get; set; }

        public Ticket() { }

        public Ticket(string note, decimal amountRequest, int idNum)
        {
            Id = 0;
            Note = note;
            DateCreated = DateTime.Now;
            AmountRequest = amountRequest;
            Status = "Pending";
            UserId = idNum;

        }
    }
}
