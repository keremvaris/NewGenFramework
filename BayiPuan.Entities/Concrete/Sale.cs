using System;
using NewGenFramework.Core.Entities;

namespace BayiPuan.Entities.Concrete
{
  public class Sale : IEntity
  {
    public virtual int SaleId { get; set; }
    public  Customer Customer { get; set; }
    public  int CustomerId { get; set; }
    public  string InvoiceNo { get; set; }
    public  byte[] InvoiceImage { get; set; }
    public  string InvoiceImageExt { get; set; }
    public  Product Product { get; set; }
    public  int ProductId { get; set; }
    public  int AmountOfSales { get; set; }
    public  User User { get; set; }
    public int UserId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime AddDate { get; set; }
    public bool IsApproved { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public bool? NotApproved { get; set; }
    public DateTime? NotApprovedDate { get; set; }
    public string Reason { get; set; }
    public decimal InvoiceTotal { get; set; }
  }
}
