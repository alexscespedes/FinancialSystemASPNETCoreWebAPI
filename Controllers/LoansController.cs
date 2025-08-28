using FinancialSystemApi.Models;
using FinancialSystemApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _service;
        private readonly ILogger<LoansController> _logger;


        public LoansController(ILoanService service, ILogger<LoansController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoans()
        {
            var loans = await _service.GetAllAsync();
            return Ok(loans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoan(int id)
        {
            var loan = await _service.GetByIdAsync(id);
            if (loan == null) return NotFound();
            return Ok(loan);
        }

        [HttpPost]
        public async Task<ActionResult<Loan>> CreateLoan(Loan loan)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var newLoan = await _service.CreateAsync(loan);
            _logger.LogInformation("Loan added: {Loan}", loan.PrincipalAmount);

            return CreatedAtAction(nameof(GetLoan), new { id = loan.Id }, loan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLoan(int id, Loan loan)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var loanUpdated = await _service.UpdateAsync(id, loan);
            if (!loanUpdated)
            {
                _logger.LogWarning("Attempted to update non-existent loan with id: {Id}", id);
                return NotFound();
            }

            _logger.LogInformation("Payment updated: {Payment}", loan.PrincipalAmount);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var loanDeleted = await _service.DeleteAsync(id);
            if (!loanDeleted)
            {
                _logger.LogWarning("Attempted to delete non-existent loan with id: {Id}", id);
                return NotFound();
            }

            _logger.LogInformation("Payment deleted: {Id}", id);
            return NoContent();
        }

    }
}
