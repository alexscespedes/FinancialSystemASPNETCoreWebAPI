using System;
using FinancialSystemApi.Models;

namespace FinancialSystemApi.Services;

public interface IPaymentService
{
    Task<IEnumerable<Payment>> GetAllAsync();
    Task<Payment?> GetByIdAsync(int id);
    Task<Payment> CreateAsync(Payment payment);
    Task<bool> UpdateAsync(int id, Payment payment);
    Task<bool> DeleteAsync(int id);
}
