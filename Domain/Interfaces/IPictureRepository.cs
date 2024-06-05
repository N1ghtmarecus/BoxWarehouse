using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPictureRepository
    {
        Task<Picture> AddAsync(Picture picture);
    }
}
