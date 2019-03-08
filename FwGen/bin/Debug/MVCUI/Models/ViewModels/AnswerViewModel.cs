using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("Answers")]
[DisplayColumn("AnswerName")]
[DisplayName("Answer")]
public class AnswerViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "AnswerId Id", AutoGenerateField = false)]
public virtual int AnswerId{ get; set; }
[Display(Name = "KnowledgeTest"), Required()]
public virtual knowledgetest KnowledgeTest{ get; set; }
[Display(Name = "KnowledgeTestId"), Required()]
public virtual int KnowledgeTestId{ get; set; }
[ForeignKey("KnowledgeTestId")]
public virtual KnowledgeTestViewModel KnowledgeTest{ get; set; }
[Display(Name = "User"), Required()]
public virtual user User{ get; set; }
[Display(Name = "UserId"), Required()]
public virtual int UserId{ get; set; }
[ForeignKey("UserId")]
public virtual UserViewModel User{ get; set; }
[Display(Name = "AnswerDate"), Required()]
public virtual datetime AnswerDate{ get; set; }
[Display(Name = "ValidAnswer"), Required()]
public virtual boolean ValidAnswer{ get; set; }

     }
}