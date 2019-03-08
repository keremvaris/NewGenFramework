using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
  public class Gift:IEntity
  {
    public int GiftId { get; set; }
    public  Category Category { get; set; }
    public int CategoryId { get; set; }
    public  Brand Brand { get; set; }
    public int BrandId { get; set; }
    public string Usage { get; set; } 
    public string Coverage { get; set; } 
    public string ValidityPeriod { get; set; } 
    public string Indivisible { get; set; } 
    public string Combining { get; set; } 
    public string TermOfUse { get; set; } 
    public string Cancellation { get; set; } 
    public string Description { get; set; }
    public int GiftPoint { get; set; }
    public bool IsActive { get; set; }
    public string Detail { get; set; }
  }

}
