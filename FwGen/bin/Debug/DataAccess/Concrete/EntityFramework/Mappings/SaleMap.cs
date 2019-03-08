using System.Data.Entity.ModelConfiguration;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.DataAccess.Concrete.EntityFramework.Mappings
{
  public class SaleMap : EntityTypeConfiguration<Sale>
  {
    public SaleMap()
    {
      ToTable("Sales","dbo");
HasKey(x => x.SaleId);
Property(x => x.SaleId).HasColumnName("SaleId");
Property(x => x.Customer).HasColumnName("Customer");
Property(x => x.CustomerId).HasColumnName("CustomerId");
Property(x => x.InvoiceNo).HasColumnName("InvoiceNo");
Property(x => x.InvoiceImage).HasColumnName("InvoiceImage");
Property(x => x.InvoiceImageExt).HasColumnName("InvoiceImageExt");
Property(x => x.Product).HasColumnName("Product");
Property(x => x.ProductId).HasColumnName("ProductId");
Property(x => x.AmountOfSales).HasColumnName("AmountOfSales");
Property(x => x.User).HasColumnName("User");
Property(x => x.UserId).HasColumnName("UserId");
Property(x => x.InvoiceDate).HasColumnName("InvoiceDate");
Property(x => x.AddDate).HasColumnName("AddDate");
Property(x => x.IsApproved).HasColumnName("IsApproved");
Property(x => x.ApprovedDate).HasColumnName("ApprovedDate");
Property(x => x.NotApproved).HasColumnName("NotApproved");
Property(x => x.NotApprovedDate).HasColumnName("NotApprovedDate");
Property(x => x.Reason).HasColumnName("Reason");
Property(x => x.InvoiceTotal).HasColumnName("InvoiceTotal");

    }
  }
}