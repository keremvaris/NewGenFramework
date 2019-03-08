using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.MvcWebUi.Models.ViewModels
{
  [Table("MyNews")]
  [DisplayColumn("NewsName")]
  [DisplayName("Haberler")]
  public class MyNewViewModel
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "NewsId Id", AutoGenerateField = false)]
    public virtual int NewsId { get; set; }
    [Display(Name = "Başlık"), Required()]
    public virtual string NewsName { get; set; }
    [ScaffoldColumn(false)]
    [Display(Name = "Yükle")]
    [DataType(DataType.Upload)]
    public virtual byte[] NewsImage { get; set; }
    [ScaffoldColumn(false)]
    [Display(Name = "Dosya Uzantısı")]
    [MaxLength(5)]
    public virtual string NewsImageExt { get; set; }
    [Display(Name = "Detay"), Required()]
    public virtual string Description { get; set; }
    [Display(Name = "Haber Tipi"), Required()]
    public virtual NewsType NewsType { get; set; }
    [Display(Name = "Aktif mi?"), Required()]
    public virtual bool IsActive { get; set; }

  }
}