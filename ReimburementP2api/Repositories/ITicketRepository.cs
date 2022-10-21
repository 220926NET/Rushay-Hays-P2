using ReimburementP2api.Models;

namespace ReimburementP2api.Repositories
{
    public interface ITicketRepository
    {
        List<Ticket> GetTicketsForUser(int userId);
        void AddTicket(Ticket ticket);
    }
}
