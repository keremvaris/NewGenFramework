using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
  public class Score:IEntity
  {
    public int ScoreId { get; set; }
    public decimal ScoreTotal { get; set; }
    public ScoreType ScoreType { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime ScoreDate { get; set; }
  }
}
