using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.MvcWebUi.Models.ViewModels
{
  [Table("Scores")]
  [DisplayColumn("ScoreTotal")]
  [DisplayName("Score")]
  public class ScoreViewModel
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "ScoreId Id", AutoGenerateField = false)]
    public virtual int ScoreId { get; set; }
    [Display(Name = "Kazanılan Puan"), Required()]
    public virtual decimal ScoreTotal { get; set; }
    [Display(Name = "Puan Tipi"), Required()]
    public virtual ScoreType ScoreType { get; set; }
    [Display(Name = "Kullanıcı"), Required()]
    public virtual int UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual UserViewModel User { get; set; }

  }
}