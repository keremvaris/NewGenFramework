using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("Sellers")]
[DisplayColumn("SellerName")]
[DisplayName("Seller")]
public class SellerViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "SellerId Id", AutoGenerateField = false)]
public virtual int SellerId{ get; set; }
[Display(Name = "UserType"), Required()]
public virtual usertype UserType{ get; set; }
[Display(Name = "UserTypeId"), Required()]
public virtual int UserTypeId{ get; set; }
[ForeignKey("UserTypeId")]
public virtual UserTypeViewModel UserType{ get; set; }
[Display(Name = "SellerName"), Required()]
public virtual string SellerName{ get; set; }
[Display(Name = "City"), Required()]
public virtual city City{ get; set; }
[Display(Name = "CityId"), Required()]
public virtual int CityId{ get; set; }
[ForeignKey("CityId")]
public virtual CityViewModel City{ get; set; }
[Display(Name = "SellerCode"), Required()]
public virtual string SellerCode{ get; set; }

     }
}