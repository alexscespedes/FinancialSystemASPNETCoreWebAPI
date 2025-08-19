using System;
using FinancialSystemApi.Models;
using FinancialSystemApi.Models.Enums;

namespace FinancialSystemApi.Data;

public class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        if (context.Customers.Any() || context.Users.Any() || context.Loans.Any() || context.Payments.Any())
            return;

        var customers = new List<Customer>
        {
            new Customer { FirstName = "Alexander", LastName = "Sencion", Phone = "809-414-9966", Email = "alex@gmail.com", Address = "ohn Smith, 123 Main Street, Anytown, CA 91234, USA" ,
                            DateOfBirth = new DateTime(1999, 08, 03)
            },
            new Customer { FirstName = "Damon", LastName = "Salvatore", Phone = "829-424-7766", Email = "damon@gmail.com", Address = "ohn Smith, 456 Main Street, Anytown, CA 91234, USA" ,
                            DateOfBirth = new DateTime(1983, 05, 09)
            },
            new Customer { FirstName = "Chris", LastName = "Paul", Phone = "654-169-8523", Email = "chris@gmail.com", Address = "ohn Smith, 789 Main Street, Anytown, CA 91234, USA" ,
                            DateOfBirth = new DateTime(1986, 12, 10)
            }
        };

        var users = new List<User>
        {
            new User { Username = "alexscespedes", PasswordHash = "8743b52063cd84097a65d1633f5c74f5", Role = Role.Admin},
            new User { Username = "marialopez10", PasswordHash = "01dfae6e5d4d90d9892622325959afbe:7050461", Role = Role.Secretary},
            new User { Username = "reneortiz16", PasswordHash = "f0fda58630310a6dd91a7d8f0a4ceda2:4225637426", Role = Role.Admin},
        };

        context.Customers.AddRange(customers);
        context.Users.AddRange(users);
        context.SaveChanges();

        var loans = new List<Loan>
        {
            new Loan { CustomerId = 1, PrincipalAmount = 1000000, InterestRate = 0.12m, TermsMonth = 60, StartDate = new DateTime(2025, 08, 25), LoanStatus = LoanStatus.Active,
                        CreatedBy = 1 },
            new Loan { CustomerId = 2, PrincipalAmount = 500000, InterestRate = 0.09m, TermsMonth = 52, StartDate = new DateTime(2025, 08, 27), LoanStatus = LoanStatus.Renewed,
                        CreatedBy = 2 },
            new Loan { CustomerId = 3, PrincipalAmount = 999990, InterestRate = 0.10m, TermsMonth = 56, StartDate = new DateTime(2025, 08, 29), LoanStatus = LoanStatus.Overdue,
                        CreatedBy = 2 }
        };

        context.Loans.AddRange(loans);
        context.SaveChanges();

        var payments = new List<Payment>
        {
            new Payment { LoanId = 1, Amount = 150000, RecordedBy = 2 },
            new Payment { LoanId = 1, Amount = 20000, RecordedBy = 2 },
            new Payment { LoanId = 1, Amount = 385200, RecordedBy = 3 },
        };

        context.Payments.AddRange(payments);
        context.SaveChanges();
    }
}
