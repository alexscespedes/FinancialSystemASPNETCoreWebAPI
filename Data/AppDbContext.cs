using System;
using FinancialSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialSystemApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Loan>()
            .Property(l => l.PrincipalAmount)
            .HasPrecision(18, 2);
        modelBuilder.Entity<Loan>()
            .Property(l => l.InterestRate)
            .HasPrecision(3, 2);

        modelBuilder.Entity<Payment>()
            .Property(p => p.Amount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Customer>()
            .Property(c => c.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<Customer>()
            .Property(c => c.UpdatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<User>()
            .Property(u => u.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<User>()
            .Property(u => u.UpdatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<Loan>()
            .Property(l => l.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<Loan>()
            .Property(l => l.UpdatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<Payment>()
            .Property(p => p.PaymentDate)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Loan)
            .WithMany(l => l.Payments)
            .HasForeignKey(p => p.LoanId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.User)
            .WithMany(u => u.Payments)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
