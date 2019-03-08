using System.Data.Entity.ModelConfiguration;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.DataAccess.Concrete.EntityFramework.Mappings
{
  public class UnitTypeMap : EntityTypeConfiguration<UnitType>
  {
    public UnitTypeMap()
    {
      ToTable("UnitTypes","dbo");
HasKey(x => x.UnitTypeId);
Property(x => x.UnitTypeId).HasColumnName("UnitTypeId");
Property(x => x.UnitTypeName).HasColumnName("UnitTypeName");

    }
  }
}