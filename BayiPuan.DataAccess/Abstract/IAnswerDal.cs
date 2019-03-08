
using NewGenFramework.Core.DataAccess;
using BayiPuan.Entities.Concrete;
namespace BayiPuan.DataAccess.Abstract
{
  //Neden IEntityRepository direk kullanmıyoruz da IProductDal diye bir şeyi araya atıyoruz sorusuna 
  //Nesneye özgü metodlar geliştirilebilir.
  //for Ex:
  //List<AnswersDetail> GetAnswersDetails();
  public interface IAnswerDal : IEntityRepository<Answer>
  {

  }
}