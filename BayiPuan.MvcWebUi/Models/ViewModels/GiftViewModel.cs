using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;


namespace BayiPuan.MvcWebUi.Models.ViewModels
{
  [Table("Gifts")]
  [DisplayColumn("Description")]
  [DisplayName("Hediyeler")]
  public class GiftViewModel
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "GiftId", AutoGenerateField = false)]
    public virtual int GiftId { get; set; }
    [Display(Name = "Kategori"), Required()]
    public virtual int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public virtual CategoryViewModel Category { get; set; }
    [Display(Name = "Marka"), Required()]
    public virtual int BrandId { get; set; }
    [ForeignKey("BrandId")]
    public virtual BrandViewModel Brand { get; set; }
    [Display(Name = "Kullanım Şekli"), Required()]
    public virtual string Usage { get; set; } = "Sadece XXXX mağazalarında (Outletler Hariç) geçerlidir.";
    [Display(Name = "Kapsamı"), Required()]
    public virtual string Coverage { get; set; } = "Tüm ürünlerde geçerlidir.";
    [Display(Name = "Geçerlilik Süresi"), Required()]
    public virtual string ValidityPeriod { get; set; } = "Satın aldığınız tarihten itibaren 3 ay içinde kullanabilirsiniz.";
    [Display(Name = "Bölünebilir mi?"), Required()]
    public virtual string Indivisible { get; set; } = "Tek seferde kullanılır. Farklı alışverişlerde kullanılamaz.";
    [Display(Name = "Birleştirilebilir mi?"), Required()]
    public virtual string Combining { get; set; } = "Hediye Çekleri Birleştirilemez.";
    [Display(Name = "Kullanım Şartı"), Required()]
    public virtual string TermOfUse { get; set; } = "Toplam alışveriş tutarı Hediye Çeki değerinin üzerinde olmalıdır.";
    [Display(Name = "İptal Edilebilir mi?"), Required()]
    public virtual string Cancellation { get; set; } = "Hediye Çeki alındıktan sonra İPTAL edilemez.";
    [Display(Name = "Tanımı"), Required()]
    public virtual string Description { get; set; }
    [Display(Name = "Puan"), Required()]
    public virtual int GiftPoint { get; set; }
    [Display(Name = "Detaylı Açıklama")]
    [DataType(DataType.Html)]
    [AllowHtml]
    public virtual string Detail { get; set; }
    [Display(Name = "Aktif mi?"), Required()]
    public virtual bool IsActive { get; set; }
    

  }
}