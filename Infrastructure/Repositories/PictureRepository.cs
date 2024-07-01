using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        private readonly BoxWarehouseContext _context;

        public PictureRepository(BoxWarehouseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Picture>> GetByBoxCutterIdAsync(int boxCutterId)
        {
            return await _context.Pictures
                .Include(p => p.Boxes)
                .Where(p => p.Boxes!.Select(b => b.CutterID).Contains(boxCutterId))
                .ToListAsync();
        }

        public async Task<Picture?> GetMainPictureForBoxAsync(int? boxCutterId)
        {
            return await _context.Pictures
                .Where(p => p.Boxes!.Any(b => b.CutterID == boxCutterId) && p.IsMain)
                .FirstOrDefaultAsync();
        }

        public async Task<Picture?> GetByIdAsync(int? id)
        {
            return await _context.Pictures
                .Include(p => p.Boxes)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Picture> AddAsync(Picture picture)
        {
            var createdPicture = await _context.Pictures.AddAsync(picture);
            await _context.SaveChangesAsync();
            return createdPicture.Entity;
        }

        public async Task UpdateAsync(Picture picture)
        {
            _context.Pictures.Update(picture);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Picture picture)
        {
            _context.Pictures.Remove(picture);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
    }
}
