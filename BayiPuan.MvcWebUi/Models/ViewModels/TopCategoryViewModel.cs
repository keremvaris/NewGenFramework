using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BayiPuan.MvcWebUi.Models.ViewModels
{
  [Table("Categories")]
  [DisplayColumn("CategoryName")]
  [DisplayName("Top Kategori")]
  public class TopCategoryViewModel
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "CategoryId Id", AutoGenerateField = false)]
    public virtual int CategoryId { get; set; }
    [Display(Name = "CategoryName"), Required()]
    public virtual string CategoryName { get; set; }
  }
}