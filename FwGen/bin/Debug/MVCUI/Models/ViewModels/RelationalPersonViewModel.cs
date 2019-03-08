using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("RelationalPersons")]
[DisplayColumn("RelationalPersonName")]
[DisplayName("RelationalPerson")]
public class RelationalPersonViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "RelationalPersonId Id", AutoGenerateField = false)]
public virtual int RelationalPersonId{ get; set; }
[Display(Name = "RelationalPersonName"), Required()]
public virtual string RelationalPersonName{ get; set; }
[Display(Name = "RelationalPersonSurname"), Required()]
public virtual string RelationalPersonSurname{ get; set; }
[Display(Name = "MobilePhone"), Required()]
public virtual string MobilePhone{ get; set; }

     }
}