
using System.Collections.Generic;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.Business.Abstract
{
    public interface IUnitTypeService
    {
        List<UnitType> GetAll();
        UnitType GetById(int unitTypeId);
        List<UnitType> GetByUnitType(int unitTypeId);
        
        UnitType Add(UnitType unitType);
        void Update(UnitType unitType);
        void Delete(UnitType unitType);

    }
}