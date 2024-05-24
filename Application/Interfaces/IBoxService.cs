using Application.Dto;

namespace Application.Interfaces
{
    public interface IBoxService
    {
        Task<IEnumerable<BoxDto>> GetAllBoxesAsync(int pageNumber, int pageSize);
        Task<int> GetAllBoxesCountAsync();
        Task<BoxDto> GetBoxByCutterIdAsync(int id);
        Task<BoxDto> AddNewBoxAsync(BoxDto newBox);
        Task UpdateBoxAsync(BoxDto updateBox);
        Task DeleteBoxAsync(int id);
    }
}
