
using System.Collections.Generic;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.Business.Abstract
{
    public interface IBrandService
    {
        List<Brand> GetAll();
        Brand GetById(int brandId);
        List<Brand> GetByBrand(int brandId);
        
        Brand Add(Brand brand);
        void Update(Brand brand);
        void Delete(Brand brand);

    }
}