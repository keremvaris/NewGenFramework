using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
  public class Buy:IEntity
  {
    public int BuyId { get; set; }
    public Gift Gift { get; set; }
    public int GiftId { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }

    public DateTime? BuyDate { get; set; }

    public int? BuyAmount { get; set; }

    public bool IsApproved { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public bool? NotApproved { get; set; }

    public DateTime? NotApprovedDate { get; set; }

    public string Reason { get; set; }
    public int? EditUserId { get; set; }
    public BuyState BuyState { get; set; }
    public Brand Brand { get; set; }
    public int BrandId { get; set; }

  }
}
