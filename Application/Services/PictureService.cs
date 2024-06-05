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

        public async Task<PictureDto> AddPictureToBoxAsync(int boxCutterId, IFormFile file)
        {
            var box = await _boxRepository.GetByCutterIdAsync(boxCutterId);

            var picture = new Picture()
            {
                Boxes = new List<Box> { box! },
                Name = file.Name,
                Image = file.GetBytes(),
                IsMain = true
            };

            var result = await _pictureRepository.AddAsync(picture);
            return _mapper.Map<PictureDto>(result);
        }
    }
}
