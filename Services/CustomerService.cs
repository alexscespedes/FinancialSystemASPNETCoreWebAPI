using System;
using FinancialSystemApi.Data;
using FinancialSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialSystemApi.Services;

public class CustomerService : ICustomerService
{
    private readonly AppDbContext _context;

    public CustomerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Customer> CreateAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer == null) return false;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer == null) return null;

        return customer;
    }

    public async Task<bool> UpdateAsync(int id, Customer customer)
    {
        var exists = await _context.Customers.AnyAsync(p => p.Id == id);

        if (!exists) return false;

        customer.Id = id;
        _context.Entry(customer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }
}
