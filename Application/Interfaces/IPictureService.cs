using Application.Dto;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IPictureService
    {
        Task<PictureDto> AddPictureToBoxAsync(int boxCutterId, IFormFile file);
    }
}
