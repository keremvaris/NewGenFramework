using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("Buyies")]
[DisplayColumn("BuyName")]
[DisplayName("Buy")]
public class BuyViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "BuyId Id", AutoGenerateField = false)]
public virtual int BuyId{ get; set; }
[Display(Name = "Gift"), Required()]
public virtual gift Gift{ get; set; }
[Display(Name = "GiftId"), Required()]
public virtual int GiftId{ get; set; }
[ForeignKey("GiftId")]
public virtual GiftViewModel Gift{ get; set; }
[Display(Name = "User"), Required()]
public virtual user User{ get; set; }
[Display(Name = "UserId"), Required()]
public virtual int UserId{ get; set; }
[ForeignKey("UserId")]
public virtual UserViewModel User{ get; set; }
[Display(Name = "BuyDate"), Required()]
public virtual nullable`1 BuyDate{ get; set; }
[Display(Name = "BuyAmount"), Required()]
public virtual nullable`1 BuyAmount{ get; set; }
[Display(Name = "IsApproved"), Required()]
public virtual boolean IsApproved{ get; set; }
[Display(Name = "ApprovedDate"), Required()]
public virtual nullable`1 ApprovedDate{ get; set; }
[Display(Name = "NotApproved"), Required()]
public virtual nullable`1 NotApproved{ get; set; }
[Display(Name = "NotApprovedDate"), Required()]
public virtual nullable`1 NotApprovedDate{ get; set; }
[Display(Name = "Reason"), Required()]
public virtual string Reason{ get; set; }
[Display(Name = "EditUserId"), Required()]
public virtual nullable`1 EditUserId{ get; set; }
[ForeignKey("EditUserId")]
public virtual EditUserViewModel EditUser{ get; set; }
[Display(Name = "BuyState"), Required()]
public virtual buystate BuyState{ get; set; }
[Display(Name = "Brand"), Required()]
public virtual brand Brand{ get; set; }
[Display(Name = "BrandId"), Required()]
public virtual int BrandId{ get; set; }
[ForeignKey("BrandId")]
public virtual BrandViewModel Brand{ get; set; }

     }
}