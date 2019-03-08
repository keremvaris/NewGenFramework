using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BayiPuan.MvcWebUi.Models.ViewModels
{
  [Table("MyProducts")]
  [DisplayColumn("ProductName")]
  [DisplayName("Ürünlerimiz")]
  public class MyProductViewModel
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "MyProductId Id", AutoGenerateField = false)]
    public virtual int MyProductId { get; set; }
    [Display(Name = "Ürün Adı"), Required()]
    public virtual string ProductName { get; set; }
    [ScaffoldColumn(false)]
    [Display(Name = "Yükle")]
    [DataType(DataType.Upload)]
    public virtual byte[] MyProductImage { get; set; }
    [ScaffoldColumn(false)]
    [Display(Name = "Dosya Uzantısı")]
    [MaxLength(5)]
    public virtual string MyProductImageExt { get; set; }
    [Display(Name = "Ürün Detayı"), Required()]
    public virtual string Description { get; set; }
    [Display(Name = "Aktif mi?"), Required()]
    public virtual bool IsActive { get; set; }

  }
}