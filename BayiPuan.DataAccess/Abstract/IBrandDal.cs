
using NewGenFramework.Core.DataAccess;
using BayiPuan.Entities.Concrete;


namespace BayiPuan.DataAccess.Abstract
{
  //Neden IEntityRepository direk kullanmıyoruz da IProductDal diye bir şeyi araya atıyoruz sorusuna 
  //Nesneye özgü metodlar geliştirilebilir.
  //for Ex:
  //List<BrandDetail> GetBrandDetails();
  public interface IBrandDal : IEntityRepository<Brand>
  {

  }
}