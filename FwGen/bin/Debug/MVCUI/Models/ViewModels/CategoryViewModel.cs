using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("Categoryies")]
[DisplayColumn("CategoryName")]
[DisplayName("Category")]
public class CategoryViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "CategoryId Id", AutoGenerateField = false)]
public virtual int CategoryId{ get; set; }
[Display(Name = "CategoryName"), Required()]
public virtual string CategoryName{ get; set; }
[Display(Name = "TopCategoryId"), Required()]
public virtual nullable`1 TopCategoryId{ get; set; }
[ForeignKey("TopCategoryId")]
public virtual TopCategoryViewModel TopCategory{ get; set; }

     }
}