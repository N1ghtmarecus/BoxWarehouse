using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.ExtensionMethods;
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

        public IQueryable<Box> GetAll()
        {
            return _context.Boxes.AsQueryable();
        }

        public async Task<IEnumerable<Box>> GetAllAsync(int pageNumber, int pageSize, string sortField, bool ascending, string filterCutterId)
        {
            return await _context.Boxes
                .Where(m => m.CutterID.ToString().Contains(filterCutterId, StringComparison.CurrentCultureIgnoreCase))
                .OrderByPropertyName(sortField, ascending)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetAllCountAsync(string filterCutterId)
        {
            return await _context.Boxes
                .Where(m => m.CutterID.ToString().Contains(filterCutterId, StringComparison.CurrentCultureIgnoreCase))
                .CountAsync();
        }

        public async Task<Box?> GetByCutterIdAsync(int id)
        {
            return await _context.Boxes.SingleOrDefaultAsync(x => x.CutterID == id);
        }

        public async Task<IEnumerable<Box>> GetByDimensionAsync(string dimension, int dimensionValue)
        {
            return dimension switch
            {
                "length" => await _context.Boxes.Where(x => x.Length == dimensionValue).ToListAsync(),
                "width" => await _context.Boxes.Where(x => x.Width == dimensionValue).ToListAsync(),
                "height" => await _context.Boxes.Where(x => x.Height == dimensionValue).ToListAsync(),
                _ => await Task.FromResult(new List<Box>()),
            };
        }

        public async Task<IEnumerable<Box>> GetByDimensionRangeAsync(string dimension, int lowerBound, int upperBound)
        {
            return dimension switch
            {
                "length" => await _context.Boxes.Where(x => x.Length >= lowerBound && x.Length <= upperBound).ToListAsync(),
                "width" => await _context.Boxes.Where(x => x.Width >= lowerBound && x.Width <= upperBound).ToListAsync(),
                "height" => await _context.Boxes.Where(x => x.Height >= lowerBound && x.Height <= upperBound).ToListAsync(),
                _ => await Task.FromResult(new List<Box>()),
            };
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
