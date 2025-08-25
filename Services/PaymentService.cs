using System;
using FinancialSystemApi.Data;
using FinancialSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialSystemApi.Services;

public class PaymentService : IPaymentService
{
    private readonly AppDbContext _context;

    public PaymentService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Payment> CreateAsync(Payment payment)
    {
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();
        return payment;

    }

    public async Task<bool> DeleteAsync(int id)
    {
        var payment = await _context.Payments.FindAsync(id);
        if (payment == null) return false;
        
        _context.Payments.Remove(payment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        return await _context.Payments
        .Include(p => p.Loan)
        .Include(p => p.User)
        .ToListAsync();
    }

    public Task<Payment?> GetByIdAsync(int id)
    {
        var payment = _context.Payments
        .Include(p => p.Loan)
        .Include(p => p.User)
        .FirstOrDefaultAsync(p => p.Id == id);

        if (payment == null) return null!;
        return payment;
    }

    public async Task<bool> UpdateAsync(int id, Payment payment)
    {
        var exists = await _context.Payments.AnyAsync(p => p.Id == id);

        if (!exists) return false;

        payment.Id = id;
        _context.Entry(payment).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }
}
