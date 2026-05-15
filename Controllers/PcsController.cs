using Microsoft.AspNetCore.Mvc;
using WebApplication7.Dtos;
using WebApplication7.Services;

namespace WebApplication7.Controllers;

[ApiController]
[Route("api/pcs")]
public class PcsController : ControllerBase
{
    private readonly IPcService _pcService;

    public PcsController(IPcService pcService)
    {
        _pcService = pcService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PcListDto>>> GetAll()
    {
        var result = await _pcService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id:int}/components")]
    public async Task<ActionResult<PcDetailsDto>> GetByIdWithComponents(int id)
    {
        var result = await _pcService.GetByIdWithComponentsAsync(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PcListDto>> Create([FromBody] PcRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _pcService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetByIdWithComponents),
            new { id = result.Id },
            result
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PcRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updated = await _pcService.UpdateAsync(id, request);

        if (!updated)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _pcService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}