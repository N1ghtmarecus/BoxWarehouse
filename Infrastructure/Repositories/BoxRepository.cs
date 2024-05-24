using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BoxRepository : IBoxRepository
    {
        private readonly BoxWarehouseContext _context;

        public BoxRepository(BoxWarehouseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Box>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Boxes.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Box?> GetByCutterIdAsync(int id)
        {
            return await _context.Boxes.SingleOrDefaultAsync(x => x.CutterID == id);
        }

        public async Task<Box> AddAsync(Box box)
        {
            var createdBox = await _context.Boxes.AddAsync(box);
            await _context.SaveChangesAsync();
            return createdBox.Entity;
        }

        public async Task UpdateAsync(Box box)
        {
            _context.Boxes.Update(box);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Box box)
        {
            _context.Boxes.Remove(box);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
    }
}
