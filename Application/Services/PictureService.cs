using Application.Dto;
using Application.ExtensionMethods;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class PictureService : IPictureService
    {
        private readonly IPictureRepository _pictureRepository;
        private readonly IBoxRepository _boxRepository;
        private readonly IMapper _mapper;

        public PictureService(IPictureRepository pictureRepository, IBoxRepository boxRepository, IMapper mapper)
        {
            _pictureRepository = pictureRepository;
            _boxRepository = boxRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PictureDto>> GetPicturesByBoxCutterIdAsync(int boxCutterId)
        {
            var box = await _boxRepository.GetByCutterIdAsync(boxCutterId) ?? throw new ArgumentException($"Box with Cutter Id '{boxCutterId}' does not exist.");

            var pictures = await _pictureRepository.GetByBoxCutterIdAsync(boxCutterId);
            return _mapper.Map<IEnumerable<PictureDto>>(pictures);
        }

        public async Task<PictureDto> GetPictureByIdAsync(int id)
        {
            var picture = await _pictureRepository.GetByIdAsync(id);
            return _mapper.Map<PictureDto>(picture);
        }

        public async Task<PictureDto> AddPictureToBoxAsync(int boxCutterId, IFormFile file, bool isMain)
        {
            var box = await _boxRepository.GetByCutterIdAsync(boxCutterId);

            if (isMain)
            {
                var mainPicture = await _pictureRepository.GetMainPictureForBoxAsync(boxCutterId);
                if (mainPicture != null)
                {
                    mainPicture.IsMain = false;
                    await _pictureRepository.UpdateAsync(mainPicture);
                }
            }

            var picture = new Picture()
            {
                Boxes = new List<Box> { box! },
                Name = file.FileName,
                Image = file.GetBytes(),
                IsMain = isMain
            };

            var result = await _pictureRepository.AddAsync(picture);
            return _mapper.Map<PictureDto>(result);
        }

        public async Task DeletePictureAsync(int id)
        {
            var picture = await _pictureRepository.GetByIdAsync(id);
            await _pictureRepository.DeleteAsync(picture!);
        }

        public async Task UpdatePictureAsync(PictureDto picture, bool isMain)
        {
            var existingPicture = await _pictureRepository.GetByIdAsync(picture.Id);

            if (isMain)
            {
                var mainPicture = await _pictureRepository.GetMainPictureForBoxAsync(existingPicture!.Boxes!.FirstOrDefault()!.CutterID);
                if (mainPicture != null)
                {
                    mainPicture.IsMain = false;
                    await _pictureRepository.UpdateAsync(mainPicture);
                }
            }

            await _pictureRepository.UpdateAsync(_mapper.Map(picture, existingPicture)!);
        }
    }
}
