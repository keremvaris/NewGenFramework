using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BayiPuan.Entities.Concrete;
using BayiPuan.MvcWebUi.Models.ViewModels;

namespace BayiPuan.MvcWebUi.Models.ViewModels
{
  [Table("Buys")]
  [DisplayColumn("BuyId")]
  [DisplayName("HarcananPuanlar")]
  public class BuyViewModel
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "BuyId Id", AutoGenerateField = false)]
    public virtual int BuyId { get; set; }
    [Display(Name = "Hediye"), Required()]
    public virtual int GiftId { get; set; }
    [ForeignKey("GiftId")]
    public virtual GiftViewModel Gift { get; set; }

    [Display(Name = "Kullanıcı"), Required()]
    public virtual int UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual UserViewModel User { get; set; }
    [Display(Name = "Hediye Talep Tarihi"), Required()]
    public virtual DateTime BuyDate { get; set; }
    [Display(Name = "Talep Edilen Miktar"), Required()]
    public virtual int BuyAmount { get; set; }
    [Display(Name = "Onaylandı mı?"), Required()]
    public virtual bool IsApproved { get; set; }
   
    [Display(Name = "Hediye Durumu"), Required()]
    public virtual BuyState BuyState { get; set; }

  }
}