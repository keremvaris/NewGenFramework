using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
  public class Answer:IEntity
  {
    public int AnswerId { get; set; }
    public KnowledgeTest KnowledgeTest { get; set; }
    public int KnowledgeTestId { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public DateTime AnswerDate { get; set; }
    public bool ValidAnswer { get; set; }
  }
}
