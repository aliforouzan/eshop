namespace Catalog.Api.Models;

public class Product
{
    public Guid Id { set; get; }
    public string Name { set; get; } = default!;
    public List<string> Category { set; get; } = new();
    public string Description { set; get; } = default!;
    public string ImageFile { set; get; } = default!;
    public decimal Price { set; get; }
}