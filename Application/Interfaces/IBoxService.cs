using Application.Dto;

namespace Application.Interfaces
{
    public interface IBoxService
    {
        Task<IEnumerable<BoxDto>> GetAllBoxesAsync(int pageNumber, int pageSize, string sortField, bool ascending);
        Task<int> GetAllBoxesCountAsync();
        Task<BoxDto> GetBoxByCutterIdAsync(int id);
        Task<BoxDto> AddNewBoxAsync(CreateBoxDto newBox);
        Task UpdateBoxAsync(BoxDto updateBox);
        Task DeleteBoxAsync(int id);
    }
}
