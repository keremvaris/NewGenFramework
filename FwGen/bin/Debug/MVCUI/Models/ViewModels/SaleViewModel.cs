using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("Sales")]
[DisplayColumn("SaleName")]
[DisplayName("Sale")]
public class SaleViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "SaleId Id", AutoGenerateField = false)]
public virtual int SaleId{ get; set; }
[Display(Name = "Customer"), Required()]
public virtual customer Customer{ get; set; }
[Display(Name = "CustomerId"), Required()]
public virtual int CustomerId{ get; set; }
[ForeignKey("CustomerId")]
public virtual CustomerViewModel Customer{ get; set; }
[Display(Name = "InvoiceNo"), Required()]
public virtual string InvoiceNo{ get; set; }
[ScaffoldColumn(false)]
[Display(Name = "Yükle")]
[DataType(DataType.Upload)]
public virtual byte[] InvoiceImage{ get; set; }
[ScaffoldColumn(false)]
[Display(Name = "Dosya Uzantısı")]
[MaxLength(5)]
public virtual string InvoiceImageExt{ get; set; }
[Display(Name = "Product"), Required()]
public virtual product Product{ get; set; }
[Display(Name = "ProductId"), Required()]
public virtual int ProductId{ get; set; }
[ForeignKey("ProductId")]
public virtual ProductViewModel Product{ get; set; }
[Display(Name = "AmountOfSales"), Required()]
public virtual int AmountOfSales{ get; set; }
[Display(Name = "User"), Required()]
public virtual user User{ get; set; }
[Display(Name = "UserId"), Required()]
public virtual int UserId{ get; set; }
[ForeignKey("UserId")]
public virtual UserViewModel User{ get; set; }
[Display(Name = "InvoiceDate"), Required()]
public virtual datetime InvoiceDate{ get; set; }
[Display(Name = "AddDate"), Required()]
public virtual datetime AddDate{ get; set; }
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
[Display(Name = "InvoiceTotal"), Required()]
public virtual decimal InvoiceTotal{ get; set; }

     }
}