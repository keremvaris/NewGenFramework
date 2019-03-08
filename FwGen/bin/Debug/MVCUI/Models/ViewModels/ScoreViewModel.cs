using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("Scores")]
[DisplayColumn("ScoreName")]
[DisplayName("Score")]
public class ScoreViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "ScoreId Id", AutoGenerateField = false)]
public virtual int ScoreId{ get; set; }
[Display(Name = "ScoreTotal"), Required()]
public virtual decimal ScoreTotal{ get; set; }
[Display(Name = "ScoreType"), Required()]
public virtual scoretype ScoreType{ get; set; }
[Display(Name = "UserId"), Required()]
public virtual int UserId{ get; set; }
[ForeignKey("UserId")]
public virtual UserViewModel User{ get; set; }
[Display(Name = "User"), Required()]
public virtual user User{ get; set; }
[Display(Name = "ScoreDate"), Required()]
public virtual datetime ScoreDate{ get; set; }

     }
}