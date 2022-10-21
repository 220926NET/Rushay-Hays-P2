using Microsoft.AspNetCore.Mvc;
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

        // GET: api/<TicketController>
        [HttpGet]
        public IActionResult GetUser(int userId)
        {
            return Ok(_ticketRepository.GetTicketsForUser(userId));
        }

        // GET api/<TicketController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TicketController>
        [HttpPost]
        public IActionResult AddTicket(Ticket ticket)
        {
            return CreatedAtAction("Get", new { id = ticket.Id }, ticket);
        }

        // PUT api/<TicketController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TicketController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
