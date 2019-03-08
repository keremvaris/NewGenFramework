using System.Data.Entity.ModelConfiguration;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.DataAccess.Concrete.EntityFramework.Mappings
{
  public class CustomerMap : EntityTypeConfiguration<Customer>
  {
    public CustomerMap()
    {
      ToTable("Customers", "dbo");
      HasKey(x => x.CustomerId);
      Property(x => x.CustomerId).HasColumnName("CustomerId");
      Property(x => x.CustomerName).HasColumnName("CustomerName");
      Property(x => x.TaxNo).HasColumnName("TaxNo");
      Property(x => x.TaxAdministration).HasColumnName("TaxAdministration");
      Property(x => x.AddingUserId).HasColumnName("AddingUserId");
      Property(x => x.DateAdded).HasColumnName("DateAdded");
      Property(x => x.State).HasColumnName("State");
      Property(x => x.RelationalPersonName).HasColumnName("RelationalPersonName");
      Property(x => x.RelationalPersonSurname).HasColumnName("RelationalPersonSurname");
      Property(x => x.MobilePhone).HasColumnName("MobilePhone");

    }
  }
}