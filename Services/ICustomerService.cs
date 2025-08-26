using System;
using FinancialSystemApi.DTOs;
using FinancialSystemApi.Models;

namespace FinancialSystemApi.Services;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDto>> GetAllAsync();
    Task<CustomerDto?> GetByIdAsync(int id);
    Task<CustomerDto> CreateAsync(CustomerDto dto);
    Task<bool> UpdateAsync(int id, Customer customer);
    Task<bool> DeleteAsync(int id);
}
