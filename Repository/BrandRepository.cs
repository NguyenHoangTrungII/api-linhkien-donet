
using _01_WEBAPI.Data;
using _01_WEBAPI.Interfaces;
using linhkien_donet.Entities;
using Microsoft.EntityFrameworkCore;

namespace _01_webapi.respository
{
    public class Brandrepository : IBrandRepository

    {
        private readonly DataContext _context;

        public Brandrepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> BrandExists(int id)
        {
            return await _context.Brand.AnyAsync(b => b.Id == id);
        }
        public async Task<ICollection<Brand>> GetAllBrand()
        {
            return await _context.Brand.OrderBy(p => p.Id).ToArrayAsync();
        }
        public async Task<Brand> GetBrandById(int brandid)
        {
            return await _context.Brand.FirstOrDefaultAsync(b => b.Id == brandid);
        }

        public async Task<bool> CreateBrand(Brand brandnew)
        {
            var Brand = new Brand()
            {
                Name = brandnew.Name,

            };


            await _context.Brand.AddAsync(Brand);
            var result = await _context.SaveChangesAsync() > 0;
            return result;

        }

        public async Task<bool> UpdateBrand(Brand Brand)
        {
            _context.Brand.Update(Brand);
            var result = await _context.SaveChangesAsync() > 0;

            return result;
        }

        public async Task<bool> DeleteBrand(Brand Brand)
        {
            _context.Brand.Remove(Brand);
            var result = await _context.SaveChangesAsync() > 0;

            return result;
        }
    }
}
