using Application.Dto;

namespace Application.Interfaces
{
    public interface IBoxService
    {
        IQueryable<BoxDto> GetAllBoxes();
        Task<IEnumerable<BoxDto>> GetAllBoxesAsync(int pageNumber, int pageSize, string sortField, bool ascending, string filterCutterId);
        Task<int> GetAllBoxesCountAsync(string filterCutterId);
        Task<BoxDto> GetBoxByCutterIdAsync(int id);
        Task<BoxDto> AddNewBoxAsync(CreateBoxDto newBox);
        Task UpdateBoxAsync(BoxDto updateBox);
        Task DeleteBoxAsync(int id);
    }
}
