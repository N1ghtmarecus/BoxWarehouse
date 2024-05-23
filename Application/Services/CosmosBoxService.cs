using Application.Dto.Cosmos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Cosmos;
using Domain.Interfaces;

namespace Application.Services
{
    public class CosmosBoxService : ICosmosBoxService
    {
        private readonly ICosmosBoxRepository _boxRepository;
        private readonly IMapper _mapper;

        public CosmosBoxService(ICosmosBoxRepository boxRepository, IMapper mapper)
        {
            _boxRepository = boxRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CosmosBoxDto>> GetAllBoxesAsync()
        {
            var boxes = await _boxRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CosmosBoxDto>>(boxes);
        }

        public async Task<CosmosBoxDto> GetBoxByCutterIdAsync(string id)
        {
            var box = await _boxRepository.GetByCutterIdAsync(id);
            return _mapper.Map<CosmosBoxDto>(box);
        }

        public async Task<CosmosBoxDto> AddNewBoxAsync(CosmosBoxDto newBox)
        {
            if (newBox?.CutterID == null)
            {
                throw new Exception("Cutter ID is required");
            }

            var box = _mapper.Map<CosmosBox>(newBox);
            var result = await _boxRepository.AddAsync(box);
            return _mapper.Map<CosmosBoxDto>(result);
        }

        public async Task UpdateBoxAsync(CosmosBoxDto updateBox)
        {
            var existingBox = await _boxRepository.GetByCutterIdAsync(updateBox.CutterID!);
            var box = _mapper.Map(updateBox, existingBox);
            await _boxRepository.UpdateAsync(box!);
        }

        public async Task DeleteBoxAsync(string id)
        {
            var box = await _boxRepository.GetByCutterIdAsync(id);
            await _boxRepository.DeleteAsync(box!);
        }
    }
}
