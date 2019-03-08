using System.Data.Entity.ModelConfiguration;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.DataAccess.Concrete.EntityFramework.Mappings
{
  public class UserRoleMap : EntityTypeConfiguration<UserRole>
  {
    public UserRoleMap()
    {
      ToTable("UserRoles","dbo");
HasKey(x => x.UserRoleId);
Property(x => x.UserRoleId).HasColumnName("UserRoleId");
Property(x => x.RoleId).HasColumnName("RoleId");
Property(x => x.Role).HasColumnName("Role");
Property(x => x.User).HasColumnName("User");
Property(x => x.UserId).HasColumnName("UserId");

    }
  }
}