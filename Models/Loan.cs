using System;
using System.ComponentModel.DataAnnotations;
using FinancialSystemApi.Models.Enums;

namespace FinancialSystemApi.Models;

public class Loan
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }

    [DataType(DataType.Currency)]
    public decimal PrincipalAmount { get; set; }

    [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = false)]
    public decimal InterestRate { get; set; }

    [Range(1, 12, ErrorMessage = "Month must be between 1 and 12.")]
    public int TermsMonth { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime StartDate { get; set; }
    public LoanStatus LoanStatus { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
