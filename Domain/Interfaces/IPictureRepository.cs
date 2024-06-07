using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPictureRepository
    {
        Task<IEnumerable<Picture>> GetByBoxCutterIdAsync(int boxCutterId);
        Task<Picture?> GetMainPictureForBoxAsync(int? boxCutterId);
        Task<Picture?> GetByIdAsync(int? id);
        Task<Picture> AddAsync(Picture picture);
        Task UpdateAsync(Picture picture);
        Task DeleteAsync(Picture picture);
    }
}
