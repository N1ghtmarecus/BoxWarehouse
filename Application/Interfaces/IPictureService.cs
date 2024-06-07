using Application.Dto;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IPictureService
    {
        Task<IEnumerable<PictureDto>> GetPicturesByBoxCutterIdAsync(int boxCutterId);
        Task<PictureDto> GetPictureByIdAsync(int id);
        Task<PictureDto> AddPictureToBoxAsync(int boxCutterId, IFormFile file, bool isMain);
        Task DeletePictureAsync(int id);
    }
}
