
using System.Collections.Generic;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.Business.Abstract
{
    public interface IMyNewService
    {
        List<MyNew> GetAll();
        MyNew GetById(int myNewId);
        List<MyNew> GetByMyNew(int myNewId);
        
        MyNew Add(MyNew myNew);
        void Update(MyNew myNew);
        void Delete(MyNew myNew);

    }
}