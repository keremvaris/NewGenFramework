

using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
  public class Seller : IEntity
  {
    public virtual int SellerId { get; set; }
    public UserType UserType { get; set; }
    public virtual int UserTypeId { get; set; }
    public virtual string SellerName { get; set; }
    public City City { get; set; }
    public virtual int CityId { get; set; }
    public virtual string SellerCode { get; set; }
  }
}
