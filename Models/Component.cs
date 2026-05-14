namespace WebApplication7.Models;

public class Component
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int ManufacturerId { get; set; }

    public Manufacturer Manufacturer { get; set; } = null!;

    public int TypeId { get; set; }

    public ComponentType Type { get; set; } = null!;

    public ICollection<PcComponent> PcComponents { get; set; } = new List<PcComponent>();
}