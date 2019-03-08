using System;
using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
  public class User : IEntity
  {
    public int UserId { get; set; }
    public UserType UserType { get; set; }
    public int UserTypeId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string MobilePhone { get; set; }
    public string UserImage { get; set; }
    public DateTime BirthDate { get; set; }
    public bool State { get; set; }
    public bool Agreement { get; set; }
    public Seller Seller { get; set; }
    public int SellerId { get; set; }
    public bool Contact { get; set; }
  }
}
