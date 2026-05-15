using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Dtos;
using WebApplication7.Models;

namespace WebApplication7.Services;

public class PcService : IPcService
{
    private readonly AppDbContext _context;

    public PcService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PcListDto>> GetAllAsync()
    {
        return await _context.Pcs
            .Select(pc => new PcListDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            })
            .ToListAsync();
    }

    public async Task<PcDetailsDto?> GetByIdWithComponentsAsync(int id)
    {
        return await _context.Pcs
            .Where(pc => pc.Id == id)
            .Select(pc => new PcDetailsDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock,
                Components = pc.PcComponents.Select(pcComponent => new PcComponentDto
                {
                    Amount = pcComponent.Amount,
                    Component = new ComponentDto
                    {
                        Code = pcComponent.Component.Code,
                        Name = pcComponent.Component.Name,
                        Description = pcComponent.Component.Description,
                        Manufacturer = new ManufacturerDto
                        {
                            Id = pcComponent.Component.Manufacturer.Id,
                            Abbreviation = pcComponent.Component.Manufacturer.Abbreviation,
                            FullName = pcComponent.Component.Manufacturer.FullName,
                            FoundationDate = pcComponent.Component.Manufacturer.FoundationDate
                        },
                        Type = new ComponentTypeDto
                        {
                            Id = pcComponent.Component.Type.Id,
                            Abbreviation = pcComponent.Component.Type.Abbreviation,
                            Name = pcComponent.Component.Type.Name
                        }
                    }
                }).ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PcListDto> CreateAsync(PcRequestDto request)
    {
        var pc = new Pc
        {
            Name = request.Name,
            Weight = request.Weight,
            Warranty = request.Warranty,
            CreatedAt = request.CreatedAt,
            Stock = request.Stock
        };

        _context.Pcs.Add(pc);
        await _context.SaveChangesAsync();

        return new PcListDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<bool> UpdateAsync(int id, PcRequestDto request)
    {
        var pc = await _context.Pcs.FirstOrDefaultAsync(x => x.Id == id);

        if (pc is null)
        {
            return false;
        }

        pc.Name = request.Name;
        pc.Weight = request.Weight;
        pc.Warranty = request.Warranty;
        pc.CreatedAt = request.CreatedAt;
        pc.Stock = request.Stock;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pc = await _context.Pcs.FirstOrDefaultAsync(x => x.Id == id);

        if (pc is null)
        {
            return false;
        }

        _context.Pcs.Remove(pc);
        await _context.SaveChangesAsync();

        return true;
    }
}