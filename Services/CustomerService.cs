using System;
using FinancialSystemApi.Data;
using FinancialSystemApi.DTOs;
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

    public async Task<CustomerDto> CreateAsync(CustomerDto dto)
    {
        var customer = new Customer
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Phone = dto.Phone,
            Email = dto.Email
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Phone = customer.Phone,
            Email = customer.Email
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return false;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<CustomerDto>> GetAllAsync()
    {
        var customer = _context.Customers.AsQueryable();

        return await customer
            .Select(c => new CustomerDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Phone = c.Phone,
                Email = c.Email
            }).ToListAsync();
    }

    public async Task<CustomerDto?> GetByIdAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return null;

        return new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Phone = customer.Phone,
            Email = customer.Email
        };
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
