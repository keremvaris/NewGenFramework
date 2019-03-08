using System.Data.Entity.ModelConfiguration;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.DataAccess.Concrete.EntityFramework.Mappings
{
  public class RelationalPersonMap : EntityTypeConfiguration<RelationalPerson>
  {
    public RelationalPersonMap()
    {
      ToTable("RelationalPersons","dbo");
HasKey(x => x.RelationalPersonId);
Property(x => x.RelationalPersonId).HasColumnName("RelationalPersonId");
Property(x => x.RelationalPersonName).HasColumnName("RelationalPersonName");
Property(x => x.RelationalPersonSurname).HasColumnName("RelationalPersonSurname");
Property(x => x.MobilePhone).HasColumnName("MobilePhone");

    }
  }
}