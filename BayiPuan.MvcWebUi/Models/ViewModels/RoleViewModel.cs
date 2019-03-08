using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BayiPuan.MvcWebUi.Models.ViewModels
{               
[Table("Roles")]
[DisplayColumn("RoleName")]
[DisplayName("Rol")]
public class RoleViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "RoleId Id", AutoGenerateField = false)]
public virtual int RoleId{ get; set; }
[Display(Name = "RoleName"), Required()]
public virtual string RoleName{ get; set; }

     }
}