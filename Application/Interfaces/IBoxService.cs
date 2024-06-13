using Application.Dto;

namespace Application.Interfaces
{
    public interface IBoxService
    {
        IQueryable<BoxDto> GetAllBoxes();
        Task<IEnumerable<BoxDto>> GetAllBoxesAsync(int pageNumber, int pageSize, string sortField, bool ascending, string filterCutterId);
        Task<int> GetAllBoxesCountAsync(string filterCutterId);
        Task<BoxDto> GetBoxByCutterIdAsync(int id);
        Task<IEnumerable<BoxDto>> GetBoxesByDimensionAsync(string dimension, int dimensionValue);
        Task<IEnumerable<BoxDto>> GetBoxesByDimensionRangeAsync(string dimension, int lowerBound, int upperBound);
        Task<BoxDto> AddNewBoxAsync(CreateBoxDto newBox, string userId);
        Task UpdateBoxAsync(BoxDto updateBox);
        Task DeleteBoxAsync(int id);
    }
}
