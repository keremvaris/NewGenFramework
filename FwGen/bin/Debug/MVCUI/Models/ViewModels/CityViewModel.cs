using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("Cityies")]
[DisplayColumn("CityName")]
[DisplayName("City")]
public class CityViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "CityId Id", AutoGenerateField = false)]
public virtual int CityId{ get; set; }
[Display(Name = "CityName"), Required()]
public virtual string CityName{ get; set; }

     }
}