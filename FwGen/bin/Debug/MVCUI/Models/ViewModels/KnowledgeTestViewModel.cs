using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("KnowledgeTests")]
[DisplayColumn("KnowledgeTestName")]
[DisplayName("KnowledgeTest")]
public class KnowledgeTestViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "KnowledgeTestId Id", AutoGenerateField = false)]
public virtual int KnowledgeTestId{ get; set; }
[Display(Name = "Question"), Required()]
public virtual string Question{ get; set; }
[Display(Name = "Answer1"), Required()]
public virtual string Answer1{ get; set; }
[Display(Name = "Answer2"), Required()]
public virtual string Answer2{ get; set; }
[Display(Name = "Answer3"), Required()]
public virtual string Answer3{ get; set; }
[Display(Name = "Answer4"), Required()]
public virtual string Answer4{ get; set; }
[Display(Name = "ValidAnswerType"), Required()]
public virtual string ValidAnswerType{ get; set; }
[Display(Name = "Point"), Required()]
public virtual decimal Point{ get; set; }
[Display(Name = "KnowledgeDate"), Required()]
public virtual datetime KnowledgeDate{ get; set; }
[Display(Name = "IsActive"), Required()]
public virtual nullable`1 IsActive{ get; set; }

     }
}