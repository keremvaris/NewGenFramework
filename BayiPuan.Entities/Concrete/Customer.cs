using System;
using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
  public class Customer : IEntity
  {
    public virtual int CustomerId { get; set; }
    public virtual string CustomerName { get; set; }
    public virtual string TaxNo { get; set; }
    public virtual string TaxAdministration { get; set; }
    public virtual int AddingUserId { get; set; }
    public virtual DateTime DateAdded { get; set; }
    public virtual bool State { get; set; }
    public virtual string RelationalPersonName { get; set; }
    public virtual string RelationalPersonSurname { get; set; }
    public virtual string MobilePhone { get; set; }
  }
}
