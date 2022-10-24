using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ReimburementP2api.Repositories;
using ReimburementP2api.Models;   

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReimburementP2api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult GetUser(string username, string password)
        {
            return Ok(_userRepository.GetUserByUserNameAndPass(username, password));
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            bool userWasAdded = _userRepository.AddUser(user);
            if (userWasAdded)
            {
                return CreatedAtAction("Get", new { id = user.Id }, user);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
