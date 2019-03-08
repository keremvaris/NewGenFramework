using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("MyNews")]
[DisplayColumn("MyNewName")]
[DisplayName("MyNew")]
public class MyNewViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "NewsId Id", AutoGenerateField = false)]
public virtual int NewsId{ get; set; }
[Display(Name = "NewsName"), Required()]
public virtual string NewsName{ get; set; }
[ScaffoldColumn(false)]
[Display(Name = "Yükle")]
[DataType(DataType.Upload)]
public virtual byte[] NewsImage{ get; set; }
[ScaffoldColumn(false)]
[Display(Name = "Dosya Uzantısı")]
[MaxLength(5)]
public virtual string NewsImageExt{ get; set; }
[Display(Name = "Description"), Required()]
public virtual string Description{ get; set; }
[Display(Name = "NewsType"), Required()]
public virtual newstype NewsType{ get; set; }
[Display(Name = "IsActive"), Required()]
public virtual boolean IsActive{ get; set; }

     }
}