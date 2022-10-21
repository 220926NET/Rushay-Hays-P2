using Microsoft.Data.SqlClient;
using ReimburementP2api.Models;

namespace ReimburementP2api.Repositories
{
    public class TicketRepository : BaseRepository, ITicketRepository
    {
        public TicketRepository(IConfiguration configuration) : base(configuration) { }


        //This will return all the tickets of a specific user
        public List<Ticket> GetTicketsForUser(int userId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @$"
                Select Note, DateCreated, AmountRequested, s.StatusDescription
                FROM Tickets t
                LEFT JOIN Statuses s ON t.StatusId = s.Id
                WHERE UserId = @id;
                 ";
                    cmd.Parameters.AddWithValue("@id", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Ticket> tickets = new List<Ticket>();
                        while (reader.Read())
                        {
                            Ticket ticket = new Ticket()
                            {
                                Note = reader.GetString(reader.GetOrdinal("Note")),
                                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                                AmountRequest = reader.GetDecimal(reader.GetOrdinal("AmountRequested")),
                                Status = reader.GetString(reader.GetOrdinal("StatusDescription")),
                            };

                            tickets.Add(ticket);
                        }

                        return tickets;
                    }
                }

            }

        }

        //add a new ticket to the database
        public void AddTicket(Ticket ticket)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Tickets
                        (Note, AmountRequested, DateCreated, StatusId, UserId)
                        VALUES (@tNote, @tAmount, @tDate, 1, @tUserId);
                    ";
                    cmd.Parameters.AddWithValue("@tNote", ticket.Note);
                    cmd.Parameters.AddWithValue("@tAmount", ticket.AmountRequest);
                    cmd.Parameters.AddWithValue("@tDate", ticket.DateCreated);
                    cmd.Parameters.AddWithValue("@tUserId", ticket.UserId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
