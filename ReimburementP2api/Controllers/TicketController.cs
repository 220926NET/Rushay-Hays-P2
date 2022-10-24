using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ReimburementP2api.Models;
using ReimburementP2api.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReimburementP2api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        //This will get a single ticket, this is not used by the end user but is instead required for the
        //CreatedAtAction in POST to work correctly
        [HttpGet]
        public IActionResult GetSingleTicket(int ticketId)
        {
            return Ok(_ticketRepository.GetTicketById(ticketId));
        }

        //This gets ALL the tickets previously submitted by the current user
        // GET: api/<TicketController>
        [HttpGet("userTickets/{id}")]
        public IActionResult GetUsersTickets(int id)
        {
            return Ok(_ticketRepository.GetTicketsForUser(id));
        }

        // GET api/<TicketController>/5
        //The id in this param is the USER's Id
        [HttpGet("pending/{id}")]
        public IActionResult GetAllPendingTicketsExceptForUsers(int id)
        {
            return Ok(_ticketRepository.GetAllPendingTickets(id));
        }

        //This allows the current user to submit a ticket
        // POST api/<TicketController>
        [HttpPost]
        public IActionResult AddTicket(Ticket ticket)
        {
            bool ticketWasAdded = _ticketRepository.AddTicket(ticket);
            if (ticketWasAdded)
            {
                return CreatedAtAction(nameof(GetSingleTicket), new { ticketId = ticket.Id}, ticket);

            }
            else
            {
                return BadRequest();
            }
            
        }

        // PUT api/<TicketController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, int statId, bool hasAdminPrivelige)
        {
            if (hasAdminPrivelige)
            {
               bool wasUpdated =  _ticketRepository.UpdateTicket(id, statId);
                if (wasUpdated)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<TicketController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
