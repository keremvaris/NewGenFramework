using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BayiPuan.MvcWebUi.Models.ViewModels
{
  [Table("Users")]
  [DisplayColumn("UserName")]
  [DisplayName("Kullanıcılar")]
  public class UserViewModel
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "UserId Id", AutoGenerateField = false)]
    public virtual int UserId { get; set; }
    [Display(Name = "Alt Bayi"), Required()]
    public virtual int UserTypeId { get; set; }
    [ForeignKey("UserTypeId")]
    public virtual UserTypeViewModel UserType { get; set; }
    [Display(Name = "Kullanıcı Adı"), Required()]
    public virtual string UserName { get; set; }
    [Display(Name = "Ad"), Required()]
    public virtual string FirstName { get; set; }
    [Display(Name = "Soyad"), Required()]
    public virtual string LastName { get; set; }
    [Display(Name = "E-Posta"), Required()]
    public virtual string Email { get; set; }
    [Display(Name = "Cep Telefonu"), Required()]
    public virtual string MobilePhone { get; set; }
   
    [Display(Name = "Doğum Tarihi"), Required()]
    public virtual DateTime BirthDate { get; set; }
    [Display(Name = "Durum"), Required()]
    public virtual bool State { get; set; }
    [Display(Name = "Anlaşma")]
    public virtual bool Agreement { get; set; }
    [Display(Name = "Bayi"), Required()]
    public virtual int SellerId { get; set; }
    [ForeignKey("SellerId")]
    public virtual SellerViewModel Seller { get; set; }

    [Display(Name = "İletişim")]
    public virtual bool Contact { get; set; }

  }
}