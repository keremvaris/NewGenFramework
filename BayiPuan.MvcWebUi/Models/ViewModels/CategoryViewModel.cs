using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BayiPuan.MvcWebUi.Models.ViewModels;

namespace BayiPuan.MvcWebUi.Models.ViewModels
{
  [Table("Categories")]
  [DisplayColumn("CategoryName")]
  [DisplayName("Kategori")]
  public class CategoryViewModel
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "CategoryId Id", AutoGenerateField = false)]
    public virtual int CategoryId { get; set; }
    [Display(Name = "CategoryName"), Required()]
    public virtual string CategoryName { get; set; }

    //[Display(Name = "TopCategoryId")]
    //public virtual int TopCategoryId { get; set; }
    //[ForeignKey("TopCategoryId")]
    //public virtual TopCategoryViewModel TopCategory { get; set; }

  }
}