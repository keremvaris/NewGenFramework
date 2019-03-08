using System.Data.Entity.ModelConfiguration;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.DataAccess.Concrete.EntityFramework.Mappings
{
  public class RoleMap : EntityTypeConfiguration<Role>
  {
    public RoleMap()
    {
      ToTable("Roles","dbo");
HasKey(x => x.RoleId);
Property(x => x.RoleId).HasColumnName("RoleId");
Property(x => x.RoleName).HasColumnName("RoleName");

    }
  }
}