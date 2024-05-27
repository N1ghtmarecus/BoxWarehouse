using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IBoxRepository
    {
        Task<IEnumerable<Box>> GetAllAsync(int pageNumber, int pageSize, string sortField, bool ascending, string filterCutterId);
        Task<int> GetAllCountAsync(string filterCutterId);
        Task<Box?> GetByCutterIdAsync(int id);
        Task<Box> AddAsync(Box box);
        Task UpdateAsync(Box box);
        Task DeleteAsync(Box box);
    }
}
