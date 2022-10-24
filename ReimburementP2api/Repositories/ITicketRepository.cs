using ReimburementP2api.Models;

namespace ReimburementP2api.Repositories
{
    public interface ITicketRepository
    {
        List<Ticket> GetTicketsForUser(int userId);
        bool AddTicket(Ticket ticket);
        bool UpdateTicket(int tID, int statID);

        List<Ticket> GetAllPendingTickets(int userId);

        Ticket GetTicketById(int tId);
    }
}
