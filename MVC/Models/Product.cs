using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class Product
{
    public Guid Id { get; set; }
    [MaxLength(255)]
    public required string Name { get; set; }

    public string? Description { get; set; }
}