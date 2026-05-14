namespace WebApplication7.Dtos;

public class PcComponentDto
{
    public int Amount { get; set; }

    public ComponentDto Component { get; set; } = null!;
}