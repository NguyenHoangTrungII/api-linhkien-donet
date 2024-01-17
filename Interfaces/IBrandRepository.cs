using linhkien_donet.Entities;
using Microsoft.EntityFrameworkCore;

namespace _01_WEBAPI.Interfaces
{
    public interface IBrandRepository
    {
        Task<ICollection<Brand>> GetAllBrand();

        Task<bool> BrandExists(int id);
        
        Task<Brand> GetBrandById(int BrandId);

        Task<bool> CreateBrand(Brand brand);

        Task<bool> UpdateBrand(Brand Brand);


        Task<bool> DeleteBrand(Brand Brand);



    }

}
