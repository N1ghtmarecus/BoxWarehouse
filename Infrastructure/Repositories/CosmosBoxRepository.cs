using Cosmonaut;
using Cosmonaut.Extensions;
using Domain.Entities.Cosmos;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class CosmosBoxRepository : ICosmosBoxRepository
    {
        private readonly ICosmosStore<CosmosBox> _cosmosStore;
        public CosmosBoxRepository(ICosmosStore<CosmosBox> cosmosStore) 
        {
            _cosmosStore = cosmosStore;
        }
        public async Task<IEnumerable<CosmosBox>> GetAllAsync()
        {
            var boxes = await _cosmosStore.Query().ToListAsync();
            return boxes;
        }

        public async Task<CosmosBox> GetByIdAsync(string id)
        {
            return await _cosmosStore.FindAsync(id);
        }

        public async Task<CosmosBox> AddAsync(CosmosBox box)
        {
            box.ID = Guid.NewGuid().ToString();
            return await _cosmosStore.AddAsync(box);
        }

        public async Task UpdateAsync(CosmosBox box)
        {
            await _cosmosStore.UpdateAsync(box);
        }

        public async Task DeleteAsync(CosmosBox box)
        {
            await _cosmosStore.RemoveAsync(box);
        }
    }
}
