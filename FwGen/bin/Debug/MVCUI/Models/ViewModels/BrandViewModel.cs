using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("Brands")]
[DisplayColumn("BrandName")]
[DisplayName("Brand")]
public class BrandViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "BrandId Id", AutoGenerateField = false)]
public virtual int BrandId{ get; set; }
[Display(Name = "BrandName"), Required()]
public virtual string BrandName{ get; set; }
[ScaffoldColumn(false)]
[Display(Name = "Yükle")]
[DataType(DataType.Upload)]
public virtual byte[] BrandImage{ get; set; }
[ScaffoldColumn(false)]
[Display(Name = "Dosya Uzantısı")]
[MaxLength(5)]
public virtual string BrandImageExt{ get; set; }

     }
}