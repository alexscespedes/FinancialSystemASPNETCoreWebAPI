using System;
using FinancialSystemApi.Models;

namespace FinancialSystemApi.Services;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(int id);
    Task<Customer> CreateAsync(Customer customer);
    Task<bool> UpdateAsync(int id, Customer customer);
    Task<bool> DeleteAsync(int id);
}
