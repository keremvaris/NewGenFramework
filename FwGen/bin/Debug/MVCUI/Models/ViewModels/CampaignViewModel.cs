using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("Campaigns")]
[DisplayColumn("CampaignName")]
[DisplayName("Campaign")]
public class CampaignViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "CampaignId Id", AutoGenerateField = false)]
public virtual int CampaignId{ get; set; }
[Display(Name = "CampaignName"), Required()]
public virtual string CampaignName{ get; set; }
[ScaffoldColumn(false)]
[Display(Name = "Yükle")]
[DataType(DataType.Upload)]
public virtual byte[] CampaignImage{ get; set; }
[ScaffoldColumn(false)]
[Display(Name = "Dosya Uzantısı")]
[MaxLength(5)]
public virtual string CampaignImageExt{ get; set; }

     }
}