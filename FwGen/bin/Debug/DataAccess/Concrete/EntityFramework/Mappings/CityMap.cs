using System.Data.Entity.ModelConfiguration;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.DataAccess.Concrete.EntityFramework.Mappings
{
  public class CityMap : EntityTypeConfiguration<City>
  {
    public CityMap()
    {
      ToTable(@"Cities","dbo");
HasKey(x => x.CityId);
Property(x => x.CityId).HasColumnName("CityId");
Property(x => x.CityName).HasColumnName("CityName");

    }
  }
}