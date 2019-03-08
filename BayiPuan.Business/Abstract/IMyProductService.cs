
using System.Collections.Generic;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.Business.Abstract
{
    public interface IMyProductService
    {
        List<MyProduct> GetAll();
        MyProduct GetById(int myProductId);
        List<MyProduct> GetByMyProduct(int myProductId);
        
        MyProduct Add(MyProduct myProduct);
        void Update(MyProduct myProduct);
        void Delete(MyProduct myProduct);

    }
}