using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayiPuan.Entities.ComplexTypes
{
  public class SalesRanking
  {
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int SaleOrder { get; set; }
    public decimal SumSale { get; set; }
  }
}
