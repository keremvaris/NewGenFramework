using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("GN_Reports")]
[DisplayColumn("GN_ReportName")]
[DisplayName("GN_Report")]
public class GN_ReportViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "ReportId Id", AutoGenerateField = false)]
public virtual int ReportId{ get; set; }
[Display(Name = "ReportTitle"), Required()]
public virtual string ReportTitle{ get; set; }
[Display(Name = "ReportSql"), Required()]
public virtual string ReportSql{ get; set; }
[Display(Name = "ReportFilter"), Required()]
public virtual string ReportFilter{ get; set; }
[Display(Name = "ReportAuthorization"), Required()]
public virtual string ReportAuthorization{ get; set; }

     }
}