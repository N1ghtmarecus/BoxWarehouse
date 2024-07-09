using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Principal;

namespace Application.Services
{
    public class BoxService : IBoxService
    {
        private readonly IBoxRepository _boxRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IPrincipal _principal;

        public BoxService(IBoxRepository boxRepository, IMapper mapper, ILogger<BoxService> logger, IPrincipal principal)
        {
            _boxRepository = boxRepository;
            _mapper = mapper;
            _logger = logger;
            _principal = principal;
        }

        public IQueryable<BoxDto> GetAllBoxes()
        {
            _logger.LogDebug("{_principal.Identity.Name} fetching all boxes", _principal.Identity!.Name);
            var boxes = _boxRepository.GetAll();

            _logger.LogInformation("{_principal.Identity.Name} fetched {boxes.Count()} boxes", _principal.Identity.Name, boxes.Count());
            return _mapper.ProjectTo<BoxDto>(boxes);
        }

        public async Task<IEnumerable<BoxDto>> GetAllBoxesAsync(int pageNumber, int pageSize, string sortField, bool ascending, string filterCutterId)
        {
            _logger.LogInformation("{_principal.Identity.Name} filled pageNumber: {pageNumber} | pageSize: {pageSize} | sortField: {sortField} | ascending: {ascending} | filterCutterId: {filterCutterId}", _principal.Identity.Name, pageNumber, pageSize, sortField, ascending, filterCutterId);
            
            var boxes = await _boxRepository.GetAllAsync(pageNumber, pageSize, sortField, ascending, filterCutterId);

            _logger.LogInformation("{_principal.Identity.Name} fetched {boxes.Count()} boxes", _principal.Identity.Name, boxes.Count());
            return _mapper.Map<IEnumerable<BoxDto>>(boxes);
        }

        public async Task<int> GetAllBoxesCountAsync(string filterCutterId)
        {
            return await _boxRepository.GetAllCountAsync(filterCutterId);
        }

        public async Task<BoxDto> GetBoxByCutterIdAsync(int id)
        {
            _logger.LogInformation("{_principal.Identity.Name} fetching box by Cutter ID: {id}", _principal.Identity!.Name, id);
            var box = await _boxRepository.GetByCutterIdAsync(id);

            _logger.LogInformation("{_principal.Identity.Name} fetched box by Cutter ID: {id}", _principal.Identity!.Name, id);
            return _mapper.Map<BoxDto>(box);
        }

        public async Task<IEnumerable<BoxDto>> GetBoxesByDimensionAsync(string dimension, int dimensionValue)
        {
            _logger.LogInformation("{_principal.Identity.Name} fetching boxes by dimension: {dimension} | dimensionValue: {dimensionValue}", _principal.Identity!.Name, dimension, dimensionValue);
            var boxes = await _boxRepository.GetByDimensionAsync(dimension, dimensionValue);

            _logger.LogInformation("{_principal.Identity.Name} fetched {boxes.Count()} boxes by dimension: {dimension} | dimensionValue: {dimensionValue}", _principal.Identity!.Name, boxes.Count(), dimension, dimensionValue);
            return _mapper.Map<IEnumerable<BoxDto>>(boxes);

        }

        public async Task<IEnumerable<BoxDto>> GetBoxesByDimensionRangeAsync(string dimension, int lowerBound, int upperBound)
        {
            _logger.LogInformation("{_principal.Identity.Name} fetching boxes by dimension: {dimension} | lowerBound: {lowerBound} | upperBound: {upperBound}", _principal.Identity!.Name, dimension, lowerBound, upperBound);
            var boxes = await _boxRepository.GetByDimensionRangeAsync(dimension, lowerBound, upperBound);

            _logger.LogInformation("{_principal.Identity.Name} fetched {boxes.Count()} boxes by dimension: {dimension} | lowerBound: {lowerBound} | upperBound: {upperBound}", _principal.Identity!.Name, boxes.Count(), dimension, lowerBound, upperBound);
            return _mapper.Map<IEnumerable<BoxDto>>(boxes);
        }

        public async Task<BoxDto> AddNewBoxAsync(CreateBoxDto newBox, string userId)
        {
            _logger.LogInformation("{_principal.Identity.Name} adding new box with Cutter ID: {newBox.CutterID}", _principal.Identity!.Name, newBox.CutterID);
            var existingBox = await _boxRepository.GetByCutterIdAsync(newBox!.CutterID);
            if (existingBox != null)
            {
                _logger.LogError("Box with Cutter ID {newBox.CutterID} already exists", newBox.CutterID);
                throw new Exception($"Box with Cutter ID {newBox.CutterID} already exists");
            }

            var box = _mapper.Map<Box>(newBox);
            box.UserId = userId;
            var result = await _boxRepository.AddAsync(box);

            _logger.LogInformation("{_principal.Identity.Name} added new box with Cutter ID: {newBox.CutterID}", _principal.Identity!.Name, newBox.CutterID);
            return _mapper.Map<BoxDto>(result);
        }

        public async Task UpdateBoxAsync(BoxDto updateBox)
        {
            _logger.LogInformation("{_principal.Identity.Name} updating box with Cutter ID: {updateBox.CutterID}", _principal.Identity!.Name, updateBox.CutterID);
            var existingBox = await _boxRepository.GetByCutterIdAsync(updateBox.CutterID);
            var box = _mapper.Map(updateBox, existingBox);

            _logger.LogInformation("{_principal.Identity.Name} updated box with Cutter ID: {updateBox.CutterID}", _principal.Identity!.Name, updateBox.CutterID);
            await _boxRepository.UpdateAsync(box!);
        }

        public async Task DeleteBoxAsync(int id)
        {
            _logger.LogInformation("{_principal.Identity.Name} deleting box with Cutter ID: {id}", _principal.Identity!.Name, id);
            var box = await _boxRepository.GetByCutterIdAsync(id);

            _logger.LogInformation("{_principal.Identity.Name} deleted box with Cutter ID: {id}", _principal.Identity!.Name, id);
            await _boxRepository.DeleteAsync(box!);
        }
    }
}
