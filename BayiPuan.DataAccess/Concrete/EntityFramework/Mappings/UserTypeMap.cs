using System.Data.Entity.ModelConfiguration;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.DataAccess.Concrete.EntityFramework.Mappings
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