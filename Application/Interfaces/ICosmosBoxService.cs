using Application.Dto.Cosmos;

namespace Application.Interfaces
{
    public interface ICosmosBoxService
    {
        Task<IEnumerable<CosmosBoxDto>> GetAllBoxesAsync();
        Task<CosmosBoxDto> GetBoxByIdAsync(string id);
        Task<CosmosBoxDto> AddNewBoxAsync(CreateCosmosBoxDto newBox);
        Task UpdateBoxAsync(UpdateCosmosBoxDto updateBox);
        Task DeleteBoxAsync(string id);
    }
}
