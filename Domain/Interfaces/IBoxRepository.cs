using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IBoxRepository
    {
        IEnumerable<Box> GetAll();
        Box GetById(int id);
        Box Add(Box box);
        void Update(Box box);
        void Delete(Box box);
    }
}
