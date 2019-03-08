using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BayiPuan.MvcWebUi.Models.ViewModels
{
  [Table("Cities")]
  [DisplayColumn("CityName")]
  [DisplayName("City")]
  public class CityViewModel
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "CityId Id", AutoGenerateField = false)]
    public virtual int CityId { get; set; }
    [Display(Name = "Şehir Adı"), Required()]
    public virtual string CityName { get; set; }

  }
}