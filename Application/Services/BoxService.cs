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

        public IEnumerable<BoxDto> GetAllBoxes()
        {
            var boxes = _boxRepository.GetAll();
            return _mapper.Map<IEnumerable<BoxDto>>(boxes);
        }

        public BoxDto GetBoxByCutterId(int id)
        {
            var box = _boxRepository.GetByCutterId(id);
            return _mapper.Map<BoxDto>(box);
        }

        public BoxDto AddNewBox(BoxDto newBox)
        {
            if (newBox?.CutterID == null)
            {
                throw new Exception("Cutter ID is required");
            }

            var box = _mapper.Map<Box>(newBox);
            _boxRepository.Add(box);
            return _mapper.Map<BoxDto>(box);
        }

        public void UpdateBox(BoxDto updateBox)
        {
            var existingBox = _boxRepository.GetByCutterId(updateBox.CutterID);
            var box = _mapper.Map(updateBox, existingBox);
            _boxRepository.Update(box);
        }

        public void DeleteBox(int id)
        {
            var box = _boxRepository.GetByCutterId(id);
            _boxRepository.Delete(box);
        }
    }
}
