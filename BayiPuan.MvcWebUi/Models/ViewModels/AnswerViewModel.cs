using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.MvcWebUi.Models.ViewModels
{               
[Table("Answers")]
[DisplayColumn("AnswerId")]
[DisplayName("Answer")]
public class AnswerViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "AnswerId Id", AutoGenerateField = false)]
public virtual int AnswerId{ get; set; }
[Display(Name = "Soru"), Required()]
public virtual int KnowledgeTestId{ get; set; }
[ForeignKey("KnowledgeTestId")]
public virtual KnowledgeTestViewModel KnowledgeTest{ get; set; }

[Display(Name = "Kullanıcı"), Required()]
public virtual int UserId{ get; set; }
[ForeignKey("UserId")]
public virtual UserViewModel User{ get; set; }
[Display(Name = "Cevap Tarihi"), Required()]
public virtual DateTime AnswerDate{ get; set; }
[Display(Name = "Cevap"), Required()]
public virtual bool ValidAnswer{ get; set; }

     }
}