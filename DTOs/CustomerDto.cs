using System;
using System.ComponentModel.DataAnnotations;

namespace FinancialSystemApi.DTOs;

public class CustomerDto
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "FirstName is required.")]
    [StringLength(50, ErrorMessage = "FirstName cannot exceed 50 characters.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "LastName is required.")]
    [StringLength(50, ErrorMessage = "LastName cannot exceed 50 characters.")]
    public string LastName { get; set; } = string.Empty;

    [DataType(DataType.PhoneNumber)]
    public string? Phone { get; set; }

    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
}
