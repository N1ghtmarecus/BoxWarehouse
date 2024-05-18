using Application.Dto;

namespace Application.Interfaces
{
    public interface IBoxService
    {
        IEnumerable<BoxDto> GetAllBoxes();
        BoxDto GetBoxByCutterId(int id);
        BoxDto AddNewBox(BoxDto newBox);
        void UpdateBox(BoxDto updateBox);
        void DeleteBox(int id);
    }
}
