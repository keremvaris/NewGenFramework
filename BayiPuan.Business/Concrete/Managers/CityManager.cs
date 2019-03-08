
using System.Collections.Generic;
using System.Linq;
using BayiPuan.Business.Abstract;
using NewGenFramework.Core.Aspects.Postsharp.CacheAspects;
using NewGenFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using BayiPuan.DataAccess.Abstract;
using BayiPuan.Entities.Concrete;
using NewGenFramework.Core.CrossCuttingConcerns.Transaction;

namespace BayiPuan.Business.Concrete.Managers
{
    public class CityManager : ManagerBase, ICityService
    {
        private readonly ICityDal _cityDal;

        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<City> GetAll()
        {
            return _cityDal.GetList();
        }

        public City GetById(int cityId)
        {
            return _cityDal.Get(u => u.CityId == cityId);
        }      

        //[FluentValidationAspect(typeof(CityValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public City Add(City city)
        {
            return _cityDal.Add(city);
        }
        //[FluentValidationAspect(typeof(CityValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(City city)
        {
              _cityDal.Update(city);
        }

        public void Delete(City city)
        {
            _cityDal.Delete(city);
        }    

        public List<City> GetByCity(int cityId)
        {
            return _cityDal.GetList(filter: t => t.CityId == cityId).ToList();
        }
    }
}
