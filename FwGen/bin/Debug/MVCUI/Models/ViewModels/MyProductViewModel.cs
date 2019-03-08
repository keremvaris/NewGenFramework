using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("MyProducts")]
[DisplayColumn("MyProductName")]
[DisplayName("MyProduct")]
public class MyProductViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "MyProductId Id", AutoGenerateField = false)]
public virtual int MyProductId{ get; set; }
[Display(Name = "ProductName"), Required()]
public virtual string ProductName{ get; set; }
[ScaffoldColumn(false)]
[Display(Name = "Yükle")]
[DataType(DataType.Upload)]
public virtual byte[] MyProductImage{ get; set; }
[ScaffoldColumn(false)]
[Display(Name = "Dosya Uzantısı")]
[MaxLength(5)]
public virtual string MyProductImageExt{ get; set; }
[Display(Name = "Description"), Required()]
public virtual string Description{ get; set; }
[Display(Name = "IsActive"), Required()]
public virtual boolean IsActive{ get; set; }

     }
}