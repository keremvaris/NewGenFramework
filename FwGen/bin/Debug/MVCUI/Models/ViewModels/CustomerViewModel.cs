using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGenFramework.MvcWebUi.Models.ViewModels
{               
[Table("Customers")]
[DisplayColumn("CustomerName")]
[DisplayName("Customer")]
public class CustomerViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "CustomerId Id", AutoGenerateField = false)]
public virtual int CustomerId{ get; set; }
[Display(Name = "CustomerName"), Required()]
public virtual string CustomerName{ get; set; }
[Display(Name = "TaxNo"), Required()]
public virtual string TaxNo{ get; set; }
[Display(Name = "TaxAdministration"), Required()]
public virtual string TaxAdministration{ get; set; }
[Display(Name = "AddingUserId"), Required()]
public virtual int AddingUserId{ get; set; }
[ForeignKey("AddingUserId")]
public virtual AddingUserViewModel AddingUser{ get; set; }
[Display(Name = "DateAdded"), Required()]
public virtual datetime DateAdded{ get; set; }
[Display(Name = "State"), Required()]
public virtual boolean State{ get; set; }
[Display(Name = "RelationalPersonName"), Required()]
public virtual string RelationalPersonName{ get; set; }
[Display(Name = "RelationalPersonSurname"), Required()]
public virtual string RelationalPersonSurname{ get; set; }
[Display(Name = "MobilePhone"), Required()]
public virtual string MobilePhone{ get; set; }

     }
}