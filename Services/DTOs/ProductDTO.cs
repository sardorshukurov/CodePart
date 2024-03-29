using System.ComponentModel.DataAnnotations;

namespace Services.DTOs;

public class ProductDTO
{
    [MaxLength(255)]
    public required string Name { get; set; }

    public string? Description { get; set; }
}