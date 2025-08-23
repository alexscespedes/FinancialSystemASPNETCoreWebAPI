using System;
using FinancialSystemApi.Data;
using FinancialSystemApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FinancialSystemApi.Services;

public class LoanService : ILoanService
{
    private readonly AppDbContext _context;

    public LoanService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Loan> CreateAsync(Loan loan)
    {
        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();
        return loan;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var loan = await _context.Loans.FindAsync(id);
        if (loan == null) return false;
        
        _context.Loans.Remove(loan);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Loan>> GetAllAsync()
    {
        return await _context.Loans
        .Include(l => l.Customer)
        .Include(l => l.User)
        .ToListAsync();
    }

    public async Task<Loan?> GetByIdAsync(int id)
    {
        var loan = await _context.Loans
        .Include(l => l.Customer)
        .Include(l => l.User)
        .FirstOrDefaultAsync(l => l.Id == id);

        if (loan == null) return null;
        return loan;
    }

    public async Task<bool> UpdateAsync(int id, Loan loan)
    {
         var exists = await _context.Loans.AnyAsync(l => l.Id == id);

        if (!exists) return false;

        loan.Id = id;
        _context.Entry(loan).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }
}
