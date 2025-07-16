using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models;

public class Customer : Entity<CustomerId>
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    public Customer(CustomerId customerId,string name, string email)
    {
        Id = customerId;
        Name = name;
        Email = email;
    }

    public static Customer Create(CustomerId id,string name, string email)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(email);

        return new Customer(id, name, email);
    }
}