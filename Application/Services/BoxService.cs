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
            var boxes = _boxRepository.GetAll();
            return _mapper.ProjectTo<BoxDto>(boxes);
        }

        public async Task<IEnumerable<BoxDto>> GetAllBoxesAsync(int pageNumber, int pageSize, string sortField, bool ascending, string filterCutterId)
        {
            _logger.LogDebug("Person {_principal.Identity.Name} fetching all boxes", _principal.Identity!.Name);
            _logger.LogInformation("Person {_principal.Identity.Name} filled pageNumber: {pageNumber} | pageSize: {pageSize} | sortField: {sortField} | ascending: {ascending} | filterCutterId: {filterCutterId}", _principal.Identity.Name, pageNumber, pageSize, sortField, ascending, filterCutterId);
            
            var boxes = await _boxRepository.GetAllAsync(pageNumber, pageSize, sortField, ascending, filterCutterId);
            return _mapper.Map<IEnumerable<BoxDto>>(boxes);
        }

        public async Task<int> GetAllBoxesCountAsync(string filterCutterId)
        {
            return await _boxRepository.GetAllCountAsync(filterCutterId);
        }

        public async Task<BoxDto> GetBoxByCutterIdAsync(int id)
        {
            var box = await _boxRepository.GetByCutterIdAsync(id);
            return _mapper.Map<BoxDto>(box);
        }

        public async Task<IEnumerable<BoxDto>> GetBoxesByDimensionAsync(string dimension, int dimensionValue)
        {
            var boxes = await _boxRepository.GetByDimensionAsync(dimension, dimensionValue);
            return _mapper.Map<IEnumerable<BoxDto>>(boxes);

        }

        public async Task<IEnumerable<BoxDto>> GetBoxesByDimensionRangeAsync(string dimension, int lowerBound, int upperBound)
        {
            var boxes = await _boxRepository.GetByDimensionRangeAsync(dimension, lowerBound, upperBound);
            return _mapper.Map<IEnumerable<BoxDto>>(boxes);
        }

        public async Task<BoxDto> AddNewBoxAsync(CreateBoxDto newBox, string userId)
        {
            var existingBox = await _boxRepository.GetByCutterIdAsync(newBox!.CutterID);
            if (existingBox != null)
            {
                throw new Exception($"Box with Cutter ID {newBox.CutterID} already exists");
            }

            var box = _mapper.Map<Box>(newBox);
            box.UserId = userId;
            var result = await _boxRepository.AddAsync(box);
            return _mapper.Map<BoxDto>(result);
        }

        public async Task UpdateBoxAsync(BoxDto updateBox)
        {
            var existingBox = await _boxRepository.GetByCutterIdAsync(updateBox.CutterID);
            var box = _mapper.Map(updateBox, existingBox);
            await _boxRepository.UpdateAsync(box!);
        }

        public async Task DeleteBoxAsync(int id)
        {
            var box = await _boxRepository.GetByCutterIdAsync(id);
            await _boxRepository.DeleteAsync(box!);
        }
    }
}
