namespace WebApplication7.Models;

public class PcComponent
{
    public int PcId { get; set; }

    public Pc Pc { get; set; } = null!;

    public string ComponentCode { get; set; } = null!;

    public Component Component { get; set; } = null!;

    public int Amount { get; set; }
}