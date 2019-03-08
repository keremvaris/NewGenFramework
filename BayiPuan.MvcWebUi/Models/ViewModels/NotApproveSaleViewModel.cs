using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BayiPuan.MvcWebUi.Models.ViewModels
{
  [Table("Sales")]
  [DisplayColumn("SaleName")]
  [DisplayName("Satışı Reddet")]
  public class NotApproveSaleViewModel
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "SaleId Id", AutoGenerateField = false)]
    public virtual int SaleId { get; set; }
    [Display(Name = "Müşteri"), Required()]
    public virtual int CustomerId { get; set; }
    [ForeignKey("CustomerId")]
    public virtual CustomerViewModel Customer { get; set; }
    [Display(Name = "Fatura No"), Required()]
    public virtual string InvoiceNo { get; set; }
    [Display(Name = "Fatura Tutarı"), Required()]
    public virtual decimal InvoiceTotal { get; set; }

    [ScaffoldColumn(false)]
    [Display(Name = "Fatura Görseli"), Required()]
    [DataType(DataType.Upload)]
    public virtual byte[] InvoiceImage { get; set; }
    [ScaffoldColumn(false)]
    [Display(Name = "Dosya Uzantısı Otomatik Çözümlenecek, Boş Bırakınız!")]
    [MaxLength(5)]
    public virtual string InvoiceImageExt { get; set; }
    [Display(Name = "Ürün"), Required()]
    public virtual int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public virtual ProductViewModel Product { get; set; }
    [Display(Name = "Satış Miktarı"), Required()]
    public virtual int AmountOfSales { get; set; }
    [Display(Name = "Satıcı"), Required()]
    public virtual int UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual UserViewModel User { get; set; }
    [Display(Name = "Fatura Tarihi"), Required()]
    public virtual DateTime InvoiceDate { get; set; }
    [Display(Name = "Satışı Reddet"), Required()]
    public bool NotApproved { get; set; }
    [Display(Name = "Onaylanmama Gerekçesi"),Required()]
    [MaxLength(255)]
    public virtual string Reason { get; set; }

  }
}