using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
  public class MyNew : IEntity
  {
    public int NewsId { get; set; }
    public string NewsName { get; set; }
    public byte[] NewsImage { get; set; }
    public string NewsImageExt { get; set; }
    public string Description { get; set; }
    public NewsType NewsType { get; set; }
    public bool IsActive { get; set; }
  }
}
