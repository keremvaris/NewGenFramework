using System.Data.Entity.ModelConfiguration;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.DataAccess.Concrete.EntityFramework.Mappings
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