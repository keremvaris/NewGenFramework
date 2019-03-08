using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
  public class KnowledgeTest:IEntity
  {
    public int KnowledgeTestId { get; set; }
    public string Question { get; set; }
    public string Answer1 { get; set; }
    public string Answer2 { get; set; }
    public string Answer3 { get; set; }
    public string Answer4 { get; set; }
    public string ValidAnswerType { get; set; }
    public decimal Point { get; set; }
    public DateTime KnowledgeDate { get; set; }
    public bool? IsActive { get; set; }
  }
}
