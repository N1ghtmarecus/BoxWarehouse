using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        private readonly BoxWarehouseContext _context;

        public PictureRepository(BoxWarehouseContext context)
        {
            _context = context;
        }

        public async Task<Picture> AddAsync(Picture picture)
        {
            var createdPicture = await _context.Pictures.AddAsync(picture);
            await _context.SaveChangesAsync();
            return createdPicture.Entity;
        }
    }
}
