using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BayiPuan.MvcWebUi.Models.ViewModels
{
  [Table("Products")]
  [DisplayColumn("ProductName")]
  [DisplayName("Ürünler")]
  public class ProductViewModel
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "Ürün No", AutoGenerateField = false)]
    public virtual int ProductId { get; set; }
    [Display(Name = "Ürün Kodu"), Required()]
    public virtual string ProductCode { get; set; }
    [Display(Name = "Ürün Kısa Kodu"), Required()]
    public virtual string ProductShortCode { get; set; }
    [Display(Name = "Ürün Adı"), Required()]
    public virtual string ProductName { get; set; }
    
    [Display(Name = "Birim"), Required()]
    public virtual int UnitTypeId { get; set; }
    [ForeignKey("UnitTypeId")]
    public virtual UnitTypeViewModel UnitType { get; set; }
    [Display(Name = "Stok Miktarı"), Required()]
    public virtual int StockAmount { get; set; }
    [Display(Name = "Kalan Stok Miktarı"), Required()]
    public virtual int RemainingStockAmount { get; set; }
    [Display(Name = "Kritik Stok Seviyesi"), Required()]
    public virtual int CriticalStockAmount { get; set; }
    [Display(Name = "Puan"), Required()]
    public virtual decimal Point { get; set; }
    [Display(Name = "Puanın TL Karşılığı"), Required()]
    public virtual decimal PointToMoney { get; set; }

  }
}