namespace WebApplication7.Models;


public class Manufacturer
{
    public int Id { get; set; }

    public string Abbreviation { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public DateOnly FoundationDate { get; set; }

    public ICollection<Component> Components { get; set; } = new List<Component>();
}