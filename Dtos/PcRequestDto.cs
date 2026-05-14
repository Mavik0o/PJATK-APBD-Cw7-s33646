using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Dtos;

public class PcRequestDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Range(0.01, 999.99)]
    public decimal Weight { get; set; }

    [Range(1, 120)]
    public int Warranty { get; set; }

    public DateTime CreatedAt { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}