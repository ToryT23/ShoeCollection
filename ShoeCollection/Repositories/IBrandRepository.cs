using ShoeCollection.Models;
using System.Collections.Generic;

namespace ShoeCollection.Repositories
{
    public interface IBrandRepository
    {
        void AddABrand(Brand brand);
        void DeleteABrand(int brandId);
        List<Brand> GetAllBrands();
        Brand GetBrandById(int id);
        void UpdateBrand(Brand brand);
    }
}