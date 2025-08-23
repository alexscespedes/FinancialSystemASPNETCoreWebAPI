using System;
using FinancialSystemApi.Models;

namespace FinancialSystemApi.Services;

public interface ILoanService
{
    Task<IEnumerable<Loan>> GetAllAsync();
    Task<Loan?> GetByIdAsync(int id);
    Task<Loan> CreateAsync(Loan loan);
    Task<bool> UpdateAsync(int id, Loan loan);
    Task<bool> DeleteAsync(int id);
}
