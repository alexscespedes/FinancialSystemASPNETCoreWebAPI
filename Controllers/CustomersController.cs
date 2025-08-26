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

        public CustomersController(ICustomerService service)
        {
            _service = service;
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
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var customerUpdated = await _service.UpdateAsync(id, customer);
            if (!customerUpdated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customerDeleted = await _service.DeleteAsync(id);
            if (!customerDeleted) return NotFound();

            return NoContent();
        }
    }
}
