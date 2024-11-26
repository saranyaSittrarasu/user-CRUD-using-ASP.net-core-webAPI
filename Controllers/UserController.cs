using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserDetails_CRUD.Models;

namespace UserDetails_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _userContext;
        public UserController(UserContext userContext)
        {
            _userContext = userContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_userContext.Users == null)
            { return NotFound(); }

            return await _userContext.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_userContext.Users == null)
            { return NotFound(); }
            var user = await _userContext.Users.FindAsync(id);
            if (user == null)
            { return NotFound(); }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User objUser)
        {

            _userContext.Users.Add(objUser);
            await _userContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = objUser.ID }, objUser);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, User objUser)
        {
            if (id != objUser.ID) { return BadRequest(); }

            _userContext.Entry(objUser).State = EntityState.Modified;
            try
            {
                await _userContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) { throw; }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        { if (_userContext.Users == null) { return NotFound(); }
            var user = await _userContext.Users.FindAsync(id);
            if(user == null) { return NotFound(); };
            _userContext.Users.Remove(user);
            await _userContext.SaveChangesAsync();
            return Ok();
        }
    }
}
