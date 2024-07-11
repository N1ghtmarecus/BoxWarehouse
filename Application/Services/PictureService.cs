using Application.Dto;
using Application.ExtensionMethods;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Principal;

namespace Application.Services
{
    public class PictureService : IPictureService
    {
        private readonly IPictureRepository _pictureRepository;
        private readonly IBoxRepository _boxRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IPrincipal _principal;

        public PictureService(IPictureRepository pictureRepository, IBoxRepository boxRepository, IMapper mapper, ILogger logger, IPrincipal principal)
        {
            _pictureRepository = pictureRepository;
            _boxRepository = boxRepository;
            _mapper = mapper;
            _logger = logger;
            _principal = principal;
        }

        public async Task<IEnumerable<PictureDto>> GetPicturesByBoxCutterIdAsync(int boxCutterId)
        {
            _logger.LogInformation("{_principal.Identity.Name} fetching pictures for box with Cutter ID: {boxCutterId}", _principal.Identity!.Name, boxCutterId);
            var box = await _boxRepository.GetByCutterIdAsync(boxCutterId) ?? throw new ArgumentException($"Box with Cutter Id '{boxCutterId}' does not exist.");

            var pictures = await _pictureRepository.GetByBoxCutterIdAsync(boxCutterId);
            _logger.LogInformation("{_principal.Identity.Name} fetched {pictures.Count()} pictures for box with Cutter ID: {boxCutterId}", _principal.Identity!.Name, pictures.Count(), boxCutterId);
            return _mapper.Map<IEnumerable<PictureDto>>(pictures);
        }

        public async Task<PictureDto> GetPictureByIdAsync(int id)
        {
            _logger.LogInformation("{_principal.Identity.Name} fetching picture by ID: {id}", _principal.Identity!.Name, id);
            var picture = await _pictureRepository.GetByIdAsync(id);

            _logger.LogInformation("{_principal.Identity.Name} fetched picture by ID: {id}", _principal.Identity!.Name, id);
            return _mapper.Map<PictureDto>(picture);
        }

        public async Task<PictureDto> AddPictureToBoxAsync(int boxCutterId, IFormFile file, bool isMain)
        {
            var box = await _boxRepository.GetByCutterIdAsync(boxCutterId);

            if (isMain)
            {
                _logger.LogInformation("{_principal.Identity.Name} setting picture as main picture for box with Cutter ID: {boxCutterId}", _principal.Identity!.Name, boxCutterId);
                var mainPicture = await _pictureRepository.GetMainPictureForBoxAsync(boxCutterId);
                if (mainPicture != null)
                {
                    mainPicture.IsMain = false;

                    _logger.LogInformation("{_principal.Identity.Name} updating existing main picture for box with Cutter ID: {boxCutterId}", _principal.Identity!.Name, boxCutterId);
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

            _logger.LogInformation("{_principal.Identity.Name} adding picture to box with Cutter ID: {boxCutterId}", _principal.Identity!.Name, boxCutterId);
            var result = await _pictureRepository.AddAsync(picture);
            return _mapper.Map<PictureDto>(result);
        }

        public async Task DeletePictureAsync(int id)
        {
            _logger.LogInformation("{_principal.Identity.Name} deleting picture by ID: {id}", _principal.Identity!.Name, id);
            var picture = await _pictureRepository.GetByIdAsync(id);

            _logger.LogInformation("{_principal.Identity.Name} deleted picture by ID: {id}", _principal.Identity!.Name, id);
            await _pictureRepository.DeleteAsync(picture!);
        }

        public async Task UpdatePictureAsync(PictureDto picture, bool isMain)
        {
            _logger.LogInformation("{_principal.Identity.Name} updating picture by ID: {picture.Id}", _principal.Identity!.Name, picture.Id);
            var existingPicture = await _pictureRepository.GetByIdAsync(picture.Id) ?? throw new ArgumentException($"Picture with ID '{picture.Id}' does not exist.");

            switch (existingPicture.IsMain, isMain)
            {
                case (true, false):
                    _logger.LogError("{_principal.Identity.Name} cannot set the main picture to false for the current main picture.", _principal.Identity.Name);
                    throw new ArgumentException("Cannot set the main picture to false for the current main picture.");
                case (true, true):
                    _logger.LogError("{_principal.Identity.Name} cannot set to main picture because the picture is already the main picture.", _principal.Identity.Name);
                    throw new ArgumentException("The picture is already the main picture.");
                case (false, false):
                    _logger.LogError("{_principal.Identity.Name} cannot set to not main picture because the picture is already not the main picture.", _principal.Identity.Name);
                    throw new ArgumentException("The picture is already not the main picture.");
                default:
                    var mainPicture = await _pictureRepository.GetMainPictureForBoxAsync(existingPicture!.Boxes!.FirstOrDefault()!.CutterID);
                    if (mainPicture != null)
                    {
                        mainPicture.IsMain = false;

                        _logger.LogInformation("{_principal.Identity.Name} updating existing main picture for box with Cutter ID: {boxCutterId}", _principal.Identity!.Name, existingPicture.Boxes!.FirstOrDefault()!.CutterID);
                        await _pictureRepository.UpdateAsync(mainPicture);
                    }

                    await _pictureRepository.UpdateAsync(_mapper.Map(picture, existingPicture)!);
                    break;
            }
        }
    }
}
