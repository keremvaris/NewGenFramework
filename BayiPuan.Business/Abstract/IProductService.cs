
using System.Collections.Generic;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product GetById(int productId);
        List<Product> GetByProduct(int productId);
        
        Product Add(Product product);
        void Update(Product product);
        void Delete(Product product);

    }
}