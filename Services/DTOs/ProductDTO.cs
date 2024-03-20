using System.ComponentModel.DataAnnotations;

namespace Services.DTOs;

public class ProductDTO
{
    [StringLength(255)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}