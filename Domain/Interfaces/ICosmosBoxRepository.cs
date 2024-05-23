using Domain.Entities.Cosmos;

namespace Domain.Interfaces
{
    public interface ICosmosBoxRepository
    {
        Task<IEnumerable<CosmosBox>> GetAllAsync();
        Task<CosmosBox> GetByCutterIdAsync(string id);
        Task<CosmosBox> AddAsync(CosmosBox box);
        Task UpdateAsync(CosmosBox box);
        Task DeleteAsync(CosmosBox box);
    }
}
