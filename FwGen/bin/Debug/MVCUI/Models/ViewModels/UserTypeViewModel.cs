using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("UserTypes")]
[DisplayColumn("UserTypeName")]
[DisplayName("UserType")]
public class UserTypeViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "UserTypeId Id", AutoGenerateField = false)]
public virtual int UserTypeId{ get; set; }
[Display(Name = "UserTypeName"), Required()]
public virtual string UserTypeName{ get; set; }

     }
}