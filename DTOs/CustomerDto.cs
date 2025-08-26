using System;
using System.ComponentModel.DataAnnotations;

namespace FinancialSystemApi.DTOs;

public class CustomerDto
{
    public int Id { get; set; }
    
    [Required, StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [DataType(DataType.PhoneNumber)]
    public string? Phone { get; set; }

    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
}
