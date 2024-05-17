using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class BoxRepository : IBoxRepository
    {
        private static readonly ISet<Box> _boxes = new HashSet<Box>()
        {
            new(001, 427, 100, 80, 60),
            new(002, 427, 120, 100, 80),
            new(003, 427, 200, 100, 100),
            new(101, 201, 80, 70, 40),
            new(102, 201, 90, 60, 80),
            new(103, 201, 205, 115, 60)
        };

        public IEnumerable<Box> GetAll()
        {
            return _boxes;
        }

        public Box GetByCutterId(int id)
        {
            return _boxes.SingleOrDefault(x => x.CutterID == id)!;
        }

        public Box Add(Box box)
        {
            box.Created = DateTime.UtcNow;
            _boxes.Add(box);
            return box;
        }

        public void Update(Box box)
        {
            box.LastModified = DateTime.UtcNow;
        }

        public void Delete(Box box)
        {
            _boxes.Remove(box);
        }
    }
}
