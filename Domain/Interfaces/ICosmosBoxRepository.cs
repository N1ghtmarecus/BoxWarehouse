using Domain.Entities.Cosmos;

namespace Domain.Interfaces
{
    public interface ICosmosBoxRepository
    {
        Task<IEnumerable<CosmosBox>> GetAllAsync();
        Task<CosmosBox> GetByIdAsync(string id);
        Task<CosmosBox> AddAsync(CosmosBox box);
        Task UpdateAsync(CosmosBox box);
        Task DeleteAsync(CosmosBox box);
    }
}
