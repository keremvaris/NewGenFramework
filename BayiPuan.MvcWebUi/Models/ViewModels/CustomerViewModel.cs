using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BayiPuan.MvcWebUi.Models.ViewModels
{
  [Table("Customers")]
  [DisplayColumn("CustomerName")]
  [DisplayName("Müşteri")]
  public class CustomerViewModel
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "CustomerId Id", AutoGenerateField = false)]
    public virtual int CustomerId { get; set; }
    [Display(Name = "Firma Ünvanı"), Required()]
    public virtual string CustomerName { get; set; }
    [Display(Name = "Vergi No"), Required()]
    public virtual string TaxNo { get; set; }
    [Display(Name = "Vergi Dairesi"), Required()]
    public virtual string TaxAdministration { get; set; }
    [Display(Name = "Yetkili Kişi Adı"), Required()]
    public virtual string RelationalPersonName { get; set; }
    [Display(Name = "Yetkili Kişi Soyadı"), Required()]
    public virtual string RelationalPersonSurname { get; set; }
    [Display(Name = "Yetkili Kişi Telefon"), Required()]
    public virtual string MobilePhone { get; set; }

    //[Display(Name = "DateAdded"), Required()]
    //public virtual DateTime DateAdded { get; set; }
    //[Display(Name = "Aktif mi?"), Required()]
    //public virtual bool State { get; set; }

  }
}