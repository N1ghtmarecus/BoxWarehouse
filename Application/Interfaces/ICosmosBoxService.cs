using Application.Dto.Cosmos;

namespace Application.Interfaces
{
    public interface ICosmosBoxService
    {
        Task<IEnumerable<CosmosBoxDto>> GetAllBoxesAsync();
        Task<CosmosBoxDto> GetBoxByCutterIdAsync(string id);
        Task<CosmosBoxDto> AddNewBoxAsync(CosmosBoxDto newBox);
        Task UpdateBoxAsync(CosmosBoxDto updateBox);
        Task DeleteBoxAsync(string id);
    }
}
