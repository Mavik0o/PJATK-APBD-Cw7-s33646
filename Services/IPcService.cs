using WebApplication7.Dtos;

namespace WebApplication7.Services;

public interface IPcService
{
    Task<List<PcListDto>> GetAllAsync();

    Task<PcDetailsDto?> GetByIdWithComponentsAsync(int id);

    Task<PcListDto> CreateAsync(PcRequestDto request);

    Task<bool> UpdateAsync(int id, PcRequestDto request);

    Task<bool> DeleteAsync(int id);
}