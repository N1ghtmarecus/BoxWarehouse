using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IBoxRepository
    {
        IQueryable<Box> GetAll();
        Task<IEnumerable<Box>> GetAllAsync(int pageNumber, int pageSize, string sortField, bool ascending, string filterCutterId);
        Task<int> GetAllCountAsync(string filterCutterId);
        Task<Box?> GetByCutterIdAsync(int id);
        Task<IEnumerable<Box>> GetByDimensionAsync(string dimension, int dimensionValue);
        Task<IEnumerable<Box>> GetByDimensionRangeAsync(string dimension, int lowerBound, int upperBound);
        Task<Box> AddAsync(Box box);
        Task UpdateAsync(Box box);
        Task DeleteAsync(Box box);
    }
}
