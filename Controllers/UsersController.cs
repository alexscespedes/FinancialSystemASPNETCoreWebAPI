using FinancialSystemApi.Models;
using FinancialSystemApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService service, ILogger<UsersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _service.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var newUser = await _service.CreateAsync(user);
            _logger.LogInformation("User added: {User}", user.Username);

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var userUpdated = await _service.UpdateAsync(id, user);
            if (!userUpdated)
            {
                _logger.LogWarning("Attempted to update non-existent user with id: {Id}", id);
                return NotFound();
            }

            _logger.LogInformation("User updated: {User}", user.Username);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userDeleted = await _service.DeleteAsync(id);
            if (!userDeleted)
            {
                _logger.LogWarning("Attempted to delete non-existent user with id: {Id}", id);
                return NotFound();
            }

            _logger.LogInformation("User deleted: {Id}", id);
            return NoContent();
        }
    }
}
