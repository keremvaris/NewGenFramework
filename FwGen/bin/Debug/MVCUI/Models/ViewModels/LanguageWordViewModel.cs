using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("LanguageWords")]
[DisplayColumn("LanguageWordName")]
[DisplayName("LanguageWord")]
public class LanguageWordViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "Id Id", AutoGenerateField = false)]
public virtual int Id{ get; set; }
[Display(Name = "LanguageId"), Required()]
public virtual int LanguageId{ get; set; }
[ForeignKey("LanguageId")]
public virtual LanguageViewModel Language{ get; set; }
[Display(Name = "Code"), Required()]
public virtual string Code{ get; set; }
[Display(Name = "Value"), Required()]
public virtual string Value{ get; set; }

     }
}