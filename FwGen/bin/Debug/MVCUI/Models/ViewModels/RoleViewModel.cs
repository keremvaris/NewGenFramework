using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("Roles")]
[DisplayColumn("RoleName")]
[DisplayName("Role")]
public class RoleViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "RoleId Id", AutoGenerateField = false)]
public virtual int RoleId{ get; set; }
[Display(Name = "RoleName"), Required()]
public virtual string RoleName{ get; set; }

     }
}