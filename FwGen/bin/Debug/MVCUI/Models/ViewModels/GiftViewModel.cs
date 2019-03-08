using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("Gifts")]
[DisplayColumn("GiftName")]
[DisplayName("Gift")]
public class GiftViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "GiftId Id", AutoGenerateField = false)]
public virtual int GiftId{ get; set; }
[Display(Name = "Category"), Required()]
public virtual category Category{ get; set; }
[Display(Name = "CategoryId"), Required()]
public virtual int CategoryId{ get; set; }
[ForeignKey("CategoryId")]
public virtual CategoryViewModel Category{ get; set; }
[Display(Name = "Brand"), Required()]
public virtual brand Brand{ get; set; }
[Display(Name = "BrandId"), Required()]
public virtual int BrandId{ get; set; }
[ForeignKey("BrandId")]
public virtual BrandViewModel Brand{ get; set; }
[Display(Name = "Usage"), Required()]
public virtual string Usage{ get; set; }
[Display(Name = "Coverage"), Required()]
public virtual string Coverage{ get; set; }
[Display(Name = "ValidityPeriod"), Required()]
public virtual string ValidityPeriod{ get; set; }
[Display(Name = "Indivisible"), Required()]
public virtual string Indivisible{ get; set; }
[Display(Name = "Combining"), Required()]
public virtual string Combining{ get; set; }
[Display(Name = "TermOfUse"), Required()]
public virtual string TermOfUse{ get; set; }
[Display(Name = "Cancellation"), Required()]
public virtual string Cancellation{ get; set; }
[Display(Name = "Description"), Required()]
public virtual string Description{ get; set; }
[Display(Name = "GiftPoint"), Required()]
public virtual int GiftPoint{ get; set; }
[Display(Name = "IsActive"), Required()]
public virtual boolean IsActive{ get; set; }
[Display(Name = "Detail"), Required()]
public virtual string Detail{ get; set; }

     }
}