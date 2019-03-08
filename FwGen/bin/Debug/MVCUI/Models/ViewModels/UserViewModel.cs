using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("Users")]
[DisplayColumn("UserName")]
[DisplayName("User")]
public class UserViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "UserId Id", AutoGenerateField = false)]
public virtual int UserId{ get; set; }
[Display(Name = "UserType"), Required()]
public virtual usertype UserType{ get; set; }
[Display(Name = "UserTypeId"), Required()]
public virtual int UserTypeId{ get; set; }
[ForeignKey("UserTypeId")]
public virtual UserTypeViewModel UserType{ get; set; }
[Display(Name = "UserName"), Required()]
public virtual string UserName{ get; set; }
[Display(Name = "Password"), Required()]
public virtual string Password{ get; set; }
[Display(Name = "FirstName"), Required()]
public virtual string FirstName{ get; set; }
[Display(Name = "LastName"), Required()]
public virtual string LastName{ get; set; }
[Display(Name = "Email"), Required()]
public virtual string Email{ get; set; }
[Display(Name = "MobilePhone"), Required()]
public virtual string MobilePhone{ get; set; }
[Display(Name = "UserImage"), Required()]
public virtual string UserImage{ get; set; }
[Display(Name = "BirthDate"), Required()]
public virtual datetime BirthDate{ get; set; }
[Display(Name = "State"), Required()]
public virtual boolean State{ get; set; }
[Display(Name = "Agreement"), Required()]
public virtual boolean Agreement{ get; set; }
[Display(Name = "Seller"), Required()]
public virtual seller Seller{ get; set; }
[Display(Name = "SellerId"), Required()]
public virtual int SellerId{ get; set; }
[ForeignKey("SellerId")]
public virtual SellerViewModel Seller{ get; set; }
[Display(Name = "Contact"), Required()]
public virtual boolean Contact{ get; set; }

     }
}