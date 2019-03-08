using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BayiPuan.MvcWebUi.Models.ViewModels
{
  [Table("Sellers")]
  [DisplayColumn("SellerName")]
  [DisplayName("Bayiler")]
  public class SellerViewModel
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "SellerId Id", AutoGenerateField = false)]
    public virtual int SellerId { get; set; }

    [Display(Name = "Bayi Tipi"), Required()]
    public virtual int UserTypeId { get; set; }
    [ForeignKey("UserTypeId")]
    public virtual UserTypeViewModel UserType { get; set; }


    [Display(Name = "Bayi Adı"), Required()]
    public virtual string SellerName { get; set; }


    [Display(Name = "Şehir"), Required()]
    public virtual int CityId { get; set; }
    [ForeignKey("CityId")]
    public virtual CityViewModel City { get; set; }
    [Display(Name = "Bayi Kodu"), Required()]
    public virtual string SellerCode { get; set; }

  }
}