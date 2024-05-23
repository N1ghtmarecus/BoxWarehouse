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

        public async Task<IEnumerable<BoxDto>> GetAllBoxesAsync()
        {
            var boxes = await _boxRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BoxDto>>(boxes);
        }

        public async Task<BoxDto> GetBoxByCutterIdAsync(int id)
        {
            var box = await _boxRepository.GetByCutterIdAsync(id);
            return _mapper.Map<BoxDto>(box);
        }

        public async Task<BoxDto> AddNewBoxAsync(BoxDto newBox)
        {
            if (newBox?.CutterID == null)
            {
                throw new Exception("Cutter ID is required");
            }

            var box = _mapper.Map<Box>(newBox);
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
