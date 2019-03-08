using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
  public class MyProduct:IEntity
  {
    public int MyProductId { get; set; }
    public string ProductName { get; set; }
    public byte[] MyProductImage { get; set; }
    public string MyProductImageExt { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
  }

}
