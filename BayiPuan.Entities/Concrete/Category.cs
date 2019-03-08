using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
  public class Category : IEntity
  {
    public virtual int CategoryId { get; set; }
    public virtual string CategoryName { get; set; }
    public virtual int? TopCategoryId { get; set; }
  }
}
