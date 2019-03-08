using System.Data.Entity.ModelConfiguration;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.DataAccess.Concrete.EntityFramework.Mappings
{
  public class UserTypeMap : EntityTypeConfiguration<UserType>
  {
    public UserTypeMap()
    {
      ToTable("UserTypes","dbo");
HasKey(x => x.UserTypeId);
Property(x => x.UserTypeId).HasColumnName("UserTypeId");
Property(x => x.UserTypeName).HasColumnName("UserTypeName");

    }
  }
}