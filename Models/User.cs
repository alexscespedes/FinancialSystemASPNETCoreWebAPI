using System;
using System.ComponentModel.DataAnnotations;
using FinancialSystemApi.Models.Enums;

namespace FinancialSystemApi.Models;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Username is required.")]
    [MaxLength(20, ErrorMessage = "Username cannot exceed 20 characters")]
    public string Username { get; set; } = string.Empty;
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    public Role Role { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

}
