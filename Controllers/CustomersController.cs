using FinancialSystemApi.DTOs;
using FinancialSystemApi.Models;
using FinancialSystemApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerService service, ILogger<CustomersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _service.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _service.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(CustomerDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var customer = await _service.CreateAsync(dto);
            _logger.LogInformation("Customer added: {Customer}", dto.FirstName + ' ' + dto.LastName);

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var customerUpdated = await _service.UpdateAsync(id, customer);
            if (!customerUpdated)
            {
                _logger.LogWarning("Attempted to update non-existent customer with id: {Id}", id);
                return NotFound();
            }

            _logger.LogInformation("Customer updated: {Customer}", customer.FirstName + ' ' + customer.LastName);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customerDeleted = await _service.DeleteAsync(id);
            if (!customerDeleted)
            {
                _logger.LogWarning("Attempted to delete non-existent customer with id: {Id}", id);
                return NotFound();
            }

            _logger.LogInformation("Customer deleted: {Id}", id);
            return NoContent();
        }
    }
}
