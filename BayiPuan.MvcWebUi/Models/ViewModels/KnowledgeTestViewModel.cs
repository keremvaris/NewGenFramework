using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.MvcWebUi.Models.ViewModels
{               
[Table("KnowledgeTests")]
[DisplayColumn("Question")]
[DisplayName("KnowledgeTest")]
public class KnowledgeTestViewModel
{
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
[Display(Name = "KnowledgeTestId Id", AutoGenerateField = false)]
public virtual int KnowledgeTestId{ get; set; }
[Display(Name = "Soru"), Required()]
public virtual string Question{ get; set; }
[Display(Name = "Cevap1"), Required()]
public virtual string Answer1{ get; set; }
[Display(Name = "Cevap2"), Required()]
public virtual string Answer2{ get; set; }
[Display(Name = "Cevap3"), Required()]
public virtual string Answer3{ get; set; }
[Display(Name = "Cevap4"), Required()]
public virtual string Answer4{ get; set; }
[Display(Name = "CevapTipi"), Required()]
public virtual string ValidAnswerType { get; set; }

[Display(Name = "Puan Karşılığı"), Required()]
public virtual decimal Point{ get; set; }
[Display(Name = "Tarih"), Required()]
public virtual DateTime KnowledgeDate{ get; set; }
[Display(Name = "Aktif mi?"), Required()]
public virtual bool IsActive{ get; set; }

     }
}