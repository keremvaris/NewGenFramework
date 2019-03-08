
using System.Collections.Generic;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.Business.Abstract
{
    public interface ICityService
    {
        List<City> GetAll();
        City GetById(int cityId);
        List<City> GetByCity(int cityId);
        
        City Add(City city);
        void Update(City city);
        void Delete(City city);

    }
}