using System;
using FinancialSystemApi.Models.Enums;

namespace FinancialSystemApi.Models;

public class Loan
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal InterestRate { get; set; }
    public int TermsMonth { get; set; }
    public DateTime StartDate { get; set; }
    public LoanStatus LoanStatus { get; set; }
    public int CreatedBy { get; set; }
    public User? User { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

}
