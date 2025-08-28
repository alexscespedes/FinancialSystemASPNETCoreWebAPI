using FinancialSystemApi.Models;
using FinancialSystemApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _service;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(IPaymentService service, ILogger<PaymentsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            var payments = await _service.GetAllAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            var payment = await _service.GetByIdAsync(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var newPayment = await _service.CreateAsync(payment);
            _logger.LogInformation("Payment added: {Payment}", payment.Amount);

            return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, payment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, Payment payment)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var paymentUpdated = await _service.UpdateAsync(id, payment);
            if (!paymentUpdated)
            {
                _logger.LogWarning("Attempted to update non-existent payment with id: {Id}", id);
                return NotFound();
            }

            _logger.LogInformation("Payment updated: {Payment}", payment.Amount);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var paymentDeleted = await _service.DeleteAsync(id);
            if (!paymentDeleted) 
            {
                _logger.LogWarning("Attempted to delete non-existent payment with id: {Id}", id);
                return NotFound();
            }

            _logger.LogInformation("Payment deleted: {Id}", id);
            return NoContent();
        }
    }
}
