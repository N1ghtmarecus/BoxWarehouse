using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class BoxService : IBoxService
    {
        private readonly IBoxRepository _boxRepository;
        private readonly IMapper _mapper;

        public BoxService(IBoxRepository boxRepository, IMapper mapper)
        {
            _boxRepository = boxRepository;
            _mapper = mapper;
        }

        public IQueryable<BoxDto> GetAllBoxes()
        {
            var boxes = _boxRepository.GetAll();
            return _mapper.ProjectTo<BoxDto>(boxes);
        }

        public async Task<IEnumerable<BoxDto>> GetAllBoxesAsync(int pageNumber, int pageSize, string sortField, bool ascending, string filterCutterId)
        {
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

        public async Task<IEnumerable<BoxDto>> GetBoxesByLengthAsync(int length)
        {
            var boxes = await _boxRepository.GetByLengthAsync(length);
            return _mapper.Map<IEnumerable<BoxDto>>(boxes);
        }

        public async Task<IEnumerable<BoxDto>> GetBoxesByLengthRangeAsync(int lowerBound, int upperBound)
        {
            var boxes = await _boxRepository.GetByLengthRangeAsync(lowerBound, upperBound);
            return _mapper.Map<IEnumerable<BoxDto>>(boxes);
        }

        public async Task<BoxDto> AddNewBoxAsync(CreateBoxDto newBox, string userId)
        {
            if (newBox?.CutterID == null)
            {
                throw new Exception("Cutter ID is required");
            }

            var existingBox = await _boxRepository.GetByCutterIdAsync(newBox.CutterID);
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
