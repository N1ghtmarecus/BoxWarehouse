using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class BoxRepository : IBoxRepository
    {
        private readonly BoxWarehouseContext _context;

        public BoxRepository(BoxWarehouseContext context)
        {
            _context = context;
        }

        public IEnumerable<Box> GetAll()
        {
            return _context.Boxes;
        }

        public Box GetByCutterId(int id)
        {
            return _context.Boxes.SingleOrDefault(x => x.CutterID == id)!;
        }

        public Box Add(Box box)
        {
            _context.Boxes.Add(box);
            _context.SaveChanges();
            return box;
        }

        public void Update(Box box)
        {
            _context.Boxes.Update(box);
            _context.SaveChanges();
        }

        public void Delete(Box box)
        {
            _context.Boxes.Remove(box);
            _context.SaveChanges();
        }
    }
}
