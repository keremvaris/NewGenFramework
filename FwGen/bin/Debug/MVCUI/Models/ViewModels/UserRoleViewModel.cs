using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("UserRoles")]
[DisplayColumn("UserRoleName")]
[DisplayName("UserRole")]
public class UserRoleViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "UserRoleId Id", AutoGenerateField = false)]
public virtual int UserRoleId{ get; set; }
[Display(Name = "RoleId"), Required()]
public virtual int RoleId{ get; set; }
[ForeignKey("RoleId")]
public virtual RoleViewModel Role{ get; set; }
[Display(Name = "Role"), Required()]
public virtual role Role{ get; set; }
[Display(Name = "User"), Required()]
public virtual user User{ get; set; }
[Display(Name = "UserId"), Required()]
public virtual int UserId{ get; set; }
[ForeignKey("UserId")]
public virtual UserViewModel User{ get; set; }

     }
}