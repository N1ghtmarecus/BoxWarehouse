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

        public async Task<CosmosBoxDto> GetBoxByIdAsync(string id)
        {
            var box = await _boxRepository.GetByIdAsync(id);
            return _mapper.Map<CosmosBoxDto>(box);
        }

        public async Task<CosmosBoxDto> AddNewBoxAsync(CreateCosmosBoxDto newBox)
        {
            if (newBox?.CutterID == null)
            {
                throw new Exception("Cutter ID is required");
            }

            var box = _mapper.Map<CosmosBox>(newBox);
            var result = await _boxRepository.AddAsync(box);
            return _mapper.Map<CosmosBoxDto>(result);
        }

        public async Task UpdateBoxAsync(UpdateCosmosBoxDto updateBox)
        {
            var existingBox = await _boxRepository.GetByIdAsync(updateBox.ID!);
            var box = _mapper.Map(updateBox, existingBox);
            await _boxRepository.UpdateAsync(box!);
        }

        public async Task DeleteBoxAsync(string id)
        {
            var box = await _boxRepository.GetByIdAsync(id);
            await _boxRepository.DeleteAsync(box!);
        }
    }
}
